using System.Text.Json.Serialization;

namespace Spoleto.TrueApi.Documents
{
    /// <summary>
    /// Документ ввода в оборот. Импорт с ФТС (Мех)
    /// </summary>
    public class SupplyImportFtsFur : SupplyImportBase<SupplyImportFtsFurItem>
    {
        /// <summary>
        /// Тип документа
        /// </summary>
        [JsonIgnore]
        public override DocumentType DocumentType => DocumentType.FURS_FTS_INTRODUCE;
    }
}
