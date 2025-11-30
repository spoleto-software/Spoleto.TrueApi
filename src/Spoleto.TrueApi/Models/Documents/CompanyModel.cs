using System.Text.Json.Serialization;

namespace Spoleto.TrueApi.Documents
{
    public class CompanyModel
    {
        /// <summary>
        /// Наименование
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// Адрес
        /// </summary>
        [JsonPropertyName("address")]
        public string Address { get; set; }

        /// <summary>
        /// ИНН
        /// </summary>
        [JsonPropertyName("inn")]
        public string Inn { get; set; }

        /// <summary>
        /// КПП
        /// </summary>
        /// <remarks>
        /// Код причины постановки на учет
        /// </remarks>
        [JsonPropertyName("kpp")]
        public string Kpp { get; set; }
    }
}
