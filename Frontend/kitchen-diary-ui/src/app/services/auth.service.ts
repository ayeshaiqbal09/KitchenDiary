import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable} from 'rxjs';
import { tap } from 'rxjs/operators';
import { LoginRequest } from '../models/login';
import { RegisterRequest } from '../models/register';
import { AuthResponse } from '../models/auth-response';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private apiUrl = 'http://localhost:5281/api/auth';

  constructor(private http: HttpClient) { }

  register(registerRequest: RegisterRequest): Observable<void> {
    return this.http.post<void>(
      `${this.apiUrl}/register`,
      registerRequest
    );
    
  }

  login(loginRequest: LoginRequest): Observable<AuthResponse> {

  return this.http.post<AuthResponse>(
    `${this.apiUrl}/login`,
    loginRequest
  ).pipe(

    tap(response => {

  localStorage.setItem('token', response.token);
  localStorage.setItem('fullName', response.fullName);

})

  );

}
  getToken(): string | null {
    return localStorage.getItem('token');
  }
  
    logout(): void {

  localStorage.removeItem('token');
  localStorage.removeItem('fullName');

}
  
  isLoggedIn(): boolean {
    return this.getToken() !== null;
  }

  getFullName(): string | null {
  return localStorage.getItem('fullName');
}

}