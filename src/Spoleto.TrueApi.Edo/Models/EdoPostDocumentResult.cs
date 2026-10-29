namespace Spoleto.TrueApi.Edo.Models
{
    /// <summary>
    /// Результат загрузки файла информации продавца в «ЭДО Лайт».
    /// </summary>
    public record EdoPostDocumentResult
    {
        /// <summary>
        /// Идентификатор документа в «ЭДО Лайт». По нему черновик подписывается и отправляется, а документ скачивается.
        /// </summary>
        /// <remarks>
        /// Если файл с таким именем уже загружен, сервис возвращает идентификатор ранее загруженного файла.
        /// </remarks>
        public required string Id { get; set; }

        /// <summary>
        /// Идентификатор файла (ИдФайл), он же имя файла без расширения.
        /// </summary>
        public required string FileId { get; set; }

        /// <summary>
        /// XML отправленного файла.
        /// </summary>
        public required string Content { get; set; }

        /// <summary>
        /// Отправленный файл байт в байт, в windows-1251. Подпись документа делается по этим байтам.
        /// </summary>
        public required byte[] RawContent { get; set; }

        public override string ToString() => $"{Id} ({FileId})";
    }
}
