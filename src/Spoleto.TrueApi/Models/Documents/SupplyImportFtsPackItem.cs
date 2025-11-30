using System.Text.Json.Serialization;

namespace Spoleto.TrueApi.Documents
{
    /// <summary>
    /// Товар в упаковке.
    /// </summary>
    public class SupplyImportFtsPackItem
    {
        private PackType? _packType;

        /// <summary>
        /// Уникальный идентификатор товара
        /// </summary>
        /// <remarks>
        /// Указывается КИ или КИТУ или АТК.
        /// Важно: в текущей реализации функционала документа импорта возможно указание только КИ.
        /// </remarks>
        [JsonPropertyName("cis")]
        public string Cis { get; set; }

        /// <summary>
        /// Уникальный идентификатор товара КИ
        /// </summary>
        /// <remarks>
        /// Актуально только, если тело документа было передано в формате XML.
        /// </remarks>
        [JsonPropertyName("ki")]
        public string Ki { get; set; }

        /// <summary>
        /// Уникальный идентификатор товара КИТУ
        /// </summary>
        /// <remarks>
        /// Актуально только, если тело документа было передано в формате XML.
        /// </remarks>
        [JsonPropertyName("kitu")]
        public string Kitu { get; set; }

        /// <summary>
        /// Уникальный идентификатор товара АТК
        /// </summary>
        /// <remarks>
        /// Актуально только, если тело документа было передано в формате XML.
        /// </remarks>
        [JsonPropertyName("atk")]
        public string Atk { get; set; }

        /// <summary>
        /// Тип упаковки
        /// </summary>
        /// <remarks>
        /// UNIT - КИ; LEVEL1-99 - КИТУ; АТК - агрегированный таможенный код
        /// </remarks>
        [JsonPropertyName("packType")]
        public PackType PackType
        {
            get
            {
                if (_packType == null)
                {
                    if (Ki != null)
                        _packType = PackType.UNIT;
                    else if (Atk != null)
                        _packType = PackType.АТК;
                }

                return _packType ?? PackType.UNIT;
            }

            set => _packType = value;
        }

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
        public string ProductSize { get; set; }
    }
}
