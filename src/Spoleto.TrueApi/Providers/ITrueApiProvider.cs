using Spoleto.TrueApi.Documents;

namespace Spoleto.TrueApi
{
    /// <summary>
    /// Провайдер для работы с сервисом True Api.
    /// </summary>
    public interface ITrueApiProvider
    {
        /// <summary>
        /// Создание документа любого типа.
        /// </summary>
        /// <typeparam name="T">Тип создаваемого документа.</typeparam>
        /// <param name="settings">Настройки провайдера.</param>
        /// <param name="documentInfo">Информация о создаваемом документе.</param>
        /// <param name="productGroup">Группа документа.</param>
        /// <returns>Идентификатор созданного документа.</returns>
        string CreateDocument<T>(TrueApiProviderOption settings, DocumentInfoModel<T> documentInfo, ProductGroup productGroup) where T : ITrueApiDocument
            => CreateDocumentAsync(settings, documentInfo, productGroup).GetAwaiter().GetResult();

        /// <summary>
        /// Создание документа любого типа.
        /// </summary>
        /// <typeparam name="T">Тип создаваемого документа.</typeparam>
        /// <param name="settings">Настройки провайдера.</param>
        /// <param name="documentInfo">Информация о создаваемом документе.</param>
        /// <param name="productGroup">Группа документа.</param>
        /// <returns>Идентификатор созданного документа.</returns>
        Task<string> CreateDocumentAsync<T>(TrueApiProviderOption settings, DocumentInfoModel<T> documentInfo, ProductGroup productGroup) where T : ITrueApiDocument;

        /// <summary>
        /// Получение информации о документе по его идентификатору.
        /// </summary>
        /// <typeparam name="T">Тип создаваемого документа.</typeparam>
        /// <param name="settings">Настройки провайдера.</param>
        /// <param name="productGroup">Группа документа.</param>
        /// <param name="documentId">Идентификатор документа.</param>
        /// <returns>Информации о документе.</returns>
        List<DocumentInfoReportModel> GetDocumentById(TrueApiProviderOption settings, ProductGroup? productGroup, string documentId)
            => GetDocumentByIdAsync(settings, productGroup, documentId).GetAwaiter().GetResult();

        /// <summary>
        /// Получение информации о документе по его идентификатору.
        /// </summary>
        /// <typeparam name="T">Тип создаваемого документа.</typeparam>
        /// <param name="settings">Настройки провайдера.</param>
        /// <param name="productGroup">Группа документа.</param>
        /// <param name="documentId">Идентификатор документа.</param>
        /// <returns>Информации о документе.</returns>
        Task<List<DocumentInfoReportModel>> GetDocumentByIdAsync(TrueApiProviderOption settings, ProductGroup? productGroup, string documentId);

        /// <summary>
        /// Получение информации о документе по его идентификатору.
        /// </summary>
        /// <typeparam name="T">Тип создаваемого документа.</typeparam>
        /// <param name="settings">Настройки провайдера.</param>
        /// <param name="productGroup">Группа документа.</param>
        /// <param name="documentId">Идентификатор документа.</param>
        /// <returns>Информации о документе.</returns>
        List<DocumentInfoReportModel<T>> GetDocumentById<T>(TrueApiProviderOption settings, ProductGroup? productGroup, string documentId) where T : ITrueApiDocument
            => GetDocumentByIdAsync<T>(settings, productGroup, documentId).GetAwaiter().GetResult();

        /// <summary>
        /// Получение информации о документе по его идентификатору.
        /// </summary>
        /// <typeparam name="T">Тип создаваемого документа.</typeparam>
        /// <param name="settings">Настройки провайдера.</param>
        /// <param name="productGroup">Группа документа.</param>
        /// <param name="documentId">Идентификатор документа.</param>
        /// <returns>Информации о документе.</returns>
        Task<List<DocumentInfoReportModel<T>>> GetDocumentByIdAsync<T>(TrueApiProviderOption settings, ProductGroup? productGroup, string documentId) where T : ITrueApiDocument;

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
        string GetCisOrderId(TrueApiProviderOption settings, CisOrderSearchInfoModel cisOrderSearchInfo)
            => GetCisOrderIdAsync(settings, cisOrderSearchInfo).GetAwaiter().GetResult();

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
        Task<string> GetCisOrderIdAsync(TrueApiProviderOption settings, CisOrderSearchInfoModel cisOrderSearchInfo);

