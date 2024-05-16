import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class ApiService {
  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json; charset=utf-8',
      'X-API-KEY': environment.apiKey,
    }),
  };

  constructor(private http: HttpClient) {}

  getCustomerList(): Observable<any[]> {
    return this.http.get<any[]>(
      environment.apiUrl + 'Customers/GetAll',
      this.httpOptions
    );
  }

  addCustomer(customer: any): Observable<any> {
    return this.http.post<any>(
      environment.apiUrl + 'Customers/Add',
      customer,
      this.httpOptions
    );
  }

  updateCustomer(customer: any): Observable<any> {
    return this.http.put<any>(
      environment.apiUrl + 'Customers/Update',
      customer,
      this.httpOptions
    );
  }

  deleteCustomer(Id: number): Observable<number> {
    return this.http.delete<number>(
      environment.apiUrl + 'Customers/Delete/Id=' + Id,
      this.httpOptions
    );
  }
}
