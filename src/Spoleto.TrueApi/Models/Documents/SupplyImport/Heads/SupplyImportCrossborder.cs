using System.Text.Json.Serialization;

namespace Spoleto.TrueApi.Documents
{
    /// <summary>
    /// Документ трансграничной торговли (Одежда и обувь)
    /// </summary>
    public class SupplyImportCrossborder : SupplyImportCrossborderBase<SupplyImportCrossborderItem>
    {
        /// <summary>
        /// Тип документа
        /// </summary>
        [JsonIgnore]
        public override DocumentType DocumentType => DocumentType.CROSSBORDER;
    }
}
