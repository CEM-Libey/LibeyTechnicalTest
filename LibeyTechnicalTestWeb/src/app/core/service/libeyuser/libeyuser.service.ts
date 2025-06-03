import { Injectable } from "@angular/core";
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { environment } from "../../../../environments/environment";
import { LibeyUser } from "src/app/entities/libeyuser";

// Interfaces para DTOs
export interface UserCreateRequest {
	documentNumber: string;
	documentTypeId: number;
	name: string;
	fathersLastName: string;
	mothersLastName: string;
	address: string;
	ubigeoCode: string;
	phone: string;
	email: string;
	password: string;
}

export interface UserSearchQuery {
	documentNumber?: string;
	name?: string;
	email?: string;
	active?: boolean;
	page?: number;
	pageSize?: number;
}

export interface UserListResponse {
	users: LibeyUser[];
	totalCount: number;
	page: number;
	pageSize: number;
	totalPages: number;
}

export interface DocumentType {
	id: number;
	description: string;
}

export interface Ubigeo {
	ubigeoCode: string;
	provinceCode: string;
	regionCode: string;
	description: string;
	provinceDescription: string;
	regionDescription: string;
}

@Injectable({
	providedIn: "root",
})
export class LibeyUserService {
	private readonly apiUrl = environment.pathLibeyTechnicalTest + "api/LibeyUser";

	constructor(private http: HttpClient) {}

	// Método original - mantener compatibilidad
	Find(documentNumber: string): Observable<LibeyUser> {
		return this.http.get<LibeyUser>(`${this.apiUrl}/${documentNumber}`);
	}

	// CRUD Operations - nuevos métodos
	getUsers(query: UserSearchQuery = {}): Observable<UserListResponse> {
		let params = new HttpParams();

		if (query.documentNumber) params = params.set('documentNumber', query.documentNumber);
		if (query.name) params = params.set('name', query.name);
		if (query.email) params = params.set('email', query.email);
		if (query.active !== undefined) params = params.set('active', query.active.toString());
		if (query.page) params = params.set('page', query.page.toString());
		if (query.pageSize) params = params.set('pageSize', query.pageSize.toString());

		return this.http.get<UserListResponse>(this.apiUrl, { params });
	}

	getUserByDocument(documentNumber: string): Observable<LibeyUser> {
		return this.http.get<LibeyUser>(`${this.apiUrl}/${documentNumber}`);
	}

	createUser(user: UserCreateRequest): Observable<any> {
		return this.http.post(this.apiUrl, user);
	}

	updateUser(documentNumber: string, user: UserCreateRequest): Observable<any> {
		return this.http.put(`${this.apiUrl}/${documentNumber}`, user);
	}

	deleteUser(documentNumber: string): Observable<any> {
		return this.http.delete(`${this.apiUrl}/${documentNumber}`);
	}

	// Catalog Operations
	getDocumentTypes(): Observable<DocumentType[]> {
		return this.http.get<DocumentType[]>(`${this.apiUrl}/document-types`);
	}

	getUbigeos(): Observable<Ubigeo[]> {
		return this.http.get<Ubigeo[]>(`${this.apiUrl}/ubigeos`);
	}
}