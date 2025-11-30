using System.Text.Json.Serialization;

namespace Spoleto.TrueApi
{
    /// <summary>
    /// Контейнер для различных сущностей (атрибутов, брендов, категорий), в котором результат - это список объектов.
    /// </summary>
    public class NkContainerListModel<T> where T : INkObject
    {
        /// <summary>
        /// Номер версии API метода
        /// </summary>
        [JsonPropertyName("apiversion")]
        public decimal ApiVersion { get; set; }

        /// <summary>
        /// Результат
        /// </summary>
        /// <remarks>
        /// При private наличии ответа
        /// </remarks>
        [JsonPropertyName("result")]
        public List<T> Result { get; set; }
    }
}
