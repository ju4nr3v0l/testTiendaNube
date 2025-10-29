import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class Api {
  private apiUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getToken(code: string): Observable<any> {
    return this.http.post(`${this.apiUrl}/auth/token`, { code });
  }

  getStoreInfo(): Observable<any> {
    return this.http.get(`${this.apiUrl}/store/info`);
  }

  getOrders(): Observable<any> {
    return this.http.get(`${this.apiUrl}/orders`);
  }

  createPayment(data: any): Observable<any> {
    return this.http.post(`${this.apiUrl}/payments`, data);
  }
}
