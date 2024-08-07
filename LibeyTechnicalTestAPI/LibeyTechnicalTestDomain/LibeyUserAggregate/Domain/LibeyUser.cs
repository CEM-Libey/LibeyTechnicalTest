namespace LibeyTechnicalTestDomain.LibeyUserAggregate.Domain
{
    public class LibeyUser
    {
        public string DocumentNumber { get; set; }
        public int DocumentTypeId { get;  set; }
        public string Name { get;  set; }
        public string FathersLastName { get;  set; }
        public string MothersLastName { get;  set; }
        public string Address { get;  set; }
        public string UbigeoCode { get;  set; }
        public string RegionCode { get; set; }
        public string ProvinceCode { get; set; }
        public string Phone { get;  set; }
        public string Email { get;  set; }
        public string Password { get; set; }
        public bool Active { get;  set; }
        public LibeyUser(string documentNumber, int documentTypeId, string name, string fathersLastName, string mothersLastName, string address,
        string ubigeoCode,string regionCode,string provinceCode, string phone, string email, string password)
        {
            DocumentNumber = documentNumber;
            DocumentTypeId = documentTypeId;
            Name = name;
            FathersLastName = fathersLastName;
            MothersLastName = mothersLastName;
            Address = address;
            UbigeoCode = ubigeoCode;
            RegionCode=regionCode;
            ProvinceCode=provinceCode;
            Phone = phone;
            Email = email;
            Password = password;
            Active = true;
        }
    }
}