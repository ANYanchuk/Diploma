import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { User } from '../models/user.model';
import { Errand } from '../models/errand.model';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root',
})
export class UsersService {
  private readonly _url = `/api/Users`;

  constructor(
    private readonly _http: HttpClient,
    private readonly _auth: AuthService,
  ) {}

  getAll(): Observable<User[]> {
    return this._http.get<User[]>(this._url);
  }

  getAssignedErrands(): Observable<Errand[]> {
    const id = this._auth.user?.id;
    if (!id) {
      return of([]);
    }
    return this._http.get<Errand[]>(`${this._url}/${id}/errands`);
  }

  // uploadReport(errandId: number, comment: string, file: File): Observable<any> {
  //   const formData = new FormData();
  //   formData.set('ErrandId', errandId.toString(10));
  //   formData.set('Comment', comment);
  //   formData.set('Files', file);
  // }
}
