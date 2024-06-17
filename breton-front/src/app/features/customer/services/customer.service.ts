import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { environment } from 'src/environment/environment';
import { CreateCustomerRequest } from '../models/Request/CreateCustomerRequest';
import { UpdateCustomerRequest } from '../models/Request/UpdateCustomerRequest';
import { IAddressResponse } from '../models/Response/IAddressResponse';
import { ICustomerResponse } from '../models/Response/ICustomerResponse';
import { IPageResponse } from '../models/Response/IPageResponse';
import { GetCustomersRequest } from '../models/Request/GetCustomersRequest';

const httpOptions = { headers: new HttpHeaders({ 'Content-Type': "application/json" }) }

@Injectable({
  providedIn: 'root'
})
export class CustomerService {

  private readonly baseURL: string = environment.apiCustomerURL;

  constructor(private http: HttpClient) { }

  createCustomer(customer: CreateCustomerRequest): Observable<void> {
    return this.http.post<void>(this.baseURL, customer, { ...httpOptions, responseType: 'text' as 'json' });
  }

  updateCustomer(customer: UpdateCustomerRequest): Observable<void> {
    return this.http.put<void>(`${this.baseURL}/${customer.id}`, customer, { ...httpOptions, responseType: 'text' as 'json' });
  }

  getAddressByPostalCode(postalCode: string): Observable<IAddressResponse> {
    return this.http.get<IAddressResponse>(`${this.baseURL}/postal-code/${postalCode}`);
  }

  deleteCustomer(id: number): Observable<void> {
    return this.http.delete<void>(`${this.baseURL}/${id}`);
  }

  getCustomerById(id: number): Observable<ICustomerResponse> {
    return this.http.get<ICustomerResponse>(`${this.baseURL}/${id}`);
  }

  getCustomers(search: GetCustomersRequest): Observable<IPageResponse<ICustomerResponse>> {
    let params = new HttpParams();

    for (const [key, value] of Object.entries(search)) {
      if (value) {
        params = params.append(key, value.toString());
      }
    }

    return this.http.get<IPageResponse<ICustomerResponse>>(this.baseURL, { params: params });
  }
}
