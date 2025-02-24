import { TipoDocumento } from './../../../entities/documenttype';
import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders, HttpResponse } from "@angular/common/http";
import { Observable } from "rxjs";
import { environment } from "../../../../environments/environment";
import { LibeyUser } from "src/app/entities/libeyuser";
import { UbigeoResponse } from 'src/app/entities/ubigeo';



@Injectable({
	providedIn: "root",
})
export class LibeyUserService {

  private readonly URL=environment.pathLibeyTechnicalTest;

	constructor(private http: HttpClient) {}


	// Find(documentNumber: string): Observable<LibeyUser> {
	// 	const uri = `${this.URL}/LibeyUser/${documentNumber}`;
	// 	return this.http.get<LibeyUser>(uri);
	// }


  Find(documentNumber: string): Observable<LibeyUser> {
    return this.http.get<LibeyUser>(`${this.URL}/LibeyUser/${documentNumber}`);
  }

  getAllLibeyUsers$(): Observable<LibeyUser[]> {
    return this.http.get<LibeyUser[]>(`${this.URL}/LibeyUser`);
  }

  getAllDocumentTypes$(): Observable<TipoDocumento[]> {
    return this.http.get<TipoDocumento[]>(`${this.URL}/DocumentType`);
  }

  getUbigeo$(): Observable<UbigeoResponse> {
    return this.http.get<UbigeoResponse>(`${this.URL}/Ubigeo`);
  }

  createLibeyUser(user: LibeyUser): Observable<HttpResponse<any>> {
    return this.http.post<any>(`${this.URL}/LibeyUser`, user, { observe: 'response' });
  }

  updateLibeyUser(user: LibeyUser): Observable<HttpResponse<any>> {
    return this.http.put<LibeyUser>(`${this.URL}/LibeyUser`, user, { observe: 'response' });
  }



  deleteLibeyUser(documentNumber: string): Observable<void> {
    return this.http.delete<void>(`${this.URL}/LibeyUser/${documentNumber}`);
  }

  updateUserStatus(documentNumber: string, estado: boolean): Observable<void> {
    const url = `${this.URL}/LibeyUser/${documentNumber}/state`;
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    const body = { active: estado };
    return this.http.put<void>(url, body, { headers });
  }

}
