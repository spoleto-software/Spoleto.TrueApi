using System.Text.Json.Serialization;

namespace Spoleto.TrueApi
{
    /// <summary>
    /// Фиды - пакеты обновлений.
    /// </summary>
    /// <remarks>
    /// Используются для создания и обновленния карточек товаров.
    /// </remarks>
    public class NkFeedModel
    {
        /// <summary>
        /// Идентификатор entry, который лаборатория может задать для более конкретной идентификации ответа
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>
        /// Идентификатор товара
        /// </summary>
        /// <remarks>
        /// Обязательно при обновлении товара
        /// </remarks>
        [JsonPropertyName("good_id")]
        public int? GoodId { get; set; }

        /// <summary>
        /// Идентификатор в Национальном каталоге товаров, код товара
        /// </summary>
        /// <remarks>
        /// Обязательно для нового товара
        /// </remarks>
        [JsonPropertyName("gtin")]
        public string Gtin { get; set; }

        /// <summary>
        /// Признак создания карточки товара с техническим кодом товара
        /// </summary>
        /// <remarks>
        /// Принимает значения 1/0 (true/false).
        /// Обязателен при создании карточки товара с техническим кодом товара, в этом случае код товара в энтри не указывается.
        /// </remarks>
        [JsonPropertyName("is_tech_gtin")]
        public bool? IsTechGtin { get; set; }

        /// <summary>
        /// Признак создания карточки товара с техническим кодом товара
        /// </summary>
        /// <remarks>
        /// Принимает значения 1/0 (true/false).
        /// Обязателен при создании карточки товара с техническим кодом товара, в этом случае код товара в энтри не указывается.
        /// </remarks>
        [JsonPropertyName("is_kit")]
        public bool? IsKit { get; set; }

        /// <summary>
        /// Наименование товара
        /// </summary>
        /// <remarks>
        /// Обязательно при создании товара
        /// </remarks>
        [JsonPropertyName("good_name")]
        public string GoodName { get; set; }

        /// <summary>
        /// ТН ВЭД
        /// </summary>
        /// <remarks>
        /// Обязательно для нового товара
        /// </remarks>
        [JsonPropertyName("tnved")]
        public string Tnved { get; set; }

        /// <summary>
        /// Торговая марка товара
        /// </summary>
        /// <remarks>
        /// Обязательно для нового товара.
        /// </remarks>
        [JsonPropertyName("brand")]
        public string Brand { get; set; }

        /// <summary>
        /// Признак того, что товар надо отправлять на модерацию.
        /// </summary>
        /// <remarks>
        /// Возможные значения:
        /// true - товар отправляется на модерацию, карточка создается в статусе «На модерации»;
        /// false - товар на модерацию не отправляется, карточка создается в статусе «Черновик».
        /// </remarks>
        [JsonPropertyName("moderation")]
        public bool? Moderation { get; set; }

        /// <summary>
        /// Массив идентификаторов
        /// </summary>
        /// <remarks>
        /// Если создается карточка товара с кодом товара, относящимся к упаковке типа trade-unit,
        /// то массив идентификаторов обязательно должен включать, как минимум, идентификатор данного вида упаковки.     
        /// </remarks>
        [JsonPropertyName("identified_by")]
        public List<NkIdentifierCreatingModel> IdentifiedBy { get; set; }

        /// <summary>
        /// Массив идентификаторов категорий
        /// </summary>
        [JsonPropertyName("categories")]
        public List<int> Categories { get; set; }

        /// <summary>
        /// Массив атрибутов
        /// </summary>
        [JsonPropertyName("good_attrs")]
        public List<NkFeedAttributeModel> GoodAttributes { get; set; }

        /// <summary>
        /// Массив изображений
        /// </summary>
        [JsonPropertyName("good_images")]
        public List<NkImageCreatingModel> GoodImages { get; set; }

        public override string ToString() => GoodName;
    }
}
