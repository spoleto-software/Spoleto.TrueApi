using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Spoleto.TrueApi
{
    /// <summary>
    /// Контейнер со списком товаров с подробной информацией, которые доступны в данный момент времени участнику оборота товаров
    /// </summary>
    public class ProductFullInfoContainerModel
    {
        /// <summary>
        /// Массив с информацией о товарах.
        /// </summary>
        [JsonPropertyName("cisInfo")]
        [Required]
        public ProductFullInfoModel Result { get; set; }

        //public override string ToString() => $"Results.Count = {(Result.Count ?? 0)};";
    }
}
