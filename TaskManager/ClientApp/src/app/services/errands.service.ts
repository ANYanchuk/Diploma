import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ErrandDto } from '../dto/errand.dto';
import { Observable } from 'rxjs';
import { Errand } from '../models/errand.model';
import { AuthService } from './auth.service';
import { UsersService } from './users.service';
import { UserRole } from '../models/user.model';

@Injectable({
  providedIn: 'root',
})
export class ErrandsService {
  private readonly _url = `/api/Errands`;

  constructor(
    private readonly _http: HttpClient,
    private readonly _auth: AuthService,
    private readonly _usersService: UsersService,
  ) {}

  getAll(): Observable<Errand[]> {
    const role = this._auth.role;
    if (role === UserRole.LEAD) {
      return this._http.get<Errand[]>(this._url);
    }
    return this._usersService.getAssignedErrands();
  }

  create(dto: ErrandDto): Observable<Errand> {
    return this._http.post<Errand>(this._url, dto);
  }

  update(id: number, dto: ErrandDto): Observable<Errand> {
    return this._http.put<Errand>(`${this._url}/${id}`, dto);
  }
}
