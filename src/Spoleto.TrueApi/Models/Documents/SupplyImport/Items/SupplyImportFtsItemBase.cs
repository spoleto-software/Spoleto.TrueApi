using System.Text.Json.Serialization;

namespace Spoleto.TrueApi.Documents
{
    /// <summary>
    /// Базовый класс товара для ввода в оборот. Импорт c ФТС
    /// </summary>
    public abstract class SupplyImportFtsItemBase : SupplyImportItemBase
    {
        /// <summary>
        /// Уникальный идентификатор товара
        /// </summary>
        /// <remarks>
        /// Указывается КИ или КИТУ или АТК
        /// </remarks>
        [JsonPropertyName("cis")]
        public virtual string Cis { get; set; }
    }
}