        /// <summary>
        /// Метод для получения статуса заказа на список кодов маркировки (идентификации).
        /// </summary>
        /// <remarks>
        /// cis - Контрольный идентификационный знак.
        /// </remarks>
        /// <param name="settings">Настройки провайдера.</param>
        /// <param name="cisOrderUuid">Идентификатор заказа.</param>
        /// <returns>Статус заказа.</returns>
        [Obsolete]
        CisOrderStatus GetCisOrderStatus(TrueApiProviderOption settings, string cisOrderUuid)
            => GetCisOrderStatusAsync(settings, cisOrderUuid).GetAwaiter().GetResult();

        /// <summary>
        /// Метод для получения статуса заказа на список кодов маркировки (идентификации).
        /// </summary>
        /// <remarks>
        /// cis - Контрольный идентификационный знак.
        /// </remarks>
        /// <param name="settings">Настройки провайдера.</param>
        /// <param name="cisOrderUuid">Идентификатор заказа.</param>
        /// <returns>Статус заказа.</returns>
        [Obsolete]
        Task<CisOrderStatus> GetCisOrderStatusAsync(TrueApiProviderOption settings, string cisOrderUuid);

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
        List<CisFullInfoModel> GetCisFullInfoList(TrueApiProviderOption settings, string cisOrderUuid)
            => GetCisFullInfoListAsync(settings, cisOrderUuid).GetAwaiter().GetResult();

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
        Task<List<CisFullInfoModel>> GetCisFullInfoListAsync(TrueApiProviderOption settings, string cisOrderUuid);

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
        List<CisPublicInfoContainerModel> GetCisPublicInfoList(TrueApiProviderOption settings, List<string> cisList, ProductGroup? productGroup = null)
            => GetCisPublicInfoListAsync(settings, cisList, productGroup).GetAwaiter().GetResult();

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
        Task<List<CisPublicInfoContainerModel>> GetCisPublicInfoListAsync(TrueApiProviderOption settings, List<string> cisList, ProductGroup? productGroup = null);

        /////// <summary>
        /////// Метод для получения данных о списке кодов маркировки (идентификации).
        /////// </summary>
        /////// <remarks>
        /////// cis - Контрольный идентификационный знак.
        /////// </remarks>
        /////// <param name="settings">Настройки провайдера.</param>
        /////// <param name="cisList">Список запрашиваемых кодов идентификации (КИ).
        /////// В списке должно быть от 1 до 35 (демо контур) - 50 (продуктивный контур).</param>
        /////// <returns>Подробная информация о списке запрашиваемых кодов идентификации (КИ).</returns>
        ////List<CisInfoModel> GetCisInfoList(TrueApiProviderOption settings, List<string> cisList)
        ////    => GetCisInfoListAsync(settings, cisList).GetAwaiter().GetResult();

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
        //Task<List<CisInfoModel>> GetCisInfoListAsync(TrueApiProviderOption settings, List<string> cisList);

        /// <summary>
        /// Метод получения краткой информации о списке КИ по заданному фильтру.
        /// </summary>
        /// <remarks>
        /// cis - Контрольный идентификационный знак.
        /// </remarks>
        /// <param name="settings">Настройки провайдера.</param>
        /// <param name="searchInfoModel">Фильтр для поиска КИ.</param>
        /// <returns>Краткая информация о списке КИ.</returns>
        List<CisShortInfoContainerModel> GetCisShortInfoList(TrueApiProviderOption settings, List<string> searchCisList)
            => GetCisShortInfoListAsync(settings, searchCisList).GetAwaiter().GetResult();

        /// <summary>
        /// Метод получения краткой информации о списке КИ по заданному фильтру.
        /// </summary>
        /// <remarks>
        /// cis - Контрольный идентификационный знак.
        /// </remarks>
        /// <param name="settings">Настройки провайдера.</param>
        /// <param name="searchInfoModel">Фильтр для поиска КИ.</param>
        /// <returns>Краткая информация о списке КИ.</returns>
        Task<List<CisShortInfoContainerModel>> GetCisShortInfoListAsync(TrueApiProviderOption settings, List<string> searchCisList);

