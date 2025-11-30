namespace Spoleto.TrueApi
{
    /// <summary>
    /// Провайдер для работы с методами Национального каталога.
    /// </summary>
    public interface ITrueApiNkProvider
    {
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
        NkContainerListModel<NkAttributeModel> GetAttributes(TrueApiNkProviderOption settings, NkAttributeType attrType, string catId = null, string tnved = null)
            => GetAttributesAsync(settings, attrType, catId, tnved).GetAwaiter().GetResult();

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
        Task<NkContainerListModel<NkAttributeModel>> GetAttributesAsync(TrueApiNkProviderOption settings, NkAttributeType attrType, string catId = null, string tnved = null);

        /// <summary>
        /// Метод получения списка торговых марок
        /// </summary>
        /// <param name="settings">Настройки провайдера.</param>
        /// <returns>Список торговых марок.</returns>
        NkContainerListModel<NkBrandModel> GetBrands(TrueApiNkProviderOption settings)
             => GetBrandsAsync(settings).GetAwaiter().GetResult();

        /// <summary>
        /// Метод получения списка торговых марок
        /// </summary>
        /// <param name="settings">Настройки провайдера.</param>
        /// <returns>Список торговых марок.</returns>
        Task<NkContainerListModel<NkBrandModel>> GetBrandsAsync(TrueApiNkProviderOption settings);

        /// <summary>
        /// Метод получения дерева категорий, корень дерева не возвращается
        /// </summary>
        /// <param name="settings">Настройки провайдера.</param>
        /// <returns>Список данных о каждом элементе дерева категорий.</returns>
        NkContainerListModel<NkCategoryModel> GetCategories(TrueApiNkProviderOption settings)
             => GetCategoriesAsync(settings).GetAwaiter().GetResult();

        /// <summary>
        /// Метод получения дерева категорий, корень дерева не возвращается
        /// </summary>
        /// <param name="settings">Настройки провайдера.</param>
        /// <returns>Список данных о каждом элементе дерева категорий.</returns>
        Task<NkContainerListModel<NkCategoryModel>> GetCategoriesAsync(TrueApiNkProviderOption settings);

        /// <summary>
        /// Метод генерации черновиков кодов товаров (GTIN).
        /// </summary>
        /// <param name="settings">Настройки провайдера.</param>
        /// <param name="quantity">Количество новых черновиков кодов товаров, которые нужно сгенерировать.
        /// Обязателен при запросе генерации новых черновиков gtin</param>
        /// <param name="exist">Признак, обозначающий запрос уже сгенерированных и имеющихся в базе пользователя gtin</param>
        /// <param name="supplierKey">Ключ поставщика или производителя товаров</param>
        /// <returns>Список черновиков кодов товаров.</returns>
        NkContainerModel<NkGtinDraftModel> GenerateGtinDrafts(TrueApiNkProviderOption settings, int quantity, bool? exist = null, string supplierKey = null)
            => GenerateGtinDraftsAsync(settings, quantity, exist, supplierKey).GetAwaiter().GetResult();

        /// <summary>
        /// Метод генерации черновиков кодов товаров (GTIN).
        /// </summary>
        /// <param name="settings">Настройки провайдера.</param>
        /// <param name="quantity">Количество новых черновиков кодов товаров, которые нужно сгенерировать.
        /// Обязателен при запросе генерации новых черновиков gtin</param>
        /// <param name="exist">Признак, обозначающий запрос уже сгенерированных и имеющихся в базе пользователя gtin</param>
        /// <param name="supplierKey">Ключ поставщика или производителя товаров</param>
        /// <returns>Список черновиков кодов товаров.</returns>
        Task<NkContainerModel<NkGtinDraftModel>> GenerateGtinDraftsAsync(TrueApiNkProviderOption settings, int quantity, bool? exist = null, string supplierKey = null);

        /// <summary>
        /// Метод создания и обновления товаров
        /// </summary>
        /// <param name="settings">Настройки провайдера.</param>
        /// <param name="feedModel">Пакет обновлений товара.</param>
        /// <param name="supplierKey">Ключ поставщика или производителя товаров.</param>
        /// <returns>ID фида созданных/обновлённых товаров либо сообщение об ошибке (при наличии).</returns>
        NkContainerModel<NkFeedResultModel> CreateFeed(TrueApiNkProviderOption settings, NkFeedModel feedModel, string supplierKey = null)
            => CreateFeedAsync(settings, feedModel, supplierKey).GetAwaiter().GetResult();

        /// <summary>
        /// Метод создания и обновления товаров
        /// </summary>
        /// <param name="settings">Настройки провайдера.</param>
        /// <param name="feedModel">Пакет обновлений товара.</param>
        /// <param name="supplierKey">Ключ поставщика или производителя товаров.</param>
        /// <returns>ID фида созданных/обновлённых товаров либо сообщение об ошибке (при наличии).</returns>
        Task<NkContainerModel<NkFeedResultModel>> CreateFeedAsync(TrueApiNkProviderOption settings, NkFeedModel feedModel, string supplierKey = null);

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
        NkContainerModel<NkFeedStatusModel> GetFeedStatus(TrueApiNkProviderOption settings, string feedId, string supplierKey = null)
            => GetFeedStatusAsync(settings, feedId, supplierKey).GetAwaiter().GetResult();

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
        Task<NkContainerModel<NkFeedStatusModel>> GetFeedStatusAsync(TrueApiNkProviderOption settings, string feedId, string supplierKey = null);

        /// <summary>
        /// Метод отправки на модерацию карточки товаров в статусе "Черновик"
        /// </summary>
        /// <param name="settings">Настройки провайдера.</param>
        /// <param name="good_id">ID товара в каталоге.</param>
        /// <returns>Информация об отправке на модерацию.</returns>
        NkContainerModel<NkModerationModel> SendToModeration(TrueApiNkProviderOption settings, string good_id)
            => SendToModerationAsync(settings, good_id).GetAwaiter().GetResult();

        /// <summary>
        /// Метод отправки на модерацию карточки товаров в статусе "Черновик"
        /// </summary>
        /// <param name="settings">Настройки провайдера.</param>
        /// <param name="goodId">ID товара в каталоге.</param>
        /// <returns>Информация об отправке на модерацию.</returns>
        Task<NkContainerModel<NkModerationModel>> SendToModerationAsync(TrueApiNkProviderOption settings, string goodId);

        /// <summary>
        /// Метод отправки на модерацию карточки товаров в статусе "Черновик"
        /// </summary>
        /// <param name="settings">Настройки провайдера.</param>
        /// <param name="gtin">Код товара.</param>
        /// <param name="inn">ИНН аккаунта.</param>
        /// <returns>Информация об отправке на модерацию.</returns>
        NkContainerModel<NkModerationModel> SendToModeration(TrueApiNkProviderOption settings, string gtin, string inn)
            => SendToModerationAsync(settings, gtin, inn).GetAwaiter().GetResult();

        /// <summary>
        /// Метод отправки на модерацию карточки товаров в статусе "Черновик"
        /// </summary>
        /// <param name="settings">Настройки провайдера.</param>
        /// <param name="gtin">Код товара.</param>
        /// <param name="inn">ИНН аккаунта.</param>
        /// <returns>Информация об отправке на модерацию.</returns>
        Task<NkContainerModel<NkModerationModel>> SendToModerationAsync(TrueApiNkProviderOption settings, string gtin, string inn);

        /// <summary>
        /// Метод получения полной информации о товаре.
        /// </summary>
        /// <param name="settings">Настройки провайдера.</param>
        /// <param name="searchInfoModel">Информация для поиска полной информации о товаре.</param>
        /// <returns>Полная информация о товаре.</returns>
        NkContainerListModel<NkProductFullInfoModel> GetProductFullInfo(TrueApiNkProviderOption settings, NkProducSearchInfoModel searchInfoModel)
            => GetProductFullInfoAsync(settings, searchInfoModel).GetAwaiter().GetResult();

        /// <summary>
        /// Метод получения полной информации о товаре.
        /// </summary>
        /// <param name="settings">Настройки провайдера.</param>
        /// <param name="searchInfoModel">Информация для поиска полной информации о товаре.</param>
        /// <returns>Полная информация о товаре.</returns>
        Task<NkContainerListModel<NkProductFullInfoModel>> GetProductFullInfoAsync(TrueApiNkProviderOption settings, NkProducSearchInfoModel searchInfoModel);

        /// <summary>
        /// Метод получения карточки товара, в том числе неопубликованной карточки.
        /// </summary>
        /// <remarks>
        /// Метод возвращает всю имеющуюся информацию о продукте, то есть все заполненные атрибуты карточки товара независимо от статуса самой карточки,
        /// в отличие от метода product <see cref="GetProductFullInfo"/>, который возвращает информацию только по опубликованным карточкам.
        /// </remarks>
        /// <param name="settings">Настройки провайдера.</param>
        /// <param name="searchInfoModel">Информация для поиска полной информации о товаре.</param>
        /// <returns>Карточки товара, в том числе неопубликованные карточки.</returns>
        NkContainerListModel<NkFeedProductFullInfoModel> GetFeedProductFullInfo(TrueApiNkProviderOption settings, NkFeedProducSearchInfoModel searchInfoModel)
            => GetFeedProductFullInfoAsync(settings, searchInfoModel).GetAwaiter().GetResult();

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
        Task<NkContainerListModel<NkFeedProductFullInfoModel>> GetFeedProductFullInfoAsync(TrueApiNkProviderOption settings, NkFeedProducSearchInfoModel searchInfoModel);

        /// <summary>
        /// Метод определения кода принадлежности товара к маркируемым товарным группам.
        /// </summary>
        /// <remarks>
        /// В одном запросе максимум может быть указано 100 кодов товара и кодов ТНВЭД суммарно
        /// </remarks>
        /// <param name="settings">Настройки провайдера.</param>
        /// <param name="searchInfoModel">Информация с кодами товара и кодами ТНВЭД.</param>
        /// <returns>Информация о принадлежности товара с указанными кодом товара или кодами ТНВЭД к маркируемым товарным группам.</returns>
        NkContainerListModel<NkCheckProductInfoModel> CheckProductForMarking(TrueApiNkProviderOption settings, NkCheckProductSearchInfoModel searchInfoModel)
            => CheckProductForMarkingAsync(settings, searchInfoModel).GetAwaiter().GetResult();

        /// <summary>
        /// Метод определения кода принадлежности товара к маркируемым товарным группам.
        /// </summary>
        /// <remarks>
        /// В одном запросе максимум может быть указано 100 кодов товара и кодов ТНВЭД суммарно
        /// </remarks>
        /// <param name="settings">Настройки провайдера.</param>
        /// <param name="searchInfoModel">Информация с кодами товара и кодами ТНВЭД.</param>
        /// <returns>Информация о принадлежности товара с указанными кодом товара или кодами ТНВЭД к маркируемым товарным группам.</returns>
        Task<NkContainerListModel<NkCheckProductInfoModel>> CheckProductForMarkingAsync(TrueApiNkProviderOption settings, NkCheckProductSearchInfoModel searchInfoModel);

        /// <summary>
        /// Метод «Получить XML для последующей подписи карточки» (3.4.1)
        /// </summary>
        /// <remarks>
        /// Можно передавать больше максимального количества (25).<br/>
        /// Внутри метода будет разбивка по 25 товарных позиций максимум для запроса.
        /// </remarks>
        /// <param name="settings">Настройки провайдера.</param>
        /// <param name="searchModel">Фильтр для поиска карточек.</param>
        /// <returns>Карточки товаров с XML.</returns>
        NkContainerModel<NkFeedProductDocumentModel> GetFeedProductDocumentList(TrueApiNkProviderOption settings, NkFeedProductDocumentSearchModel searchModel)
            => GetFeedProductDocumentListAsync(settings, searchModel).GetAwaiter().GetResult();

        /// <summary>
        /// Метод «Получить XML для последующей подписи карточки» (3.4.1)
        /// </summary>
        /// <remarks>
        /// Можно передавать больше максимального количества (25).<br/>
        /// Внутри метода будет разбивка по 25 товарных позиций максимум для запроса.
        /// </remarks>
        /// <param name="settings">Настройки провайдера.</param>
        /// <param name="searchModel">Фильтр для поиска карточек.</param>
        /// <returns>Карточки товаров с XML.</returns>
        Task<NkContainerModel<NkFeedProductDocumentModel>> GetFeedProductDocumentListAsync(TrueApiNkProviderOption settings, NkFeedProductDocumentSearchModel searchModel);

        /// <summary>
        /// Метод «Подписать карточку с использованием открепленной подписи» (3.4.3)
        /// </summary>
        /// <remarks>
        /// Можно передавать больше максимального количества (25).<br/>
        /// Внутри метода будет разбивка по 25 товарных позиций максимум для запроса.
        /// </remarks>
        /// <param name="settings">Настройки провайдера.</param>
        /// <param name="requestModel">Параметры запроса для подписи карточек товара.</param>
        /// <returns>Информация о подписании карточек товара.</returns>
        NkContainerModel<NkFeedProductSignModel> SignFeedProductDocumentList(TrueApiNkProviderOption settings, List<NkFeedProductSignRequestModel> requestModel)
            => SignFeedProductDocumentListAsync(settings, requestModel).GetAwaiter().GetResult();

        /// <summary>
        /// Метод «Подписать карточку с использованием открепленной подписи» (3.4.3)
        /// </summary>
        /// <remarks>
        /// Можно передавать больше максимального количества (25).<br/>
        /// Внутри метода будет разбивка по 25 товарных позиций максимум для запроса.
        /// </remarks>
        /// <param name="settings">Настройки провайдера.</param>
        /// <param name="requestModel">Параметры запроса для подписи карточек товара.</param>
        /// <returns>Информация о подписании карточек товара.</returns>
        Task<NkContainerModel<NkFeedProductSignModel>> SignFeedProductDocumentListAsync(TrueApiNkProviderOption settings, List<NkFeedProductSignRequestModel> requestModel);

    }
}