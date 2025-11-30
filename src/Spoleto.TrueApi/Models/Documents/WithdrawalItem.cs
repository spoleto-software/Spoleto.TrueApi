using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Spoleto.TrueApi.Documents
{
    /// <summary>
    /// Товар для вывода из оборота
    /// </summary>
    public class WithdrawalItem
    {
        /// <summary>
        /// КИ/КИН
        /// </summary>
        /// <remarks>
        /// КИТУ расформировывается при указании КИН, входящего в состав КИТУ
        /// </remarks>
        [JsonPropertyName("cis")]
        [Required]
        public string Cis { get; set; }

        /// <summary>
        /// Дата первичного документа
        /// </summary>
        /// <remarks>
        /// Задается в формате yyyy-MM-dd, указывается при необходимости или отличия от сведений параметра document_date
        /// </remarks>
        [JsonPropertyName("primary_document_date")]
        public DateTime? PrimaryDocumentDate { get; set; }

        /// <summary>
        /// Номер первичного документа
        /// </summary>
        /// <remarks>
        /// указывается при необходимости или отличия от сведений параметра document_number
        /// </remarks>
        [JsonPropertyName("primary_document_number")]
        public string PrimaryDocumentNumber { get; set; }

        /// <summary>
        /// Вид первичного документа
        /// </summary>
        /// <remarks>
        /// Вид первичного документа зависит от параметра "action".
        /// Указывается при необходимости или отличия от сведений параметра document_type
        /// </remarks>
        [JsonPropertyName("primary_document_type")]
        public PrimaryDocumentType PrimaryDocumentType { get; set; }

        /// <summary>
        /// Наименование первичного документа
        /// </summary>
        /// <remarks>
        /// указывается при необходимости или отличия от сведений параметра primary_document_custom_name
        /// </remarks>
        [JsonPropertyName("primary_document_custom_name")]
        public string PrimaryDocumentCustomName { get; set; }

        /// <summary>
        /// Цена за единицу
        /// </summary>
        /// <remarks>
        /// Стоимость указывается в копейках, с учетом НДС
        /// </remarks>
        [JsonPropertyName("product_cost")]
        public int? ProductCost { get; set; }
    }
}
