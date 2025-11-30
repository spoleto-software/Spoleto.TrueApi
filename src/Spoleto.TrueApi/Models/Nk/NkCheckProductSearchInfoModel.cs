using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Spoleto.TrueApi
{
    /// <summary>
    /// Информация для определения принадлежности товара с указанными кодом товара или кодами ТНВЭД к маркируемым товарным группам
    /// </summary>
    public class NkCheckProductSearchInfoModel
    {
        /// <summary>
        /// Список кодов товаров
        /// </summary>
        [JsonPropertyName("gtins")]
        [Required]
        public List<string> Gtins { get; set; }

        /// <summary>
        /// Список кодов товаров
        /// </summary>
        [JsonPropertyName("tnveds")]
        [Required]
        public List<string> Tnveds { get; set; }
    }
}
