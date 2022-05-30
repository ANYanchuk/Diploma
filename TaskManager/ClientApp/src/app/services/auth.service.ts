import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import jwtDecode from 'jwt-decode';
import { Router } from '@angular/router';
import { User } from '../models/user.model';
import { HttpClient } from '@angular/common/http';
import { LoginDto } from '../dto/login.dto';

export enum LoginResult {
  SUCCESS = 'SUCCESS',
  ERROR = 'ERROR',
}

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private readonly _url = `/api/auth`;
  private readonly _authUserSubject = new BehaviorSubject<User | null>(null);

  get user$() {
    return this._authUserSubject.asObservable();
  }

  get user() {
    return this._authUserSubject.value;
  }

  get role() {
    return this._authUserSubject.value?.Role || null;
  }

  constructor(
    private readonly _router: Router,
    private readonly _http: HttpClient,
  ) {
    this.checkStorage();
  }

  checkStorage() {
    const token = localStorage.getItem('token');
    if (token) {
      this.decodeAndPropagate(token);
    }
  }

  async login(email: string, password: string): Promise<LoginResult> {
    const loginUrl = `${this._url}/login`;
    const dto: LoginDto = {
      Email: email,
      Password: password,
    };
    return this._http
      .post(loginUrl, dto, {
        responseType: 'text',
      })
      .toPromise()
      .then(token => {
        this.decodeAndPropagate(token);
        return LoginResult.SUCCESS;
      })
      .catch(response => {
        console.error(response.error);
        return LoginResult.ERROR;
      });
  }

  logout() {
    this._authUserSubject.next(null);
    localStorage.removeItem('token');
    this._router.navigateByUrl('/login');
  }

  private decodeAndPropagate(token: string): void {
    const user = jwtDecode<User>(token);
    this._authUserSubject.next(user);
    localStorage.setItem('token', token);
  }
}
