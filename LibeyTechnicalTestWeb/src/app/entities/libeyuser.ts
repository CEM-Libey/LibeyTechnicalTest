export interface LibeyUser{
    documentNumber:string;
    documentTypeId:string;
    name:string;
    fathersLastName :string;
    mothersLastName :string;
    address :string;
    regionCode :string;
    provinceCode :string;
    ubigeoCode :string;
    phone :string;
    email :string;
    password :string;
    active :boolean;
}
export interface LibeyUserAddOrAlter{
  documentNumber:string;
  documentTypeId:string;
  name:string;
  fathersLastName :string;
  mothersLastName :string;
  address :string;
  ubigeoCode :string;
  regionCode :string;
  provinceCode :string;
  phone :string;
  email :string;
  password :string;
}
