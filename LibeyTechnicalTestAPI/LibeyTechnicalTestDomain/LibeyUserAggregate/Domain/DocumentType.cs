namespace LibeyTechnicalTestDomain.LibeyUserAggregate.Domain
{
    public class DocumentType
    {
        public int DocumentTypeId { get; set; }
        public string DocumentTypeDescription { get; set; }

        public DocumentType() { }

        public DocumentType(int documentType, string documentTypeDescription)
        {
            DocumentTypeId = documentType;
            DocumentTypeDescription = documentTypeDescription;
        }
    }
}
