import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root' 
})
export class ApiServiceAdquisition {
  private http = inject(HttpClient); 
  private apiUrl = 'https://localhost:7193'; 

  constructor() {}

  getData(endpoint: string): Observable<any> {
    return this.http.get(`${this.apiUrl}/${endpoint}`);
  }

  postData(endpoint: string, body: any): Observable<any> {
    return this.http.post(`${this.apiUrl}/${endpoint}`, body);
  }

  updateData(endpoint: string, body: any): Observable<any> {
    return this.http.put(`${this.apiUrl}/${endpoint}`, body);
  }

  deleteData(endpoint: string, id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${endpoint}/${id}`);
  }
}
