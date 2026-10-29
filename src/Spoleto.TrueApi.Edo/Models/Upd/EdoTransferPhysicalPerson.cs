namespace Spoleto.TrueApi.Edo.Models.Upd
{
    /// <summary>
    /// Физическое лицо, которому доверена отгрузка товаров (ФЛПер).
    /// </summary>
    public record EdoTransferPhysicalPerson : EdoPerson
    {
        /// <summary>
        /// ИНН физического лица (ИННФЛПер).
        /// </summary>
        public string? PersonInn { get; set; }

        /// <summary>
        /// Иные сведения, идентифицирующие физическое лицо (ИныеСвед).
        /// </summary>
        public string? OtherInfo { get; set; }

        /// <summary>
        /// Основание, по которому физическому лицу доверена отгрузка товаров (ОснДоверФЛ).
        /// </summary>
        public EdoDocumentDetails? PersonBase { get; set; }
    }
}
