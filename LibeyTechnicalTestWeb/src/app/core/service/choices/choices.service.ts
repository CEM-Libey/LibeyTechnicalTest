import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { environment } from "../../../../environments/environment";
import { DocumentType } from "src/app/entities/documentType";
import { Region } from "src/app/entities/region";
import { Province } from "src/app/entities/province";
import { Ubigeo } from "src/app/entities/ubigeo";
@Injectable({
    providedIn: "root",
})
export class ChoicesService {
    private base = `${environment.pathLibeyTechnicalTest}`
    
    constructor(private http: HttpClient) {}

    FindDocumentsTypes(): Observable<DocumentType[]> {
        return this.http.get<DocumentType[]>(`${this.base}/DocumentType`);
    }

    FindRegions(): Observable<Region[]> {
        return this.http.get<Region[]>(`${this.base}/Region`);
    }

    FindProvinces(regionCode: string): Observable<Province[]> {
        return this.http.get<Province[]>(`${this.base}/Province/${regionCode}`);
    }
    
    FindUbigeos(provinceCode: string, regionCode: string): Observable<Ubigeo[]> {
        return this.http.get<Ubigeo[]>(`${this.base}/Ubigeo/${provinceCode}/${regionCode}`);
    }
}