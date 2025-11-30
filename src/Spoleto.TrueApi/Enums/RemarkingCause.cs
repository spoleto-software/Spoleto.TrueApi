namespace Spoleto.TrueApi
{
    /// <summary>
    /// Причины повторной маркировки
    /// </summary>
    public enum RemarkingCause
    {
        /// <summary>
        /// испорчено либо утеряно СИ с КИ
        /// </summary>
        KM_SPOILED_OR_LOST,

        /// <summary>
        /// выявлены ошибки описания товара
        /// </summary>
        DESCRIPTION_ERRORS,

        /// <summary>
        /// возврат от розничного покупателя
        /// </summary>
        RETAIL_RETURN,

        /// <summary>
        /// возврат в случае дистанционной продажи
        /// </summary>
        REMOTE_SALE_RETURN
    }
}
