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
            ValidateBusinessRules(documentNumber, name, email);
            
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

        // Métodos de dominio
        public void UpdateProfile(string name, string fathersLastName, string mothersLastName, 
                                string address, string ubigeoCode, string phone, string email)
        {
            ValidateBusinessRules(DocumentNumber, name, email);
            
            Name = name;
            FathersLastName = fathersLastName;
            MothersLastName = mothersLastName;
            Address = address;
            UbigeoCode = ubigeoCode;
            Phone = phone;
            Email = email;
        }

        public void Deactivate()
        {
            Active = false;
        }

        public void Activate()
        {
            Active = true;
        }

        public void ChangePassword(string newPassword)
        {
            if (string.IsNullOrWhiteSpace(newPassword))
                throw new ArgumentException("Password cannot be empty");
            
            Password = newPassword;
        }

        // Validaciones de dominio
        private void ValidateBusinessRules(string documentNumber, string name, string email)
        {
            if (string.IsNullOrWhiteSpace(documentNumber))
                throw new ArgumentException("Document number is required");
            
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name is required");
            
            if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
                throw new ArgumentException("Valid email is required");
        }

        public bool IsValidEmail()
        {
            return !string.IsNullOrWhiteSpace(Email) && Email.Contains("@") && Email.Contains(".");
        }
    }
}