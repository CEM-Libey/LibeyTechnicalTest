
import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { environment } from "../../../../environments/environment";
import { DocumentType } from '../../../entities/documentType';
import { Province } from '../../../entities/province';
import { Region } from '../../../entities/region';
import { Ubigeo } from '../../../entities/ubigeo';
@Injectable({
	providedIn: "root",
})
export class ConfigurationService {
	constructor(private http: HttpClient) {}
	GetDocumentType(): Observable<DocumentType[]> {
		const uri = `${environment.pathLibeyTechnicalTest}Configuration/document_type`;
		return this.http.get<DocumentType[]>(uri);
	}

  GetRegion(): Observable<Region[]> {
		const uri = `${environment.pathLibeyTechnicalTest}Configuration/region`;
		return this.http.get<Region[]>(uri);
	}

  GetProvince(idRegion: string): Observable<Province[]> {
		const uri = `${environment.pathLibeyTechnicalTest}Configuration/province/${idRegion}`;
		return this.http.get<Province[]>(uri);
	}

  GetUbigeo(idRegion: string,idProvince:string): Observable<Ubigeo[]> {
		const uri = `${environment.pathLibeyTechnicalTest}Configuration/distrito/${idRegion}/${idProvince}`;
		return this.http.get<Ubigeo[]>(uri);
	}
}


