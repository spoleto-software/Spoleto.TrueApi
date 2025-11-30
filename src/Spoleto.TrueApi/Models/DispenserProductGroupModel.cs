using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Spoleto.TrueApi
{
    /// <summary>
    /// Товарные группа.
    /// </summary>
    public class DispenserProductGroupModel
    {
        /// <summary>
        /// Идентификатор текущей задачи.
        /// </summary>
        [JsonPropertyName("id")]
        [Required]
        public string Id { get; set; }

        /// <summary>
        /// Наименование товарной группы.
        /// </summary>
        [JsonPropertyName("name")]
        [Required]
        public string Name { get; set; }

        public override string ToString() => $"{Name} ({Id})";
    }
}