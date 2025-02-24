export interface Distrito {
  codigoDistrito: string;
  nombreDistrito: string;
}

export interface Provincia {
  codigoProvincia: string;
  nombreProvincia: string;
  distritos: Distrito[];
}

export interface Region {
  codigoRegion: string;
  nombreRegion: string;
  provincias: Provincia[];
}

export interface UbigeoResponse {
  regiones: Region[];
}
