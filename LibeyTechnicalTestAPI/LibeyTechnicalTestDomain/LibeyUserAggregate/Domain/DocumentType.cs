namespace LibeyTechnicalTestDomain.LibeyUserAggregate.Domain
{
    public class DocumentType
    {
        public int DocumentTypeId { get; set; }
        public string DocumentTypeDescription { get; set; }
        public ICollection<LibeyUser> LibeyUsers { get; set; } = new List<LibeyUser>();

        public DocumentType() { }

        public DocumentType(int documentTypeId, string documentTypeDescription)
        {
            DocumentTypeId = documentTypeId;
            DocumentTypeDescription = documentTypeDescription;
        }
    }    
}