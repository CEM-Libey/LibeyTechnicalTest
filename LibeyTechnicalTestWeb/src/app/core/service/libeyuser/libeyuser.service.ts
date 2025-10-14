import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { environment } from "../../../../environments/environment";
import { LibeyUser } from "src/app/entities/libeyuser";

@Injectable({
	providedIn: "root",
})
export class LibeyUserService {

	private apiUrl = environment.pathLibeyTechnicalTest + "LibeyUser";

	constructor(private http: HttpClient) {}
	
	Find(documentNumber: string): Observable<LibeyUser> { 
		return this.http.get<LibeyUser>(`${this.apiUrl}/${documentNumber}`);
	}

	getAll(): Observable<LibeyUser[]> {
		return this.http.get<LibeyUser[]>(this.apiUrl);
	}

	getUser(id:string): Observable<LibeyUser> {
		return this.http.get<LibeyUser>(`${this.apiUrl}/${id}`);	
	}

	createUser(libeyUser: LibeyUser): Observable<void> {
		return this.http.post<void>(this.apiUrl, libeyUser);
	}

	updateUser(libeyUser: LibeyUser): Observable<void> { 
		return this.http.put<void>(this.apiUrl, libeyUser);
	}

	deleteUser(documentNumber: string): Observable<void> { 
		return this.http.delete<void>(`${this.apiUrl}/${documentNumber}`);
	}

}