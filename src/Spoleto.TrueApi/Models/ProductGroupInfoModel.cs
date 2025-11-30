using System.Text;
using System.Text.Json.Serialization;

namespace Spoleto.TrueApi
{
    /// <summary>
    /// Информация о товарных группах
    /// </summary>
    public class ProductGroupInfoModel
    {
        /// <summary>
        /// Код товара
        /// </summary>
        [JsonPropertyName("data")]
        public string Data { get; set; }

        /// <summary>
        /// Идентификатор товарной группы
        /// </summary>
        /// <remarks>
        /// Параметр указывается в случае успешного выполнения запроса
        /// </remarks>
        [JsonPropertyName("tg-id")]
        public string TgId { get; set; }

        /// <summary>
        /// Наименование товарной группы
        /// </summary>
        [JsonPropertyName("tg-name")]
        public string TgName { get; set; }

        /// <summary>
        /// Сообщение об ошибке при обработке кода товара
        /// </summary>
        /// <remarks>
        /// Параметр указывается при наличии ошибки в обработке кода товара при успешном выполнении запроса
        /// </remarks>
        [JsonPropertyName("error-msg")]
        public string ErrorMsg { get; set; }

        /// <summary>
        /// Код ошибки при обработке кода товара
        /// </summary>
        [JsonPropertyName("error-code")]
        public string ErrorCode { get; set; }

        public override string ToString()
        {
            var sB = new StringBuilder($"{Data}");
            if (TgName != null)
                sB.Append($"; TgName = {TgName}");

            if (ErrorMsg != null)
                sB.Append($"; ErrorMsg = {ErrorMsg}");

            return sB.ToString();
        }
    }
}
