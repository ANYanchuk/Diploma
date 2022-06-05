import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { ErrandDto } from '../dto/errand.dto';
import { Observable } from 'rxjs';
import { Errand } from '../models/errand.model';
import { AuthService } from './auth.service';
import { UsersService } from './users.service';
import { UserRole } from '../models/user.model';

const httpOptions = {
  headers: new HttpHeaders({'Content-Type': 'application/json'})
}

@Injectable({
  providedIn: 'root'
})


export class RolesService {

  private readonly _url = `/api/Roles`;

  constructor(
    private readonly _http: HttpClient,
    private readonly _auth: AuthService,
    private readonly _usersService: UsersService,
  ) { }



  getAll(): Observable<string[]> {
    const role = this._auth.role;
    return this._http.get<string[]>(this._url);
  }

  create(role: string): Observable<string> {
    return this._http.post<string>(this._url, JSON.stringify(role), httpOptions);
  }

  delete(role: string): Observable<any> {
    return this._http.delete(this._url + '/' + role);
  }
}
