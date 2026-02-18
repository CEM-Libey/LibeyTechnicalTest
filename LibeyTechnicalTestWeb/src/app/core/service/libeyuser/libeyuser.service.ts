import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { environment } from "../../../../environments/environment";
import { LibeyUser } from "src/app/entities/libeyuser";
import { Live } from "@ng-bootstrap/ng-bootstrap/util/accessibility/live";
@Injectable({
	providedIn: "root",
})
export class LibeyUserService {
	constructor(private http: HttpClient) {}
	Find(documentNumber: string): Observable<LibeyUser> {
		const uri = `${environment.pathLibeyTechnicalTest}LibeyUser/${documentNumber}`;
		return this.http.get<LibeyUser>(uri);
	}
	getAll(): Observable<LibeyUser[]> { 
		const uri = `${environment.pathLibeyTechnicalTest}LibeyUser/listLibeyUsers`;
		return this.http.get<LibeyUser[]>(uri);
	}
	create(user: any): Observable<LibeyUser> { 
		const uri = `${environment.pathLibeyTechnicalTest}LibeyUser`;
		return this.http.post<LibeyUser>(uri, user); 
	}
	update(user: any): Observable<LibeyUser> { 
		const uri = `${environment.pathLibeyTechnicalTest}LibeyUser`;
		return this.http.put<LibeyUser>(uri, user); 
	}
	delete(documentNumber: string): Observable<LibeyUser> { 
		const uri = `${environment.pathLibeyTechnicalTest}LibeyUser/${documentNumber}`;
		return this.http.delete<LibeyUser>(uri);
	}
}