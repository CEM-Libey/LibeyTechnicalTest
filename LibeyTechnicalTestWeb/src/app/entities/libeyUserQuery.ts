export interface LibeyUserQuery {
    skip: number;
    take: number;
    documentTypeId?: string;
    ubigeoCode?: string;
    documentNumber?: string;
    email?: string,
    phone?: string;
    name?: string
}
