using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Spoleto.TrueApi
{
    /// <summary>
    /// Информация для поиска заказа на создание кодов маркировки (идентификации).
    /// </summary>
    public class CisOrderSearchInfoModel
    {
        /// <summary>
        /// Код товара
        /// </summary>
        /// <remarks>
        /// Выбор через "c" like 'gtin%' для упаковки типа "единица товара" и через "c" like '(01)gtin(21)%' для упаковки 1-го уровня
        /// </remarks>
        [JsonPropertyName("gtin")]
        [Required]
        public string Gtin { get; set; }

        /// <summary>
        /// Код типа упаковки
        /// </summary>
        [JsonPropertyName("packageType")]
        public PackType PackageType { get; set; }

        /// <summary>
        /// ИНН целевого участника
        /// </summary>
        [JsonPropertyName("targetParticipantInn")]
        public string TargetParticipantInn { get; set; }

        /// <summary>
        /// Дата ввода в оборот (для ТГ "Табачная продукция" Дата нанесения)
        /// </summary>
        /// <remarks>
        /// Задается в формате yyyy-MM-ddTHH:mm:ss.SSS’Z
        /// </remarks>
        [JsonConverter(typeof(JavaScriptDateTimeJsonConverter))]
        [JsonPropertyName("producedDate")]
        public DateTime? ProducedDate { get; set; }

        public override string ToString() => Gtin;
    }
}
