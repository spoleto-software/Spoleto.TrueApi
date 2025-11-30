using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Spoleto.TrueApi
{
    /// <summary>
    /// Результат определения принадлежности товара с указанными кодом товара или кодами ТНВЭД к маркируемым товарным группам
    /// </summary>
    public class NkCheckProductInfoModel : INkObject
    {
        /// <summary>
        /// Массив, содержащий полученные коды товаров и коды ТН ВЭД,
        /// к которым коды товаров привязаны, информацию о маркировке и код ответа
        /// </summary>
        [JsonPropertyName("gtins")]
        [Required]
        public List<NkCheckProductGtinInfoModel> Gtins { get; set; }

        /// <summary>
        /// Массив, содержащий полученные коды ТН ВЭД, информацию о маркировке и код ответа
        /// </summary>
        [JsonPropertyName("tnveds")]
        [Required]
        public List<NkCheckProductTnvedInfoModel> Tnveds { get; set; }
    }
}
