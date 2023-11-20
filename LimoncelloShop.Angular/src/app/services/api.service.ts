import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  constructor(private httpClient: HttpClient) { }


  get<Type>(url: string, params?: HttpParams): Observable<Type> {
    return this.httpClient.get<Type>(url, { params: params });
  }

  post<Type>(url: string, body: any): Observable<Type> {
    return this.httpClient.post<Type>(url, body);
  }

  put<Type>(url: string, body: any): Observable<Type> {
    return this.httpClient.put<Type>(url, body);
  }

  delete<Type>(url: string): Observable<Type> {
    return this.httpClient.delete<Type>(url);
  }
}