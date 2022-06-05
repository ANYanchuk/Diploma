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
export class ReportFormatsService {

  private readonly _url = `/api/ReportFormats`;

  constructor(
    private readonly _http: HttpClient,
    private readonly _auth: AuthService,
    private readonly _usersService: UsersService,
  ) { }

  getAll(): Observable<string[]> {
    const role = this._auth.role;
    return this._http.get<string[]>(this._url);
  }

  create(format: string): Observable<string> {
    return this._http.post<string>(this._url, JSON.stringify(format), httpOptions);
  }

  delete(format: string): Observable<any> {
    return this._http.delete(this._url + '/' + format);
  }
}
