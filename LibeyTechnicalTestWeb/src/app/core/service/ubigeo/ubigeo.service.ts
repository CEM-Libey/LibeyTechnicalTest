import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Combo } from 'src/app/entities/combo';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class UbigeoService {
  constructor(private http: HttpClient) { }
  private baseUrl = `${environment.pathLibeyTechnicalTest}Ubigeo`;
  region(): Observable<Combo[]> {
    return this.http.get<Combo[]>(`${this.baseUrl}/regionList`);
  }
  provinces(regionCode: string): Observable<Combo[]> {
    return this.http.get<Combo[]>(`${this.baseUrl}/byregions/${regionCode}`);
  }
  ubigeos(provinceCode: string): Observable<Combo[]> {
    return this.http.get<Combo[]>(`${this.baseUrl}/byprovinces/${provinceCode}`);
  }

}
