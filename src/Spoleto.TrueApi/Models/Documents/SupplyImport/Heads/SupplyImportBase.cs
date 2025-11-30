using System.Text.Json.Serialization;

namespace Spoleto.TrueApi.Documents
{
    /// <summary>
    /// Базовый класс документа ввода в оборот
    /// </summary>
    public abstract class SupplyImportBase<T> : ITrueApiDocument where T : SupplyImportItemBase
    {
        /// <summary>
        /// Тип документа
        /// </summary>
        public abstract DocumentType DocumentType { get; }

        /// <summary>
        /// ИНН участника, осуществившего эмиссию КИ
        /// </summary>
        [JsonPropertyName("trade_participant_inn")]
        public string TradeParticipantInn { get; set; }

        /// <summary>
        /// Список товаров на ввод в оборот
        /// </summary>
        [JsonPropertyName("products_list")]
        public List<T> ProductsList { get; set; }
    }
}
