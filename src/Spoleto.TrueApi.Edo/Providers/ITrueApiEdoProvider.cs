using Spoleto.TrueApi.Edo.Models;
using Spoleto.TrueApi.Edo.Models.Options;

namespace Spoleto.TrueApi.Edo.Providers
{
    /// <summary>
    /// Провайдер методов «ЭДО Лайт» в True API.
    /// </summary>
    public interface ITrueApiEdoProvider
    {
        /// <summary>
        /// Получение списка входящих документов.
        /// </summary>
        /// <param name="settings">Настройки провайдера.</param>
        /// <param name="searchModel">Критерии отбора документов.</param>
        /// <returns>Страница списка входящих документов.</returns>
        EdoDocumentCollection GetIncomingDocumentList(TrueApiEdoProviderOption settings, EdoDocumentSearchModel? searchModel = null)
            => GetIncomingDocumentListAsync(settings, searchModel).GetAwaiter().GetResult();

        /// <summary>
        /// Получение списка входящих документов.
        /// </summary>
        /// <param name="settings">Настройки провайдера.</param>
        /// <param name="searchModel">Критерии отбора документов.</param>
        /// <returns>Страница списка входящих документов.</returns>
        Task<EdoDocumentCollection> GetIncomingDocumentListAsync(TrueApiEdoProviderOption settings, EdoDocumentSearchModel? searchModel = null);

        /// <summary>
        /// Получение файла входящего документа.
        /// </summary>
        /// <param name="settings">Настройки провайдера.</param>
        /// <param name="documentId">Идентификатор документа в системе ЭДО Оператора.</param>
        /// <returns>Файл как есть: XML для УПД, УКД и квитанций (в кодировке файла, для УПД - windows-1251), PDF для договоров с оператором ГИС МТ.</returns>
        byte[] GetIncomingDocument(TrueApiEdoProviderOption settings, string documentId)
            => GetIncomingDocumentAsync(settings, documentId).GetAwaiter().GetResult();

        /// <summary>
        /// Получение файла входящего документа.
        /// </summary>
        /// <param name="settings">Настройки провайдера.</param>
        /// <param name="documentId">Идентификатор документа в системе ЭДО Оператора.</param>
        /// <returns>Файл как есть: XML для УПД, УКД и квитанций (в кодировке файла, для УПД - windows-1251), PDF для договоров с оператором ГИС МТ.</returns>
        Task<byte[]> GetIncomingDocumentAsync(TrueApiEdoProviderOption settings, string documentId);

        /// <summary>
        /// Получение списка исходящих документов.
        /// </summary>
        /// <param name="settings">Настройки провайдера.</param>
        /// <param name="searchModel">Критерии отбора документов.</param>
        /// <returns>Страница списка исходящих документов.</returns>
        EdoDocumentCollection GetOutgoingDocumentList(TrueApiEdoProviderOption settings, EdoDocumentSearchModel? searchModel = null)
            => GetOutgoingDocumentListAsync(settings, searchModel).GetAwaiter().GetResult();

        /// <summary>
        /// Получение списка исходящих документов.
        /// </summary>
        /// <param name="settings">Настройки провайдера.</param>
        /// <param name="searchModel">Критерии отбора документов.</param>
        /// <returns>Страница списка исходящих документов.</returns>
        Task<EdoDocumentCollection> GetOutgoingDocumentListAsync(TrueApiEdoProviderOption settings, EdoDocumentSearchModel? searchModel = null);

        /// <summary>
        /// Получение файла исходящего документа.
        /// </summary>
        /// <param name="settings">Настройки провайдера.</param>
        /// <param name="documentId">Идентификатор документа в системе ЭДО Оператора.</param>
        /// <returns>Файл как есть: XML для УПД, УКД и квитанций (в кодировке файла, для УПД - windows-1251), PDF для договоров с оператором ГИС МТ.</returns>
        byte[] GetOutgoingDocument(TrueApiEdoProviderOption settings, string documentId)
            => GetOutgoingDocumentAsync(settings, documentId).GetAwaiter().GetResult();

        /// <summary>
        /// Получение файла исходящего документа.
        /// </summary>
        /// <param name="settings">Настройки провайдера.</param>
        /// <param name="documentId">Идентификатор документа в системе ЭДО Оператора.</param>
        /// <returns>Файл как есть: XML для УПД, УКД и квитанций (в кодировке файла, для УПД - windows-1251), PDF для договоров с оператором ГИС МТ.</returns>
        Task<byte[]> GetOutgoingDocumentAsync(TrueApiEdoProviderOption settings, string documentId);
    }
}
