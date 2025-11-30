namespace Spoleto.TrueApi
{
    /// <summary>
    /// Типы идентификатора
    /// </summary>
    public enum NkIdentifierType
    {
        /// <summary>
        /// Глобальный код товара (штрих-код)
        /// </summary>
        gtin,

        /// <summary>
        /// Штрих-код Barcode (штрихкод с неправильной контрольной цифрой)
        /// </summary>
        barcode,

        /// <summary>
        /// локальный идентификатор товарной позиции (артикул) 
        /// </summary>
        sku,

        /// <summary>
        /// локальный штрих-код LTIN (например весовые штрих-коды) 
        /// </summary>
        ltin
    }
}
