import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { environment } from "../../../../environments/environment";
import { LibeyUser } from "src/app/entities/libeyuser";
@Injectable({
	providedIn: "root",
})
export class LibeyUserService {
	private base = `${environment.pathLibeyTechnicalTest}/LibeyUser`
	
	constructor(private http: HttpClient) {}

	Find(documentNumber: string): Observable<LibeyUser> {
		return this.http.get<LibeyUser>(`${this.base}/${documentNumber}`);
	}

	FindAll(): Observable<LibeyUser[]> {
		return this.http.get<LibeyUser[]>(`${this.base}/all`);
	}

	Create(payload: LibeyUser) {
		return this.http.post<boolean>(this.base, payload);
	}

	Update(payload: Partial<LibeyUser>) {
		return this.http.patch<boolean>(this.base, payload);
	}

	Remove(documentNumber: string) {
		return this.http.delete<boolean>(`${this.base}/${documentNumber}`);
	}
}