using System.IO.Compression;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Spoleto.Common;
using Spoleto.Common.Helpers;
using Spoleto.TrueApi.Documents;


namespace Spoleto.TrueApi
{
    /// <summary>
    /// Провайдер для работы с сервисом True Api.
    /// </summary>
    public class TrueApiProvider : ITrueApiProvider
    {
        private readonly ITrueApiTokenProvider _tokenProvider;
        private string _token;

        private readonly HttpClient _httpClient;
        private readonly bool _disposeHttpClient;

        public TrueApiProvider() : this(new HttpClient(), true)
        {
        }

        public TrueApiProvider(HttpClient httpClient, bool disposeHttpClient) : this(httpClient, disposeHttpClient, new TrueApiTokenProvider(httpClient, false))
        {
        }

        public TrueApiProvider(HttpClient httpClient, bool disposeHttpClient, ITrueApiTokenProvider tokenProvider)
        {
            _httpClient = httpClient;
            _disposeHttpClient = disposeHttpClient;
            _tokenProvider = tokenProvider;

            _httpClient.ConfigureHttpClient();
        }

        #region IDisposable
        bool _disposed;

        protected virtual void Dispose(bool disposing)
        {
            if (disposing && !_disposed)
            {
                _disposed = true;
                if (_disposeHttpClient)
                {
                    _httpClient.Dispose();
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion

        private static T FromResult<T>(string bodyJson)
        {
            if (typeof(T) == typeof(string))
            {
                if (Guid.TryParse(bodyJson, out var guidRes))
                    return (T)((object)guidRes.ToString());
                else if (long.TryParse(bodyJson, out var longRes))
                    return (T)((object)longRes.ToString());
                else
                    return JsonHelper.FromJson<T>(bodyJson);
            }
            else
                return JsonHelper.FromJson<T>(bodyJson);
        }

        private async Task InitHeaders(HttpRequestMessage requestMessage, TrueApiProviderOption settings)
        {
            requestMessage.ConfigureRequestMessage();

            var token = await GetTokenAsync(settings).ConfigureAwait(false);
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        private async Task<string> GetTokenAsync(TrueApiProviderOption settings) => _token ??= (await _tokenProvider.GetTokenAsync(settings).ConfigureAwait(false)).Token;


        private async Task<T> InvokeAsync<T>(TrueApiProviderOption settings, Uri uri, HttpMethod method, string requestJsonContent = null,
            bool isZipResponse = false)
        {
            var client = _httpClient;

            using var requestMessage = new HttpRequestMessage(method, uri);
            await InitHeaders(requestMessage, settings).ConfigureAwait(false);
            if (requestJsonContent != null)
            {
                requestMessage.Content = new StringContent(requestJsonContent, DefaultSettings.Encoding, DefaultSettings.ContentType);
            }

            using var responseMessage = await client.SendAsync(requestMessage).ConfigureAwait(false);

            if (responseMessage.IsSuccessStatusCode)
            {
                if (isZipResponse)//todo: тест-тест-тест. Протестировать на самом ли деле приходит архив.
                {
                    using var memoryStream = new MemoryStream();
                    await responseMessage.Content.CopyToAsync(memoryStream).ConfigureAwait(false);
                    memoryStream.Position = 0;

                    using ZipArchive archive = new ZipArchive(memoryStream);
                    var jsonFileEntry = archive.Entries.FirstOrDefault();
                    var objectResult = default(T);
                    if (jsonFileEntry != null)
                    {
                        using var zipFileEntryStream = jsonFileEntry.Open();
                        objectResult = await JsonHelper.FromJsonStreamAsync<T>(zipFileEntryStream).ConfigureAwait(false);
                    }

                    return objectResult;
                }
                else
                {
                    string result = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
                    var objectResult = FromResult<T>(result);
                    return objectResult;
                }
            }

            var errorResult = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);

            if (!String.IsNullOrEmpty(errorResult))
            {
                if (responseMessage.Content.Headers.ContentType.MediaType == ContentTypes.ApplicationJson)
                {
                    try
                    {
                        var errorModel = JsonHelper.FromJson<ErrorModel>(errorResult);

                        //todo: Будет ли тут кейс с истекшим токеном? Какая тогда будет ошибка?
                        if (responseMessage.StatusCode == System.Net.HttpStatusCode.Unauthorized
                            && errorModel?.ErrorMessage == "unauthorized")//ExpiredToken Переданный токен не активен
                        {
                            _token = null;
                            return await InvokeAsync<T>(settings, uri, method, requestJsonContent).ConfigureAwait(false);
                        }

                        throw new Exception(errorModel.ErrorMessage);
                    }
                    catch (JsonException)
                    {
                        throw new Exception(errorResult);
                    }
                }
                else
                {
                    throw new Exception(errorResult);
                }
            }
            else
            {
                throw new Exception(responseMessage.ReasonPhrase);
            }
        }

        /// <summary>
        /// Создание документа любого типа.
        /// </summary>
        /// <typeparam name="T">Тип создаваемого документа.</typeparam>
        /// <param name="settings">Настройки провайдер.</param>
        /// <param name="documentInfo">Информация о создаваемом документе.</param>
        /// <param name="productGroup">Группа документа.</param>
        /// <returns>Идентификатор созданного документа.</returns>
        public async Task<string> CreateDocumentAsync<T>(TrueApiProviderOption settings, DocumentInfoModel<T> documentInfo, ProductGroup productGroup) where T : ITrueApiDocument
        {
            var uri = new Uri(UriHelper.UrlCombine(settings.ServiceUrl, $"/lk/documents/create?pg={productGroup}"));

            var documentInfoJson = JsonHelper.ToJson(documentInfo);

            return await InvokeAsync<string>(settings, uri, HttpMethod.Post, documentInfoJson).ConfigureAwait(false);
        }

        // /api/v4/true-api/doc/{docId}/info
        /// <summary>
        /// Получение информации о документе по его идентификатору.
        /// </summary>
        /// <typeparam name="T">Тип создаваемого документа.</typeparam>
        /// <param name="settings">Настройки провайдер.</param>
        /// <param name="productGroup">Группа документа.</param>
        /// <param name="documentId">Идентификатор документа.</param>
        /// <returns>Информации о документе.</returns>
        public async Task<List<DocumentInfoReportModel<T>>> GetDocumentByIdAsync<T>(TrueApiProviderOption settings, ProductGroup? productGroup, string documentId) where T : ITrueApiDocument
        {
            var path = $"/doc/{documentId}/info";
            if (productGroup != null)
            {
                path += "?pg={productGroup}";
            }

            var uri = new Uri(UriHelper.UrlCombine("https://markirovka.crpt.ru/api/v4/true-api", path)); //todo: hardcode

            return await InvokeAsync<List<DocumentInfoReportModel<T>>>(settings, uri, HttpMethod.Get).ConfigureAwait(false);
        }

        /// <summary>
        /// Метод для получения идентификатора заказа на создание кодов маркировки (идентификации).
        /// </summary>
        /// <remarks>
        /// cis - Контрольный идентификационный знак.
        /// </remarks>
        /// <param name="settings">Настройки провайдера.</param>
        /// <param name="cisOrderSearchInfo">Информация для поиска заказа.</param>
        /// <returns>Идентификатор заказа на создание кодов маркировки (идентификации).</returns>
        [Obsolete]
        public async Task<string> GetCisOrderIdAsync(TrueApiProviderOption settings, CisOrderSearchInfoModel cisOrderSearchInfo)
        {
            var uri = new Uri(UriHelper.UrlCombine(settings.ServiceUrl, $"/cises/my"));

            var cisOrderSearchInfoJson = JsonHelper.ToJson(cisOrderSearchInfo);

            var result = await InvokeAsync<CisOrderSearchResultModel>(settings, uri, HttpMethod.Post, cisOrderSearchInfoJson).ConfigureAwait(false);

            return result?.Uuid;
        }

        /// <summary>
        /// Метод для получения статуса заказа на список кодов маркировки (идентификации).
        /// </summary>
        /// <remarks>
        /// cis - Контрольный идентификационный знак.
        /// </remarks>
        /// <param name="settings">Настройки провайдера.</param>
        /// <param name="cisOrderUuid">Идентификатор заказа</param>
        /// <returns>Статус заказа.</returns>
        [Obsolete]
        public async Task<CisOrderStatus> GetCisOrderStatusAsync(TrueApiProviderOption settings, string cisOrderUuid)
        {
            var uri = new Uri(UriHelper.UrlCombine(settings.ServiceUrl, $"/cises/orders/{cisOrderUuid}/status"));

            var result = await InvokeAsync<CisOrderStatus>(settings, uri, HttpMethod.Get).ConfigureAwait(false);

            return result;
        }

        /// <summary>
        /// Запрос результата заказа на список кодов маркировки (идентификации).
        /// </summary>
        /// <remarks>
        /// cis - Контрольный идентификационный знак.
        /// </remarks>
        /// <param name="settings">Настройки провайдера.</param>
        /// <param name="cisOrderUuid">Идентификатор заказа.</param>
        /// <returns>Подробная информация по заказу с кодами идентификации (КИ).</returns>
        [Obsolete]
        public async Task<List<CisFullInfoModel>> GetCisFullInfoListAsync(TrueApiProviderOption settings, string cisOrderUuid)
        {
            var uri = new Uri(UriHelper.UrlCombine(settings.ServiceUrl, $"/cises/orders/{cisOrderUuid}/result"));

            return await InvokeAsync<List<CisFullInfoModel>>(settings, uri, HttpMethod.Get, isZipResponse: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Метод для получения общедоступной информации о списке кодов маркировки (идентификации).
        /// </summary>
        /// <remarks>
        /// cis - Контрольный идентификационный знак.
        /// </remarks>
        /// <param name="settings">Настройки провайдера.</param>
        /// <param name="cisList">Список запрашиваемых кодов идентификации (КИ).
        /// Не более 1000 КИ в массиве.</param>
        /// <param name="productGroup">Товарная группа.
        /// Параметр обязательно указывать для товарных групп: milk – "Молочная продукция"; water – "Упакованная вода"</param>
        /// <returns>Подробная общедоступная информация о списке запрашиваемых кодов идентификации (КИ).</returns>
        public async Task<List<CisPublicInfoContainerModel>> GetCisPublicInfoListAsync(TrueApiProviderOption settings,
            List<string> cisList, ProductGroup? productGroup = null)
        {
            int maxCount = 999;
            var urlString = new StringBuilder(UriHelper.UrlCombine(settings.ServiceUrl, "/cises/info"));
            if (productGroup != null)
            {
                urlString.Append($"?pg={productGroup}");
            }
            var uri = new Uri(urlString.ToString());
            var mcCodeList = new List<List<string>>();
            int i = maxCount + 1;
            List<string> res = null;
            foreach (var mcItem in cisList)
            {
                if (i > maxCount)
                {
                    i = 1;
                    res = new List<string>();
                    mcCodeList.Add(res);
                }
                res.Add(mcItem);
                i++;
            }
            var result = new List<CisPublicInfoContainerModel>();
            foreach (var codes in mcCodeList)
            {
                var cisListJson = JsonHelper.ToJson(codes);
                result.AddRange(await InvokeAsync<List<CisPublicInfoContainerModel>>(settings, uri, HttpMethod.Post, cisListJson).ConfigureAwait(false));
            }
            return result;

        }

        ///// <summary>
        ///// Метод для получения данных о списке кодов маркировки (идентификации).
        ///// </summary>
        ///// <remarks>
        ///// cis - Контрольный идентификационный знак.
        ///// </remarks>
        ///// <param name="settings">Настройки провайдера.</param>
        ///// <param name="cisList">Список запрашиваемых кодов идентификации (КИ).
        ///// В списке должно быть от 1 до 35 (демо контур) - 50 (продуктивный контур).</param>
        ///// <returns>Подробная информация о списке запрашиваемых кодов идентификации (КИ).</returns>
        //public async Task<List<CisInfoModel>> GetCisInfoListAsync(TrueApiProviderOption settings, List<string> cisList)
        //{
        //    var uri = new Uri(UriHelper.UrlCombine(settings.ServiceUrl, $"/cises/list?{string.Join("&", cisList.Select(x => "values=" + System.Web.HttpUtility.UrlEncode(x)))}"));

        //    return await InvokeAsync<List<CisInfoModel>>(settings, uri, HttpMethod.Post).ConfigureAwait(false);
        //}

        // obsolete
        /// <summary>
        /// Метод получения краткой информации о списке КИ по заданному фильтру.
        /// </summary>
        /// <remarks>
        /// cis - Контрольный идентификационный знак.
        /// </remarks>
        /// <param name="settings">Настройки провайдера.</param>
        /// <param name="searchInfoModel">Фильтр для поиска КИ.</param>
        /// <returns>Краткая информация о списке КИ.</returns>
        public async Task<List<CisShortInfoContainerModel>> GetCisShortInfoListAsync(TrueApiProviderOption settings,
            List<string> searchCisList)
        {
            //settings.ServiceUrl.Replace("/v3/", "/v4/")
            var searchInfoModelJson = JsonHelper.ToJson(searchCisList.ToArray());
            var uri = new Uri(UriHelper.UrlCombine(settings.ServiceUrl, $"/cises/short/list"));
            return await InvokeAsync<List<CisShortInfoContainerModel>>(settings, uri, HttpMethod.Post, searchInfoModelJson).ConfigureAwait(false);

        }

        //// obsolete
        ///// <summary>
        ///// Метод получения подробной информации о товарах по заданному фильтру.
        ///// </summary>
        ///// <param name="settings">Настройки провайдера.</param>
        ///// <param name="searchInfoModel">Фильтр для поиска товаров.</param>
        ///// <returns>Список товаров с подробной информацией, которые доступны в данный момент времени участнику оборота товаров.</returns>
        //public async Task<List<ProductFullInfoContainerModel>> GetProductFullInfoListAsync(TrueApiProviderOption settings,
        //    List<string> searchCisList)
        //{
        //    //settings.ServiceUrl = settings.ServiceUrl.Replace("/v3/", "/v4/");
        //    var searchInfoModelJson = JsonHelper.ToJson(searchCisList.ToArray());
        //    var uri = new Uri(UriHelper.UrlCombine(settings.ServiceUrl, $"/cises/info"));

        //    return await InvokeAsync<List<ProductFullInfoContainerModel>>(settings, uri, HttpMethod.Post).ConfigureAwait(false);
        //}



        // /api/v4/true-api/tn-ved/search + другая модель в теле
        /// <summary>
        /// Метод получения списка десятизначных кодов ТН ВЭД.
        /// </summary>
        /// <param name="settings">Настройки провайдера.</param>
        /// <param name="searchInfoModel">Фильтр для поиска кодов ТН ВЭД.</param>
        /// <returns>Массив информации об удовлетворяющих запросу кодах ТН ВЭД и общее число элементов данного массива.</returns>
        public async Task<TnvedInfoContainerModel> GetTnvedInfoListAsync(TrueApiProviderOption settings,
            TnvedSearchModel searchInfoModel)
        {
            var searchInfoModelQuery = HttpHelper.ToQueryString(searchInfoModel);
            var uri = new Uri(UriHelper.UrlCombine(settings.ServiceUrl, $"/facade/tnved/search?{searchInfoModelQuery}"));

            return await InvokeAsync<TnvedInfoContainerModel>(settings, uri, HttpMethod.Get).ConfigureAwait(false);
        }

        // Добавился обязательный параметр в теле - gtin
        /// <summary>
        /// Метод получения кода товарной группы по КИ товара (GTIN).
        /// </summary>
        /// <param name="settings">Настройки провайдера.</param>
        /// <param name="searchInfoModel">Фильтр для поиска товарных групп.</param>
        /// <returns>Массив информации о товарных группах для запрашиваемых кодов товаров (в одном запросе указываются несколько кодов товаров).</returns>
        public async Task<List<ProductGroupInfoModel>> GetProductGroupInfoListAsync(TrueApiProviderOption settings, ProductGroupSearchModel searchInfoModel)
        {
            var searchInfoModelJson = JsonHelper.ToJson(searchInfoModel);
            var uri = new Uri(UriHelper.UrlCombine(settings.ServiceUrl, $"/product/route/gtin"));

            return await InvokeAsync<List<ProductGroupInfoModel>>(settings, uri, HttpMethod.Post, searchInfoModelJson).ConfigureAwait(false);
        }

        // Убран параметр productGroupCode из тела
        /// <summary>
        /// Получение списка КИ участника оборота товаров по заданному фильтру (8.1.3).
        /// </summary>
        /// <param name="settings">Настройки провайдера.</param>
        /// <param name="searchModel">Фильтр для поиска списка КИ.</param>
        /// <returns>Список КИ участника оборота товаров по заданному фильтру.</returns>
        public async Task<DispenserTaskModel> CreateDispenserTaskCisListByFilterAsync(TrueApiProviderOption settings, CisSearchModel searchModel)
        {
            var searchModelJson = JsonHelper.ToJson(searchModel);
            var uri = new Uri(UriHelper.UrlCombine(settings.ServiceUrl, $"/dispenser/tasks"));

            return await InvokeAsync<DispenserTaskModel>(settings, uri, HttpMethod.Post, searchModelJson).ConfigureAwait(false);
        }

        /// <summary>
        /// Метод получения статуса задания на выгрузку по ID выгрузки.
        /// </summary>
        /// <param name="settings">Настройки провайдера.</param>
        /// <param name="taskId">Идентификатор формируемой выгрузки (идентификатор, который вернулся в ответе на запрос о формировании выгрузки.)</param>
        /// <param name="productGroup">Кодовое значение товарной группы.
        /// Указанное значение должно совпадать с "productGroupCode", сформированных ранее заданий.</param>
        /// <returns>Текущий статус задания на выгрузку + информация, необходимая пользователю для дальнейшей работы.</returns>
        public async Task<DispenserTaskStatusModel> GetDispenserTaskStatusAsync(TrueApiProviderOption settings, string taskId, ProductGroup productGroup)
        {
            var uri = new Uri(UriHelper.UrlCombine(settings.ServiceUrl, $"/dispenser/tasks/{taskId}?pg={(int)productGroup}"));

            return await InvokeAsync<DispenserTaskStatusModel>(settings, uri, HttpMethod.Get).ConfigureAwait(false);
        }

        /// <summary>
        /// Метод получения CSV по списку КИ участника оборота товаров по заданному фильтру (8.5.3).
        /// </summary>
        /// <param name="settings">Настройки провайдера.</param>
        /// <param name="taskId">Идентификатор формируемой выгрузки (идентификатор, который вернулся в ответе на запрос о формировании выгрузки.)</param>
        /// <param name="productGroup">Кодовое значение товарной группы.
        /// Указанное значение должно совпадать с "productGroupCode", сформированных ранее заданий.</param>
        /// <returns>Файл в формате CSV со сведениями о КИ, которые находятся на балансе у участника оборота товаров.</returns>
        public async Task<string> GetCisListByFilterAsync(TrueApiProviderOption settings, string taskId, ProductGroup productGroup)
        {
            var uri = new Uri(UriHelper.UrlCombine(settings.ServiceUrl, $"/dispenser/results/{taskId}/file?pg={(int)productGroup}/file"));

            return await InvokeAsync<string>(settings, uri, HttpMethod.Get, isZipResponse: true).ConfigureAwait(false);
        }

        /// <summary>
        /// Метод получения квитанций результата обработки универсального документа по идентификатору документа.
        /// </summary>
        /// <param name="settings">Настройки провайдера.</param>
        /// <param name="fileId">Полное имя из "ИдФайл" XML направленного документа</param>
        /// <returns>Квитанция по результатам обработки документов ЭДО содержит перечень из первых 10 ошибок по документу.</returns>
        public async Task<ProcessingResultModel> GetProcessingResultAsync(TrueApiProviderOption settings, string fileId)
        {
            var uri = new Uri(UriHelper.UrlCombine(settings.ServiceUrl, $"/documents/edo/tpr/ud?fileId={fileId}"));

            return await InvokeAsync<ProcessingResultModel>(settings, uri, HttpMethod.Get).ConfigureAwait(false);
        }
    }
}
