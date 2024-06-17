import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from '../../../../environment/environment';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { RegisterRequest } from '../models/register/RegisterRequest';

const httpOptions = { headers: new HttpHeaders({ 'Content-Type': "application/json" }) }

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  private readonly baseURL: string = environment.apiAccountURL;

  constructor(private http: HttpClient) { }

  authenticate(email: string, password: string): Observable<string> {
    return this.http.post<string>(`${this.baseURL}/authenticate`, { email, password }, { ...httpOptions, responseType: 'text' as 'json' })
  }

  register(register: RegisterRequest): Observable<any> {
    return this.http.post(`${this.baseURL}/register`, register, httpOptions)
  }

  confirmEmail(emailToken: string): Observable<any> {
    return this.http.get(`${this.baseURL}/confirm-email/${emailToken}`);
  }
}