        ///// <summary>
        ///// Метод получения подробной информации о товарах по заданному фильтру.
        ///// </summary>
        ///// <param name="settings">Настройки провайдера.</param>
        ///// <param name="searchInfoModel">Фильтр для поиска товаров.</param>
        ///// <returns>Список товаров с подробной информацией, которые доступны в данный момент времени участнику оборота товаров.</returns>
        //List<ProductFullInfoContainerModel> GetProductFullInfoList(TrueApiProviderOption settings,
        //    List<string> searchCisList)
        //    => GetProductFullInfoListAsync(settings, searchCisList).GetAwaiter().GetResult();

        ///// <summary>
        ///// Метод получения подробной информации о товарах по заданному фильтру.
        ///// </summary>
        ///// <param name="settings">Настройки провайдера.</param>
        ///// <param name="searchInfoModel">Фильтр для поиска товаров.</param>
        ///// <returns>Список товаров с подробной информацией, которые доступны в данный момент времени участнику оборота товаров.</returns>
        //Task<List<ProductFullInfoContainerModel>> GetProductFullInfoListAsync(TrueApiProviderOption settings,
        //    List<string> searchCisList);

        /// <summary>
        /// Метод получения списка десятизначных кодов ТН ВЭД.
        /// </summary>
        /// <param name="settings">Настройки провайдера.</param>
        /// <param name="searchInfoModel">Фильтр для поиска кодов ТН ВЭД.</param>
        /// <returns>Массив информации об удовлетворяющих запросу кодах ТН ВЭД и общее число элементов данного массива.</returns>
        TnvedInfoContainerModel GetTnvedInfoList(TrueApiProviderOption settings, TnvedSearchModel searchInfoModel)
            => GetTnvedInfoListAsync(settings, searchInfoModel).GetAwaiter().GetResult();

        /// <summary>
        /// Метод получения списка десятизначных кодов ТН ВЭД.
        /// </summary>
        /// <param name="settings">Настройки провайдера.</param>
        /// <param name="searchInfoModel">Фильтр для поиска кодов ТН ВЭД.</param>
        /// <returns>Массив информации об удовлетворяющих запросу кодах ТН ВЭД и общее число элементов данного массива.</returns>
        Task<TnvedInfoContainerModel> GetTnvedInfoListAsync(TrueApiProviderOption settings, TnvedSearchModel searchInfoModel);

        /// <summary>
        /// Метод получения кода товарной группы по КИ товара (GTIN).
        /// </summary>
        /// <param name="settings">Настройки провайдера.</param>
        /// <param name="searchInfoModel">Фильтр для поиска товарных групп.</param>
        /// <returns>Массив информации о товарных группах для запрашиваемых кодов товаров (в одном запросе указываются несколько кодов товаров).</returns>
        List<ProductGroupInfoModel> GetProductGroupInfoList(TrueApiProviderOption settings, ProductGroupSearchModel searchInfoModel)
            => GetProductGroupInfoListAsync(settings, searchInfoModel).GetAwaiter().GetResult();

        /// <summary>
        /// Метод получения кода товарной группы по КИ товара (GTIN).
        /// </summary>
        /// <param name="settings">Настройки провайдера.</param>
        /// <param name="searchInfoModel">Фильтр для поиска товарных групп.</param>
        /// <returns>Массив информации о товарных группах для запрашиваемых кодов товаров (в одном запросе указываются несколько кодов товаров).</returns>
        Task<List<ProductGroupInfoModel>> GetProductGroupInfoListAsync(TrueApiProviderOption settings, ProductGroupSearchModel searchInfoModel);

        /// <summary>
        /// Создание задания на получение списка КИ участника оборота товаров по заданному фильтру (8.1.3).
        /// </summary>
        /// <param name="settings">Настройки провайдера.</param>
        /// <param name="searchModel">Фильтр для поиска списка КИ.</param>
        /// <returns>Список КИ участника оборота товаров по заданному фильтру.</returns>
        DispenserTaskModel CreateDispenserTaskCisListByFilter(TrueApiProviderOption settings, CisSearchModel searchModel)
            => CreateDispenserTaskCisListByFilterAsync(settings, searchModel).GetAwaiter().GetResult();

        /// <summary>
        /// Создание задания на получение списка КИ участника оборота товаров по заданному фильтру (8.1.3).
        /// </summary>
        /// <param name="settings">Настройки провайдера.</param>
        /// <param name="searchModel">Фильтр для поиска списка КИ.</param>
        /// <returns>Список КИ участника оборота товаров по заданному фильтру.</returns>
        Task<DispenserTaskModel> CreateDispenserTaskCisListByFilterAsync(TrueApiProviderOption settings, CisSearchModel searchModel);

