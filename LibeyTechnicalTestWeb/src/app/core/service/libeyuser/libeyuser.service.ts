import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { environment } from "../../../../environments/environment";
import { LibeyUser, LibeyUserAddOrAlter } from "src/app/entities/libeyuser";
import { ResponseMessage } from "src/app/entities/responseMessage";
@Injectable({
	providedIn: "root",
})
export class LibeyUserService {
	constructor(private http: HttpClient) {}
	Find(documentNumber: string): Observable<LibeyUser> {
		const uri = `${environment.pathLibeyTechnicalTest}LibeyUser/${documentNumber}`;
		return this.http.get<LibeyUser>(uri);
	}

  Create(libeyUse: LibeyUserAddOrAlter): Observable<ResponseMessage> {
		const uri = `${environment.pathLibeyTechnicalTest}LibeyUser`;
		return this.http.post<ResponseMessage>(uri,libeyUse);
	}

  Update(libeyUse: LibeyUserAddOrAlter): Observable<ResponseMessage> {
		const uri = `${environment.pathLibeyTechnicalTest}LibeyUser`;
		return this.http.put<ResponseMessage>(uri,libeyUse);
	}
  Delete(documentNumber: string): Observable<ResponseMessage> {
		const uri = `${environment.pathLibeyTechnicalTest}LibeyUser/${documentNumber}`;
		return this.http.delete<ResponseMessage>(uri);
	}
  ChangeState(documentNumber: string): Observable<ResponseMessage> {
		const uri = `${environment.pathLibeyTechnicalTest}LibeyUser/ChangeState/${documentNumber}`;
		return this.http.get<ResponseMessage>(uri);
	}

	Get(): Observable<LibeyUser[]> {
		const uri = `${environment.pathLibeyTechnicalTest}LibeyUser`;
		return this.http.get<LibeyUser[]>(uri);
	}
  GetText(texto:string): Observable<LibeyUser[]> {
		const uri = `${environment.pathLibeyTechnicalTest}LibeyUser/getText/${texto}`;
		return this.http.get<LibeyUser[]>(uri);
	}

}
