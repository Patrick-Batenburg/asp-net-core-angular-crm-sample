import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Company } from '../models';
import { environment } from 'src/environments/environment';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class CompanyService {
  private readonly httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };
  private apiUrl = `${environment.baseApiUrl}/Companies`;

  constructor(private httpClient: HttpClient) {}

  public getCompanies(): Observable<Company[]> {
    return this.httpClient.get<Company[]>(this.apiUrl);
  }

  public getCompaniesWithActiveVacancies(): Observable<any> {
    return this.httpClient.get<any>(`${this.apiUrl}/ActiveVacancies`);
  }

  public getProduct(id: number): Observable<Company> {
    return this.httpClient.get<Company>(`${this.apiUrl}/${id}`);
  }
}
