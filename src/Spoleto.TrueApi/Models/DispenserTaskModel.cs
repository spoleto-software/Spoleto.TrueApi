using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Spoleto.TrueApi
{
    /// <summary>
    /// Ответ для всех формируемых запросов на выгрузку.
    /// </summary>
    public class DispenserTaskModel
    {
        /// <summary>
        /// Идентификатор задания на выгрузку,необходимый для работы с другими методами сервиса выгрузок.
        /// </summary>
        [JsonPropertyName("id")]
        [Required]
        public string Id { get; set; }

        /// <summary>
        /// Дата создания. Возвращается в формате yyyy-MM-ddTHH:mm:ss.SSS.
        /// </summary>
        [JsonPropertyName("createDate")]
        [Required]
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// Наименование задания.
        /// </summary>
        [JsonPropertyName("name")]
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Текущий статус.
        /// </summary>
        [JsonPropertyName("currentStatus")]
        public DispenserStatus? CurrentStatus { get; set; }

        /// <summary>
        /// Дата начала периода, по которому сформирована выгрузка(используется только для заданий c periodicity = SINGLE).
        /// Возвращается в формате yyyy-MM-dd.
        /// </summary>
        [JsonPropertyName("dataStartDate")]
        public DateTime? DateStartDate { get; set; }

        /// <summary>
        /// Дата окончания периода, по которому сформирована выгрузка(используется только для заданий c periodicity = SINGLE).
        /// Возвращается в формате yyyy-MM-dd.
        /// </summary>
        [JsonPropertyName("dataEndDate")]
        public DateTime? DateEndDate { get; set; }

        /// <summary>
        /// ИНН организации.
        /// </summary>
        [JsonPropertyName("orgInn")]
        [Required]
        public string OrgInn { get; set; }

        /// <summary>
        /// Периодичность регулярной выгрузки (только для periodicity: "REGULAR").
        /// </summary>
        [JsonPropertyName("period")]
        public DispenserPeriod? Period { get; set; }

        /// <summary>
        /// Вид периодичности.
        /// </summary>
        [JsonPropertyName("periodicity")]
        [Required]
        public DispenserPeriodicity? Periodicity { get; set; }

        /// <summary>
        /// Указывается цифровой код товарной группы.
        /// </summary>
        [JsonPropertyName("productGroupCode")]
        public ProductGroup? ProductGroupCode { get; set; }

        /// <summary>
        /// Таймаут в сек., при наступлении которого диспетчер считает, что выгрузка по данному заданию не выполнена.
        /// </summary>
        [JsonPropertyName("timeoutSecs")]
        public int? TimeoutSecs { get; set; }

        public override string ToString() => $"{Name} {CurrentStatus}: {ProductGroupCode}, {CreateDate}";

    }
}
