namespace Spoleto.TrueApi.Edo.Models.Upd
{
    /// <summary>
    /// Представитель организации, которой доверена отгрузка товаров (ПредОргПер).
    /// </summary>
    public record EdoTransferOrganizationPerson : EdoPerson
    {
        /// <summary>
        /// Должность (Должность).
        /// </summary>
        public required string JobTitle { get; set; }

        /// <summary>
        /// Иные сведения, идентифицирующие физическое лицо (ИныеСвед).
        /// </summary>
        public string? OtherInfo { get; set; }

        /// <summary>
        /// Наименование организации (НаимОргПер).
        /// </summary>
        public required string OrganizationName { get; set; }

        /// <summary>
        /// ИНН организации (ИННЮЛПер).
        /// </summary>
        public string? OrganizationInn { get; set; }

        /// <summary>
        /// Основание, по которому организации доверена отгрузка товаров (ОснДоверОргПер).
        /// </summary>
        public EdoDocumentDetails? OrganizationBase { get; set; }

        /// <summary>
        /// Основание полномочий представителя организации на отгрузку товаров (ОснПолнПредПер).
        /// </summary>
        public EdoDocumentDetails? EmployeeBase { get; set; }
    }
}
