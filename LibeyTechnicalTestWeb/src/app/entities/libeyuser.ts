export interface LibeyUser {
  documentNumber: string;
  documentTypeId: number | null;
  documentTypeDescription: string;
  name: string;
  fathersLastName: string;
  mothersLastName: string;
  address: string;
  regionCode: string| null;
  regionDescription: string;
  provinceCode: string| null;
  provinceDescription: string;
  ubigeoCode: string| null;
  ubigeoDescription: string;
  phone: string;
  email: string;
  password: string;
  active: boolean| null;
}
