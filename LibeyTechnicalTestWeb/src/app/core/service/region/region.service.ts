import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { environment } from "../../../../environments/environment";
import { Region } from "src/app/entities/region";

@Injectable({
    providedIn: "root",
})
export class RegionService {

    private apiUrl = environment.pathLibeyTechnicalTest + "Region";

    constructor(private http: HttpClient) {}

    getAll(): Observable<Region[]> {
        return this.http.get<Region[]>(this.apiUrl);
    }

}