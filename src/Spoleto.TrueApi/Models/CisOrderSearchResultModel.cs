using System.Text.Json.Serialization;

namespace Spoleto.TrueApi
{
    /// <summary>
    /// Результат поиска заказа на создание кодов маркировки (идентификации).
    /// </summary>
    public class CisOrderSearchResultModel
    {
        /// <summary>
        /// Идентификатор заказа
        /// </summary>
        /// <remarks>
        /// При наличии заказа в ГИС МТ
        /// </remarks>
        [JsonPropertyName("uuid")]
        public string Uuid { get; set; }

        public override string ToString() => Uuid;
    }
}
