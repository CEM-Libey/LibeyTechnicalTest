namespace LibeyTechnicalTestDomain.LibeyUserAggregate.Domain
{
    public class LibeyUser
    {
        public string DocumentNumber { get; private set; }
        public int DocumentTypeId { get; private set; }
        public string Name { get; private set; }
        public string FathersLastName { get; private set; }
        public string MothersLastName { get; private set; }
        public string Address { get; private set; }
        public string UbigeoCode { get; private set; }
        public string Phone { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public bool Active { get; private set; }
        public LibeyUser(string documentNumber, int documentTypeId, string name, string fathersLastName, string mothersLastName, string address,
        string ubigeoCode, string phone, string email, string password)
        {
            DocumentNumber = documentNumber;
            DocumentTypeId = documentTypeId;
            Name = name;
            FathersLastName = fathersLastName;
            MothersLastName = mothersLastName;
            Address = address;
            UbigeoCode = ubigeoCode;
            Phone = phone;
            Email = email;
            Password = password;
            Active = true;
        }

        public void SetDocumentNumber(string value)
        {
            DocumentNumber = value;
        }
        public void SetDocumentTypeId(int value)
        {
            DocumentTypeId = value;
        }
        public void SetName(string value)
        {
            Name = value;
        }
        public void SetFathersLastName(string value)
        {
            FathersLastName = value;
        }
        public void SetMothersLastName(string value)
        {
            MothersLastName = value;
        }
        public void SetAddress(string value)
        {
            Address = value;
        }
        public void SetUbigeoCode(string value)
        {
            UbigeoCode = value;
        }
        public void SetPhone(string value)
        {
            Phone = value;
        }
        public void SetEmail(string value)
        {
            Email = value;
        }
        public void SetPassword(string value)
        {
            Password = value;
        }
        public void SetActive(bool value)
        {
            Active = value;
        }
    }
}