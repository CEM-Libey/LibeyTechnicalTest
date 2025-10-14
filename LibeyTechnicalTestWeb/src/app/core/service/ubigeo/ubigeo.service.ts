import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { environment } from "../../../../environments/environment";
import { Ubigeo } from "src/app/entities/ubigeo";

@Injectable({
    providedIn: "root",
})
export class UbigeoService {

 

    private apiUrl = environment.pathLibeyTechnicalTest + "Ubigeo";

    constructor(private http: HttpClient) {}

    getAll(): Observable<Ubigeo[]> {
        debugger;
        return this.http.get<Ubigeo[]>(this.apiUrl);
    }

}