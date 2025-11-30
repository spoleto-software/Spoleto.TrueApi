using System.Text.Json.Serialization;

namespace Spoleto.TrueApi
{
    public class DatePeriodModel
    {
        /// <summary>
        /// Дата начала периода.
        /// </summary>
        [JsonPropertyName("start")]
        public DateTime? Start { get; set; }

        /// <summary>
        /// Дата окончания периода.
        /// </summary>
        [JsonPropertyName("end")]
        public DateTime? End { get; set; }

        public override string ToString() => $"{Start} -  {End}";

    }
}