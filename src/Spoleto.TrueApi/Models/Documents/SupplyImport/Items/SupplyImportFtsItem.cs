using System.Text.Json.Serialization;

namespace Spoleto.TrueApi.Documents
{
    /// <summary>
    /// Товар для ввода в оборот. Импорт c ФТС (Одежда и обувь)
    /// </summary>
    public class SupplyImportFtsItem : SupplyImportFtsItemBase
    {
        private string _productSize;

        /// <summary>
        /// Цвет
        /// </summary>
        /// <remarks>
        /// Указывается значение цвета, содержащее от 1 до 1024 буквенных символа, на английском или русском языке.
        /// Поле не обязательное для заполнения. Может присутствовать только в документах по ТГ "Обувные товары"
        /// </remarks>
        [JsonPropertyName("color")]
        public string Color { get; set; }

        /// <summary>
        /// Штихмассовый размер обуви по ГОСТ 11373-88
        /// </summary>
        /// <remarks>
        /// Возможно указание одного размера, указание диапазона недоступно.
        /// Доступно указание размера с десятичной точкой. Число кратное 0.5 в числовом диапазоне 14.5 - 47.
        /// Поле не обязательное для заполнения. Может присутствовать только в документах по ТГ "Обувные товары"
        /// </remarks>
        [JsonPropertyName("productSize")]
        public string ProductSize { get => _productSize; set => _productSize = value.Trim(); }

        /// <summary>
        /// Товар в упаковке
        /// </summary>
        [JsonPropertyName("children")]
        [JsonIgnore]
        public List<SupplyImportFtsPackItem> Children { get; set; }
    }
}
