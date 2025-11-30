using System.Text.Json.Serialization;

namespace Spoleto.TrueApi
{
    /// <summary>
    /// Контейнер для различных сущностей (атрибутов, брендов, категорий), в котором результат - это один объект.
    /// </summary>
    public class NkContainerModel<T> where T : INkObject
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
        public T Result { get; set; }

        public override string ToString() => $"Result = {Result}";
    }
}
