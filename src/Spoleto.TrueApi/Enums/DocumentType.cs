namespace Spoleto.TrueApi
{
    /// <summary>
    /// Типы документов.
    /// </summary>
    public enum DocumentType
    {
        /// <summary>
        /// Агрегация. JSON
        /// </summary>
        AGGREGATION_DOCUMENT,

        /// <summary>
        /// Агрегация. CSV
        /// </summary>
        AGGREGATION_DOCUMENT_CSV,

        /// <summary>
        /// Агрегация. XML
        /// </summary>
        AGGREGATION_DOCUMENT_XML,

        /// <summary>
        /// Формирование наборов. JSON
        /// </summary>
        SETS_AGGREGATION,

        /// <summary>
        /// Формирование наборов. CSV
        /// </summary>
        SETS_AGGREGATION_CSV,

        /// <summary>
        /// Формирование наборов. XML
        /// </summary>
        SETS_AGGREGATION_XML,

        /// <summary>
        /// Расформирование агрегата. JSON
        /// </summary>
        DISAGGREGATION_DOCUMENT,

        /// <summary>
        /// Расформирование агрегата. CSV
        /// </summary>
        DISAGGREGATION_DOCUMENT_CSV,

        /// <summary>
        /// Расформирование агрегата. XML
        /// </summary>
        DISAGGREGATION_DOCUMENT_XML,

        /// <summary>
        /// Трансформация агрегата. JSON
        /// </summary>
        REAGGREGATION_DOCUMENT,

        /// <summary>
        /// Трансформация агрегата. XML
        /// </summary>
        REAGGREGATION_DOCUMENT_XML,

        /// <summary>
        /// Трансформация агрегата. CSV
        /// </summary>
        REAGGREGATION_DOCUMENT_CSV,

        /// <summary>
        /// Ввод в оборот. Производство РФ. JSON
        /// </summary>
        LP_INTRODUCE_GOODS,

        /// <summary>
        /// Ввод в оборот. Производство РФ. CSV
        /// </summary>
        LP_INTRODUCE_GOODS_CSV,

        /// <summary>
        /// Ввод в оборот. Производство РФ. XML
        /// </summary>
        LP_INTRODUCE_GOODS_XML,

        /// <summary>
        /// Отгрузка. JSON
        /// </summary>
        LP_SHIP_GOODS,

        /// <summary>
        /// Отгрузка. CSV
        /// </summary>
        LP_SHIP_GOODS_CSV,

        /// <summary>
        /// Отгрузка. XML
        /// </summary>
        LP_SHIP_GOODS_XML,

        /// <summary>
        /// Отгрузка с выводом из оборота. JSON (MANUAL)
        /// </summary>
        LP_SHIP_RECEIPT,

        /// <summary>
        /// Отгрузка с выводом из оборота. CSV
        /// </summary>
        LP_SHIP_RECEIPT_CSV,

        /// <summary>
        /// Отгрузка с выводом из оборота. XML
        /// </summary>
        LP_SHIP_RECEIPT_XML,

        /// <summary>
        /// Отгрузка при трансграничной торговле. JSON (MANUAL)
        /// </summary>
        LP_SHIP_GOODS_CROSSBORDER,

        /// <summary>
        /// Приемка. JSON
        /// </summary>
        LP_ACCEPT_GOODS,

        /// <summary>
        /// Приемка. XML
        /// </summary>
        LP_ACCEPT_GOODS_XML,

        /// <summary>
        /// Перемаркировка. JSON
        /// </summary>
        LK_REMARK,

        /// <summary>
        /// Перемаркировка. CSV
        /// </summary>
        LK_REMARK_CSV,

        /// <summary>
        /// Перемаркировка. XML
        /// </summary>
        LK_REMARK_XML,

        /// <summary>
        /// Ввод в оборот. Импорт. JSON
        /// </summary>
        LP_GOODS_IMPORT,

        /// <summary>
        /// Ввод в оборот. Импорт. CSV
        /// </summary>
        LP_GOODS_IMPORT_CSV,

        /// <summary>
        /// Ввод в оборот. Импорт. XML
        /// </summary>
        LP_GOODS_IMPORT_XML,

        /// <summary>
        /// Отмена отгрузки. JSON
        /// </summary>
        LP_CANCEL_SHIPMENT,

        /// <summary>
        /// Отмена отгрузки при трансграничной торговле. JSON (MANUAL)
        /// </summary>
        LP_CANCEL_SHIPMENT_CROSSBORDER,

        /// <summary>
        /// Списание ненанесённых КИ. JSON
        /// </summary>
        LK_KM_CANCELLATION,

        /// <summary>
        /// Списание ненанесённых КИ. XML
        /// </summary>
        LK_KM_CANCELLATION_XML,

        /// <summary>
        /// Списание ненанесённых КИ. CSV
        /// </summary>
        LK_KM_CANCELLATION_CSV,

        /// <summary>
        /// Списание нанесённых КИ. JSON
        /// </summary>
        LK_APPLIED_KM_CANCELLATION,

        /// <summary>
        /// Списание нанесённых КИ. XML
        /// </summary>
        LK_APPLIED_KM_CANCELLATION_XML,

        /// <summary>
        /// Списание нанесённых КИ. CSV
        /// </summary>
        LK_APPLIED_KM_CANCELLATION_CSV,

        /// <summary>
        /// Ввод в оборот товара. Контрактное производство РФ. JSON
        /// </summary>
        LK_CONTRACT_COMMISSIONING,

        /// <summary>
        /// Ввод в оборот. Контрактное производство РФ. CSV
        /// </summary>
        LK_CONTRACT_COMMISSIONING_CSV,

        /// <summary>
        /// Ввод в оборот. Контрактное производство РФ. XML
        /// </summary>
        LK_CONTRACT_COMMISSIONING_XML,

        /// <summary>
        /// Ввод в оборот товара. Полученных от физических лиц. JSON
        /// </summary>
        LK_INDI_COMMISSIONING,

        /// <summary>
        /// Ввод в оборот. Полученных от физических лиц. CSV
        /// </summary>
        LK_INDI_COMMISSIONING_CSV,

        /// <summary>
        /// Ввод в оборот. Полученных от физических лиц. XML
        /// </summary>
        LK_INDI_COMMISSIONING_XML,

        /// <summary>
        /// Возврат в оборот. JSON (MANUAL)
        /// </summary>
        LP_RETURN,

        /// <summary>
        /// Возврат в оборот. CSV
        /// </summary>
        LP_RETURN_CSV,

        /// <summary>
        /// Возврат в оборот. XML
        /// </summary>
        LP_RETURN_XML,

        /// <summary>
        /// Описание остатков товара. JSON
        /// </summary>
        OST_DESCRIPTION,

        /// <summary>
        /// Описание остатков товара. CSV
        /// </summary>
        OST_DESCRIPTION_CSV,

        /// <summary>
        /// Описание остатков товара. XML
        /// </summary>
        OST_DESCRIPTION_XML,

        /// <summary>
        /// Ввод в оборот. Маркировка остатков. JSON
        /// </summary>
        LP_INTRODUCE_OST,

        /// <summary>
        /// Ввод в оборот. Маркировка остатков. CSV
        /// </summary>
        LP_INTRODUCE_OST_CSV,

        /// <summary>
        /// Ввод в оборот. Маркировка остатков. XML
        /// </summary>
        LP_INTRODUCE_OST_XML,

        /// <summary>
        /// Ввод в оборот. Трансграничная торговля (Одежда и обувь). JSON
        /// </summary>
        CROSSBORDER,

        /// <summary>
        /// Ввод в оборот. Трансграничная торговля (Мех). JSON
        /// </summary>
        FURS_CROSSBORDER,

        /// <summary>
        /// Ввод в оборот. Трансграничная торговля. CSV
        /// </summary>
        CROSSBORDER_CSV,

        /// <summary>
        /// Ввод в оборот. Трансграничная торговля. XML
        /// </summary>
        CROSSBORDER_XML,

        /// <summary>
        /// Вывод из оборота. JSON
        /// </summary>
        LK_RECEIPT,

        /// <summary>
        /// Вывод из оборота. CSV
        /// </summary>
        LK_RECEIPT_CSV,

        /// <summary>
        /// Вывод из оборота. XML
        /// </summary>
        LK_RECEIPT_XML,

        /// <summary>
        /// Ввод в оборот. На территории стран ЕАЭС (контрактное производство). JSON (MANUAL)
        /// </summary>
        LP_INTRODUCE_GOODS_CROSSBORDER_CSD_JSON,

        /// <summary>
        /// Ввод в оборот. На территории стран ЕАЭС (контрактное производство). XML
        /// </summary>
        LP_INTRODUCE_GOODS_CROSSBORDER_CSD_XML,

        /// <summary>
        /// Ввод в оборот. На территории стран ЕАЭС (контрактное производство). CSV
        /// </summary>
        LP_INTRODUCE_GOODS_CROSSBORDER_CSD_CSV,

        /// <summary>
        /// Ввод в оборот. Импорт с ФТС (Одежда и обувь). JSON (MANUAL)
        /// </summary>
        LP_FTS_INTRODUCE,

        /// <summary>
        /// Ввод в оборот. Импорт с ФТС (Мех). JSON
        /// </summary>
        FURS_FTS_INTRODUCE,

        /// <summary>
        /// Ввод в оборот. Импорт с ФТС. XML
        /// </summary>
        LP_FTS_INTRODUCE_XML,

        /// <summary>
        /// Ввод в оборот. Импорт с ФТС. CSV
        /// </summary>
        LP_FTS_INTRODUCE_CSV,

        /// <summary>
        /// Формирование АТК. JSON (MANUAL)
        /// </summary>
        ATK_AGGREGATION,

        /// <summary>
        /// Формирование АТК. CSV
        /// </summary>
        ATK_AGGREGATION_CSV,

        /// <summary>
        /// Формирование АТК. XML
        /// </summary>
        ATK_AGGREGATION_XML,

        /// <summary>
        /// Трансформация АТК. JSON (MANUAL)
        /// </summary>
        ATK_TRANSFORMATION,

        /// <summary>
        /// Трансформация АТК. CSV
        /// </summary>
        ATK_TRANSFORMATION_CSV,

        /// <summary>
        /// Трансформация АТК. XML
        /// </summary>
        ATK_TRANSFORMATION_XML,

        /// <summary>
        /// Расформирование АТК. JSON (MANUAL)
        /// </summary>
        ATK_DISAGGREGATION,

        /// <summary>
        /// Расформирование АТК. CSV
        /// </summary>
        ATK_DISAGGREGATION_CSV,

        /// <summary>
        /// Расформирование АТК. XML
        /// </summary>
        ATK_DISAGGREGATION_XML,

        /// <summary>
        /// Чек
        /// </summary>
        /// <remarks>
        /// Формируется оператором фискальных данных
        /// </remarks>
        RECEIPT,

        /// <summary>
        /// Чек возврата
        /// </summary>
        /// <remarks>
        /// Формируется оператором фискальных данных
        /// </remarks>
        RECEIPT_RETURN
    }
}