        /// <summary>
        /// Метод получения статуса задания на выгрузку по ID выгрузки (8.2).
        /// </summary>
        /// <param name="settings">Настройки провайдера.</param>
        /// <param name="taskId">Идентификатор формируемой выгрузки (идентификатор, который вернулся в ответе на запрос о формировании выгрузки.)</param>
        /// <param name="productGroup">Кодовое значение товарной группы.
        /// Указанное значение должно совпадать с "productGroupCode", сформированных ранее заданий.</param>
        /// <returns>Текущий статус задания на выгрузку + информация, необходимая пользователю для дальнейшей работы.</returns>
        DispenserTaskStatusModel GetDispenserTaskStatus(TrueApiProviderOption settings, string taskId, ProductGroup productGroup)
            => GetDispenserTaskStatusAsync(settings, taskId, productGroup).GetAwaiter().GetResult();

        /// <summary>
        /// Метод получения статуса задания на выгрузку по ID выгрузки (8.2).
        /// </summary>
        /// <param name="settings">Настройки провайдера.</param>
        /// <param name="taskId">Идентификатор формируемой выгрузки (идентификатор, который вернулся в ответе на запрос о формировании выгрузки.)</param>
        /// <param name="productGroup">Кодовое значение товарной группы.
        /// Указанное значение должно совпадать с "productGroupCode", сформированных ранее заданий.</param>
        /// <returns>Текущий статус задания на выгрузку + информация, необходимая пользователю для дальнейшей работы.</returns>
        Task<DispenserTaskStatusModel> GetDispenserTaskStatusAsync(TrueApiProviderOption settings, string taskId, ProductGroup productGroup);

        /// <summary>
        /// Метод получения CSV по списку КИ участника оборота товаров по заданному фильтру (8.5.3).
        /// </summary>
        /// <param name="settings">Настройки провайдера.</param>
        /// <param name="taskId">Идентификатор формируемой выгрузки (идентификатор, который вернулся в ответе на запрос о формировании выгрузки.)</param>
        /// <param name="productGroup">Кодовое значение товарной группы.
        /// Указанное значение должно совпадать с "productGroupCode", сформированных ранее заданий.</param>
        /// <returns>Файл в формате CSV со сведениями о КИ, которые находятся на балансе у участника оборота товаров.</returns>
        string GetCisListByFilter(TrueApiProviderOption settings, string taskId, ProductGroup productGroup)
            => GetCisListByFilterAsync(settings, taskId, productGroup).GetAwaiter().GetResult();

        /// <summary>
        /// Метод получения CSV по списку КИ участника оборота товаров по заданному фильтру (8.5.3).
        /// </summary>
        /// <param name="settings">Настройки провайдера.</param>
        /// <param name="taskId">Идентификатор формируемой выгрузки (идентификатор, который вернулся в ответе на запрос о формировании выгрузки.)</param>
        /// <param name="productGroup">Кодовое значение товарной группы.
        /// Указанное значение должно совпадать с "productGroupCode", сформированных ранее заданий.</param>
        /// <returns>Файл в формате CSV со сведениями о КИ, которые находятся на балансе у участника оборота товаров.</returns>
        Task<string> GetCisListByFilterAsync(TrueApiProviderOption settings, string taskId, ProductGroup productGroup);

        /// <summary>
        /// Метод получения квитанций результата обработки универсального документа по идентификатору документа.
        /// </summary>
        /// <param name="settings">Настройки провайдера.</param>
        /// <param name="fileId">Полное имя из "ИдФайл" XML направленного документа</param>
        /// <returns>Квитанция по результатам обработки документов ЭДО содержит перечень из первых 10 ошибок по документу.</returns>
        ProcessingResultModel GetProcessingResult(TrueApiProviderOption settings, string fileId)
            => GetProcessingResultAsync(settings, fileId).GetAwaiter().GetResult();

        /// <summary>
        /// Метод получения квитанций результата обработки универсального документа по идентификатору документа.
        /// </summary>
        /// <param name="settings">Настройки провайдера.</param>
        /// <param name="fileId">Полное имя из "ИдФайл" XML направленного документа</param>
        /// <returns>Квитанция по результатам обработки документов ЭДО содержит перечень из первых 10 ошибок по документу.</returns>
        Task<ProcessingResultModel> GetProcessingResultAsync(TrueApiProviderOption settings, string fileId);
    }
}