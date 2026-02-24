import { Injectable } from "@angular/core";
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { environment } from "../../../../environments/environment";
import { LibeyUser } from "src/app/entities/libeyuser";
import { LibeyUserDetail } from "src/app/entities/libeyUserDetail";
import { LibeyUserQuery } from "src/app/entities/libeyUserQuery";
import { BaseResponse } from "src/app/entities/baseResponse";
import { Combo } from "src/app/entities/combo";
@Injectable({
	providedIn: "root",
})
export class LibeyUserService {
	constructor(private http: HttpClient) { }
	private baseUrl = `${environment.pathLibeyTechnicalTest}LibeyUser`;
	find(documentNumber: string): Observable<LibeyUserDetail> {
		const uri = `${environment.pathLibeyTechnicalTest}LibeyUser/${documentNumber}`;
		return this.http.get<LibeyUserDetail>(uri);
	}
	list(query: LibeyUserQuery): Observable<LibeyUser[]> {
		let params = new HttpParams()
			.set('Skip', query.skip)
			.set('Take', query.take);
		if (query.documentTypeId != null) {
			params = params.set('DocumentTypeId', query.documentTypeId);
		}
		if (query.ubigeoCode) {
			params = params.set('UbigeoCode', query.ubigeoCode);
		}
		if (query.documentNumber) {
			params = params.set('documentNumber', query.documentNumber);
		}
		if (query.email) {
			params = params.set('email', query.email);
		}
		if (query.phone) {
			params = params.set('phone', query.phone);
		}
		if (query.name) {
			params = params.set('name', query.name);
		}
		return this.http.get<LibeyUser[]>(this.baseUrl, { params });
	}
	create(user: LibeyUser): Observable<BaseResponse> {
		return this.http.post<BaseResponse>(this.baseUrl, user);
	}
	update(user: LibeyUser): Observable<BaseResponse> {
		return this.http.put<BaseResponse>(this.baseUrl, user);
	}
	delete(documentNumber: string): Observable<BaseResponse> {
		return this.http.delete<BaseResponse>(`${this.baseUrl}/${documentNumber}`);
	}
	typeCombo(): Observable<Combo[]> {
		return this.http.get<Combo[]>(`${this.baseUrl}/typeCombo`);
	}
}