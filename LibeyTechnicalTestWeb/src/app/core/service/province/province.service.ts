import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { environment } from "../../../../environments/environment";
import { Province } from "src/app/entities/province";

@Injectable({
    providedIn: "root",
})
export class ProvinceService {

    private apiUrl = environment.pathLibeyTechnicalTest + "Province";

    constructor(private http: HttpClient) {}

    getAll(): Observable<Province[]> {
        return this.http.get<Province[]>(this.apiUrl);
    }

}