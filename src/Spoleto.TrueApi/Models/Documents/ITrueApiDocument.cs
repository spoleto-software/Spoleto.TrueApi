namespace Spoleto.TrueApi.Documents
{
    /// <summary>
    /// Документ True Api
    /// </summary>
    public interface ITrueApiDocument
    {
        /// <summary>
        /// Тип документа
        /// </summary>
        /// <remarks>
        /// см.Справочник "Типы документов"
        /// </remarks>
        DocumentType DocumentType { get; }
    }
}
