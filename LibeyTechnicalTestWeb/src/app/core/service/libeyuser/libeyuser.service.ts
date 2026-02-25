import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { environment } from "../../../../environments/environment";
import { LibeyUser } from "src/app/entities/libeyuser";
@Injectable({
	providedIn: "root",
})
export class LibeyUserService {
	constructor(private http: HttpClient) {}
	Find(documentNumber: string): Observable<LibeyUser> {
		const uri = `${environment.pathLibeyTechnicalTest}LibeyUser/${documentNumber}`;
		return this.http.get<LibeyUser>(uri);
	}
	GetAll(): Observable<LibeyUser[]> {
		const uri = `${environment.pathLibeyTechnicalTest}LibeyUser`;
		return this.http.get<LibeyUser[]>(uri);
	}

	Create(user: LibeyUser): Observable<any> {
		const uri = `${environment.pathLibeyTechnicalTest}LibeyUser`;
		return this.http.post(uri, user);
	}

	Update(documentNumber: string, user: LibeyUser): Observable<any> {
		const uri = `${environment.pathLibeyTechnicalTest}LibeyUser/${documentNumber}`;
		return this.http.put(uri, user);
	}

	Delete(documentNumber: string): Observable<any> {
		const uri = `${environment.pathLibeyTechnicalTest}LibeyUser/${documentNumber}`;
		return this.http.delete(uri);
	}
}