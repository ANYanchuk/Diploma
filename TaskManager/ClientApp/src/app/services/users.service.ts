import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {User} from "../models/user.model";

@Injectable({
  providedIn: 'root'
})
export class UsersService {
  private readonly _url = `/api/Users`

  constructor(private readonly _http: HttpClient) { }

  getAll(): Observable<User[]> {
    return this._http.get<User[]>(this._url);
  }
}
