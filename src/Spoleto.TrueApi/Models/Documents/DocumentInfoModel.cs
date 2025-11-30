using System.Text.Json.Serialization;
using CAPICOM;
using CIS.Cryptography;
using Spoleto.Common;
using Spoleto.Common.Helpers;


namespace Spoleto.TrueApi.Documents
{
    /// <summary>
    /// Информация по документу для передачи в True Api сервис.
    /// </summary>
    /// <typeparam name="T">Тип документа.</typeparam>
    public class DocumentInfoModel<T> where T : ITrueApiDocument
    {
        private readonly ICertificate _certificate;

        private string _signature;
        private string _productDocument;

        public DocumentInfoModel(ICertificate certificate)
        {
            _certificate = certificate;
        }

        /// <summary>
        /// Тип документа
        /// </summary>
        [JsonPropertyName("document_format")]
        public DocumentFormat DocumentFormat => DocumentFormat.MANUAL;

        /// <summary>
        /// Тело документа, переведённое в BASE64
        /// </summary>
        [JsonPropertyName("product_document")]
        public string ProductDocument
        {
            get
            {
                if (_productDocument == null)
                {
                    _productDocument = Convert.ToBase64String(DefaultSettings.Encoding.GetBytes(GetProductDocumentContent(DocumentFormat)));
                }

                return _productDocument;
            }
        }

        /// <summary>
        /// Открепленная подпись(УКЭП) в формате Base64
        /// </summary>
        [JsonPropertyName("signature")]
        public string Signature
        {
            get
            {
                if (_signature == null)
                {
                    var productDocumentBase64 = ProductDocument;
                    _signature = CryptographyHelper.SignBase64Data(productDocumentBase64, detached: true, thumbprint: _certificate.Thumbprint);
                }

                return _signature;
            }
        }

        /// <summary>
        /// Тип документа
        /// </summary>
        /// <remarks>
        /// см.Справочник "Типы документов"
        /// </remarks>
        [JsonPropertyName("type")]
        public DocumentType DocumentType => ProductDocumentObject.DocumentType;

        /// <summary>
        /// Тело документа в виде объекта C#.
        /// </summary>
        [JsonIgnore]
        public T ProductDocumentObject { get; set; }

        private string GetProductDocumentContent(DocumentFormat docFormat)
        {
            if (ProductDocumentObject != null)
            {
                return docFormat switch
                {
                    DocumentFormat.MANUAL => JsonHelper.ToJson(ProductDocumentObject),
                    _ => throw new NotSupportedException(docFormat.ToString()),
                };
            }

            return string.Empty;
        }
    }
}
