using System.Text.Json.Serialization;

namespace Spoleto.TrueApi.Documents
{
    /// <summary>
    /// Документ трансграничной торговли (Мех)
    /// </summary>
    public class SupplyImportCrossborderFur : SupplyImportCrossborderBase<SupplyImportCrossborderFurItem>
    {
        /// <summary>
        /// Тип документа
        /// </summary>
        [JsonIgnore]
        public override DocumentType DocumentType => DocumentType.FURS_CROSSBORDER;
    }
}
