using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Spoleto.TrueApi
{
    /// <summary>
    /// Текущий статус задания на выгрузку + информация, необходимая пользователю для дальнейшей работы.
    /// </summary>
    public class DispenserTaskStatusModel
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
        /// ИНН организации.
        /// </summary>
        [JsonPropertyName("orgInn")]
        [Required]
        public string OrgInn { get; set; }

        /// <summary>
        /// Указывается цифровой код товарной группы.
        /// </summary>
        [JsonPropertyName("productGroupCode")]
        public ProductGroup? ProductGroupCode { get; set; }

        /// <summary>
        /// Время хранения выгрузки в днях.
        /// </summary>
        [JsonPropertyName("downloadingStorageDays")]
        public int? DownloadingStorageDays { get; set; }

        /// <summary>
        /// Доступные товарные группы для текущего типа задач.
        /// </summary>
        [JsonPropertyName("productGroups")]
        public List<DispenserProductGroupModel> ProductGroups { get; set; }

        /// <summary>
        /// Таймаут в сек., при наступлении которого диспетчер считает, что выгрузка по данному заданию не выполнена.
        /// </summary>
        [JsonPropertyName("timeoutSecs")]
        public int? TimeoutSecs { get; set; }

        public override string ToString() => $"{Name} {CurrentStatus}: {ProductGroupCode}, {CreateDate}";
    }
}
