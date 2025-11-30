using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Spoleto.TrueApi
{
    /// <summary>
    /// Информация для поиска десятизначных кодов ТН ВЭД.
    /// </summary>
    public class TnvedSearchModel
    {
        /// <summary>
        /// Список товарных позиций с одного из которых должны начинаться искомые коды ТН ВЭД.
        /// Используется для фильтрации товарной группы.
        /// </summary>
        /// <remarks>
        /// Пример '6401,6402,6403,6404,6405'.
        /// </remarks>
        [JsonPropertyName("prefix")]
        [Required]
        public string Prefix { get; set; }

        /// <summary>
        /// Значение устанавливает количество записей в ответе, не более 10000 записей
        /// </summary>
        [JsonPropertyName("limit")]
        [Required]
        public int Limit { get; set; }

        /// <summary>
        /// Смещение.
        /// </summary>
        /// <remarks>
        /// Значение по умолчанию = 0.
        /// При значении "offset" = 0 метод возвращает корректный ответ.
        /// </remarks>
        [JsonPropertyName("offset")]
        [Required]
        public int Offset { get; set; }

        public override string ToString() => Prefix;
    }
}
