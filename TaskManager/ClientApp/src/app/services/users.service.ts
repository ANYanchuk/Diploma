import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { User } from '../models/user.model';
import { Errand } from '../models/errand.model';
import { AuthService } from './auth.service';
import { Report } from '../models/report.model';

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

  uploadReport(
    errandId: number,
    comment: string,
    file: File,
  ): Observable<Report> {
    const formData = new FormData();
    formData.set('Comment', comment);
    formData.set('Files', file);
    return this._http.post<Report>(
      `${this._url}/errands/${errandId}/report`,
      formData,
    );
  }

  deleteReport(errandId: number): Observable<void> {
    return this._http.delete<void>(`${this._url}/errands/${errandId}/report`);
  }

  getErrandsInfo(): Observable<User[]> {
    return this._http.get<User[]>(`${this._url}/info`);
  }
}
