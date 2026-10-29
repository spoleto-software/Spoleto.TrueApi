namespace Spoleto.TrueApi.Edo.Models.Upd
{
    /// <summary>
    /// Иное лицо, передавшее товар (ИнЛицо). Заполняется одно свойство.
    /// </summary>
    public record EdoOtherIssuer
    {
        /// <summary>
        /// Представитель организации, которой доверена отгрузка товаров (ПредОргПер).
        /// </summary>
        public EdoTransferOrganizationPerson? OrganizationPerson { get; set; }

        /// <summary>
        /// Физическое лицо, которому доверена отгрузка товаров (ФЛПер).
        /// </summary>
        public EdoTransferPhysicalPerson? PhysicalPerson { get; set; }
    }
}
