using System.Text;
using Spoleto.Common;
using Spoleto.Common.Helpers;

namespace Spoleto.TrueApi
{
    /// <summary>
    /// Провайдер для работы с методами Национального каталога.
    /// </summary>
    public class TrueApiNkProvider : ITrueApiNkProvider, IDisposable
    {
        private readonly HttpClient _httpClient;
        private readonly bool _disposeHttpClient;

        public TrueApiNkProvider() : this(new HttpClient(), true)
        {
            _httpClient.ConfigureHttpClient();
        }

        public TrueApiNkProvider(HttpClient httpClient, bool disposeHttpClient)
        {
            _httpClient = httpClient;
            _disposeHttpClient = disposeHttpClient;
        }

        private Task<T> InvokeGetAsync<T>(Uri uri)
            => InvokeAsync<T>(uri, HttpMethod.Get, null);

        private async Task<T> InvokeAsync<T>(Uri uri, HttpMethod method, string requestJsonContent = null)
        {
            var client = _httpClient;

            using var requestMessage = new HttpRequestMessage(method, uri);
            requestMessage.ConfigureRequestMessage();
            if (requestJsonContent != null)
            {
                requestMessage.Content = new StringContent(requestJsonContent, DefaultSettings.Encoding, DefaultSettings.ContentType);
            }

            using var responseMessage = await client.SendAsync(requestMessage).ConfigureAwait(false);

            var result = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
            if (responseMessage.IsSuccessStatusCode)
            {
                var nkContainer = JsonHelper.FromJson<T>(result);
                return nkContainer;
            }

            else if (!String.IsNullOrEmpty(result))
            {
                if (responseMessage.Content.Headers.ContentType.MediaType == ContentTypes.ApplicationJson)
                {
                    var errorModel = JsonHelper.FromJson<ErrorModel>(result);

                    throw new Exception(errorModel.ErrorMessage);
                }
                else
                {
                    throw new Exception(result);
                }
            }
            else
            {
                throw new Exception(responseMessage.ReasonPhrase);
            }
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

        /// <summary>
        /// Метод получения списка атрибутов как публичных, так и приватных для запрашивающего аккаунта.
        /// </summary>
        /// <param name="settings">Настройки провайдера.</param>
        /// <param name="attrType">Тип атрибута.</param>
        /// <param name="catId">Идентификатор категории, к которой относятся атрибуты.
        /// Является обязательным, если указании attr_type и не указан tnved.</param>
        /// <param name="tnved">код ТНВЭД (10 знаков) или группы ТНВЭД (4 знака), для которого запрашивается набор атрибутов.
        /// Обязателен, если указан параметр attr_type (тип атрибута) и не указан cat_id.</param>
        /// <returns>Список атрибутов как публичных, так и приватных для запрашивающего аккаунта</returns>
        public async Task<NkContainerListModel<NkAttributeModel>> GetAttributesAsync(TrueApiNkProviderOption settings, NkAttributeType attrType, string catId = null, string tnved = null)
        {
            var urlString = new StringBuilder(UriHelper.UrlCombine(settings.ServiceUrl, $"/attributes?apikey={settings.ApiKey}"));
            if (catId != null || tnved != null)
            {
                urlString.Append($"&attr_type={attrType}");

                if (catId != null)
                    urlString.Append($"&cat_id={catId}");
                else
                    urlString.Append($"&tnved={tnved}");
            }

            var uri = new Uri(urlString.ToString());

            return await InvokeGetAsync<NkContainerListModel<NkAttributeModel>>(uri).ConfigureAwait(false);
        }

        /// <summary>
        /// Метод получения списка торговых марок
        /// </summary>
        /// <param name="settings">Настройки провайдера.</param>
        /// <returns>Список торговых марок.</returns>
        public async Task<NkContainerListModel<NkBrandModel>> GetBrandsAsync(TrueApiNkProviderOption settings)
        {
            var uri = new Uri(UriHelper.UrlCombine(settings.ServiceUrl, $"/brands?apikey={settings.ApiKey}"));

            return await InvokeGetAsync<NkContainerListModel<NkBrandModel>>(uri).ConfigureAwait(false);
        }
        /// <summary>
        /// Метод получения дерева категорий, корень дерева не возвращается
        /// </summary>
        /// <param name="settings">Настройки провайдера.</param>
        /// <returns>Список данных о каждом элементе дерева категорий.</returns>
        public async Task<NkContainerListModel<NkCategoryModel>> GetCategoriesAsync(TrueApiNkProviderOption settings)
        {
            var uri = new Uri(UriHelper.UrlCombine(settings.ServiceUrl, $"/categories?apikey={settings.ApiKey}"));

            return await InvokeGetAsync<NkContainerListModel<NkCategoryModel>>(uri).ConfigureAwait(false);
        }

        /// <summary>
        /// Метод генерации черновиков кодов товаров (GTIN).
        /// </summary>
        /// <param name="settings">Настройки провайдера.</param>
        /// <param name="quantity">Количество новых черновиков кодов товаров, которые нужно сгенерировать.
        /// Обязателен при запросе генерации новых черновиков gtin</param>
        /// <param name="exist">Признак, обозначающий запрос уже сгенерированных и имеющихся в базе пользователя gtin</param>
        /// <param name="supplierKey">Ключ поставщика или производителя товаров</param>
        /// <returns>Список черновиков кодов товаров.</returns>
        public async Task<NkContainerModel<NkGtinDraftModel>> GenerateGtinDraftsAsync(TrueApiNkProviderOption settings, int quantity, bool? exist = null, string supplierKey = null)
        {
            var urlString = new StringBuilder(UriHelper.UrlCombine(settings.ServiceUrl, $"/generate-gtins?apikey={settings.ApiKey}&quantity={quantity}"));
            if (exist != null)
            {
                urlString.Append($"&exist={(exist.Value ? "1" : "0")}");
            }

            if (supplierKey != null)
            {
                urlString.Append($"&supplier_key={supplierKey}");
            }

            var uri = new Uri(urlString.ToString());

            return await InvokeGetAsync<NkContainerModel<NkGtinDraftModel>>(uri).ConfigureAwait(false);
        }

        /// <summary>
        /// Метод создания и обновления товаров
        /// </summary>
        /// <param name="settings">Настройки провайдера.</param>
        /// <param name="feedModel">Пакет обновлений товара.</param>
        /// <param name="supplierKey">Ключ поставщика или производителя товаров.</param>
        /// <returns>ID фида созданных/обновлённых товаров либо сообщение об ошибке (при наличии).</returns>
        public async Task<NkContainerModel<NkFeedResultModel>> CreateFeedAsync(TrueApiNkProviderOption settings, NkFeedModel feedModel, string supplierKey = null)
        {
            var urlString = new StringBuilder(UriHelper.UrlCombine(settings.ServiceUrl, $"/feed?apikey={settings.ApiKey}"));

            if (supplierKey != null)
            {
                urlString.Append($"&supplier_key={supplierKey}");
            }

            var uri = new Uri(urlString.ToString());

            var feedModelJson = JsonHelper.ToJson(feedModel);

            return await InvokeAsync<NkContainerModel<NkFeedResultModel>>(uri, HttpMethod.Post, feedModelJson).ConfigureAwait(false);
        }

        /// <summary>
        /// Метод получения статуса фида по ID фида от его заказчика
        /// </summary>
        /// <remarks>
        /// Результат возможно получить только для тех фидов, которые были отправлены компанией (лабораторией).
        /// </remarks>
        /// <param name="settings">Настройки провайдера.</param>
        /// <param name="feedId">Идентификатор фида</param>
        /// <param name="supplierKey">Ключ api_key аккаунта владельца товаров.
        /// Предназначен для лабораторий и других поставщиков контента, которые от имени владельца товаров размещают карточки товаров в его аккаунте</param>
        /// <returns>Статус фида.</returns>
        public async Task<NkContainerModel<NkFeedStatusModel>> GetFeedStatusAsync(TrueApiNkProviderOption settings, string feedId, string supplierKey = null)
        {
            var urlString = new StringBuilder(UriHelper.UrlCombine(settings.ServiceUrl, $"/feed-status?apikey={settings.ApiKey}&feed_id={feedId}"));
            if (supplierKey != null)
            {
                urlString.Append($"&supplier_key={supplierKey}");
            }

            var uri = new Uri(urlString.ToString());

            return await InvokeGetAsync<NkContainerModel<NkFeedStatusModel>>(uri).ConfigureAwait(false);
        }

        /// <summary>
        /// Метод отправки на модерацию карточки товаров в статусе "Черновик"
        /// </summary>
        /// <param name="settings">Настройки провайдера.</param>
        /// <param name="goodId">ID товара в каталоге.</param>
        /// <returns>Информация об отправке на модерацию.</returns>
        public async Task<NkContainerModel<NkModerationModel>> SendToModerationAsync(TrueApiNkProviderOption settings, string goodId)
        {
            var uri = new Uri(UriHelper.UrlCombine(settings.ServiceUrl, $"/feed-moderation?apikey={settings.ApiKey}&good_id={goodId}"));

            return await InvokeGetAsync<NkContainerModel<NkModerationModel>>(uri).ConfigureAwait(false);
        }

        /// <summary>
        /// Метод отправки на модерацию карточки товаров в статусе "Черновик"
        /// </summary>
        /// <param name="settings">Настройки провайдера.</param>
        /// <param name="gtin">Код товара.</param>
        /// <param name="inn">ИНН аккаунта.</param>
        /// <returns>Информация об отправке на модерацию.</returns>
        public async Task<NkContainerModel<NkModerationModel>> SendToModerationAsync(TrueApiNkProviderOption settings, string gtin, string inn)
        {
            var uri = new Uri(UriHelper.UrlCombine(settings.ServiceUrl, $"/feed-moderation?apikey={settings.ApiKey}&gtin={gtin}&inn={inn}"));

            return await InvokeGetAsync<NkContainerModel<NkModerationModel>>(uri).ConfigureAwait(false);
        }

        /// <summary>
        /// Метод получения полной информации о товаре.
        /// </summary>
        /// <param name="settings">Настройки провайдера.</param>
        /// <param name="searchInfoModel">Информация для поиска полной информации о товаре.</param>
        /// <returns>Полная информация о товаре.</returns>
        public async Task<NkContainerListModel<NkProductFullInfoModel>> GetProductFullInfoAsync(TrueApiNkProviderOption settings, NkProducSearchInfoModel searchInfoModel)
        {
            var searchInfoModelQuery = HttpHelper.ToQueryString(searchInfoModel);
            var uri = new Uri(UriHelper.UrlCombine(settings.ServiceUrl, $"/product?apikey={settings.ApiKey}&{searchInfoModelQuery}"));

            return await InvokeGetAsync<NkContainerListModel<NkProductFullInfoModel>>(uri).ConfigureAwait(false);
        }

        /// <summary>
        /// Метод получения карточки товара, в том числе неопубликованной карточки.
        /// </summary>
        /// <remarks>
        /// Метод возвращает всю имеющуюся информацию о продукте, то есть все заполненные атрибуты карточки товара независимо от статуса самой карточки,
        /// в отличие от метода product <see cref="GetProductFullInfoAsync"/>, который возвращает информацию только по опубликованным карточкам.
        /// </remarks>
        /// <param name="settings">Настройки провайдера.</param>
        /// <param name="searchInfoModel">Информация для поиска полной информации о товаре.</param>
        /// <returns>Карточки товара, в том числе неопубликованные карточки.</returns>
        public async Task<NkContainerListModel<NkFeedProductFullInfoModel>> GetFeedProductFullInfoAsync(TrueApiNkProviderOption settings, NkFeedProducSearchInfoModel searchInfoModel)
        {
            var searchInfoModelQuery = HttpHelper.ToQueryString(searchInfoModel);
            var uri = new Uri(UriHelper.UrlCombine(settings.ServiceUrl, $"/feed-product?apikey={settings.ApiKey}&{searchInfoModelQuery}"));

            return await InvokeGetAsync<NkContainerListModel<NkFeedProductFullInfoModel>>(uri).ConfigureAwait(false);
        }

        /// <summary>
        /// Метод определения кода принадлежности товара к маркируемым товарным группам.
        /// </summary>
        /// <remarks>
        /// В одном запросе максимум может быть указано 100 кодов товара и кодов ТНВЭД суммарно
        /// </remarks>
        /// <param name="settings">Настройки провайдера.</param>
        /// <param name="searchInfoModel">Информация с кодами товара и кодами ТНВЭД.</param>
        /// <returns>Информация о принадлежности товара с указанными кодом товара или кодами ТНВЭД к маркируемым товарным группам.</returns>
        public async Task<NkContainerListModel<NkCheckProductInfoModel>> CheckProductForMarkingAsync(TrueApiNkProviderOption settings, NkCheckProductSearchInfoModel searchInfoModel)
        {
            var uri = new Uri(UriHelper.UrlCombine(settings.ServiceUrl, $"/mark-check?apikey={settings.ApiKey}"));

            var searchInfoModelJson = JsonHelper.ToJson(searchInfoModel);

            return await InvokeAsync<NkContainerListModel<NkCheckProductInfoModel>>(uri, HttpMethod.Post, searchInfoModelJson).ConfigureAwait(false);
        }

        public async Task<NkContainerModel<NkFeedProductDocumentModel>> GetFeedProductDocumentListAsync(TrueApiNkProviderOption settings, NkFeedProductDocumentSearchModel searchModel)
        {
            const int batchSize = 25; // Максимальное количество товарных позиций в запросе не должно превышать 25.

            var uri = new Uri(UriHelper.UrlCombine(settings.ServiceUrl, $"/feed-product-document?apikey={settings.ApiKey}"));
            var count = Math.Max(searchModel.GoodIds?.Count ?? 0, searchModel.Gtins?.Count ?? 0);
            var goodInfoIds = new List<NkFeedProductDocumentInfoModel>(count);
            var errors = new List<NkFeedProductDocumentErrorModel>(count);
            var apiVersion = default(decimal);

            for (int i = 0; i < count; i += batchSize)
            {
                var goodIds = searchModel.GoodIds != null ? searchModel.GoodIds.Skip(i).Take(batchSize).ToList() : null;
                var gtins = searchModel.Gtins != null ? searchModel.Gtins.Skip(i).Take(batchSize).ToList() : null;
                var model = new NkFeedProductDocumentSearchModel { GoodIds = goodIds, Gtins = gtins, PublicationAgreement = searchModel.PublicationAgreement };
                var modelJson = JsonHelper.ToJson(model);

                var result = await InvokeAsync<NkContainerModel<NkFeedProductDocumentModel>>(uri, HttpMethod.Post, modelJson).ConfigureAwait(false);
                if (result?.Result?.GoodXmls?.Count > 0)
                    goodInfoIds.AddRange(result.Result.GoodXmls);

                if (result?.Result?.Errors?.Count > 0)
                    errors.AddRange(result.Result.Errors);

                if (result?.ApiVersion != null)
                    apiVersion = result.ApiVersion;
            }

            return new NkContainerModel<NkFeedProductDocumentModel>
            {
                ApiVersion = apiVersion,
                Result = new NkFeedProductDocumentModel { GoodXmls = goodInfoIds, Errors = errors }
            };
        }

        public async Task<NkContainerModel<NkFeedProductSignModel>> SignFeedProductDocumentListAsync(TrueApiNkProviderOption settings, List<NkFeedProductSignRequestModel> requestModel)
        {
            const int batchSize = 25; // Максимальное количество товарных позиций в запросе не должно превышать 25.

            var uri = new Uri(UriHelper.UrlCombine(settings.ServiceUrl, $"/feed-product-sign-pkcs?apikey={settings.ApiKey}"));
            var signed = new List<int>(requestModel.Count);
            var errors = new List<NkFeedProductSignErrorModel>(requestModel.Count);
            var apiVersion = default(decimal);

            for (int i = 0; i < requestModel.Count; i += batchSize)
            {
                var batch = requestModel.Skip(i).Take(batchSize).ToList();
                var modelJson = JsonHelper.ToJson(batch);

                var result = await InvokeAsync<NkContainerModel<NkFeedProductSignModel>>(uri, HttpMethod.Post, modelJson).ConfigureAwait(false);
                if (result?.Result?.Signed?.Count > 0)
                    signed.AddRange(result.Result.Signed);

                if (result?.Result?.Errors?.Count > 0)
                    errors.AddRange(result.Result.Errors);

                if (result?.ApiVersion != null)
                    apiVersion = result.ApiVersion;
            }

            return new NkContainerModel<NkFeedProductSignModel>
            {
                ApiVersion = apiVersion,
                Result = new NkFeedProductSignModel { Signed = signed, Errors = errors }
            };
        }
    }
}
