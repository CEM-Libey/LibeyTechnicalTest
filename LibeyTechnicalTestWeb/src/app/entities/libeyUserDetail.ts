import { LibeyUser } from "./libeyuser";

export interface LibeyUserDetail extends LibeyUser{
    documentTypeDescription: string;
    provinceDescription: string;
    regionDescription: string
    ubigeoDescription: string
}

