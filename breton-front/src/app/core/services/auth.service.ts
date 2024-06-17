import { Injectable } from '@angular/core';
import { jwtDecode } from 'jwt-decode';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor() {}

  getToken(key: string) {
    return localStorage.getItem(key);
  }

  setToken(key: string, value: string) {
    localStorage.setItem(key, value);
  }

  removeToken(key: string) {
    localStorage.removeItem(key)
  }

  isTokenExpired(token: string): boolean {
    try {
      const decoded: any = jwtDecode(token);
      if (decoded.exp * 1000 < Date.now()) {
        return true;
      } else {
        return false;
      }
    } catch (error) {
      return true;
    }
  }

}
