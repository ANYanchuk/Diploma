import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CreateErrandDto } from '../dto/create-errand.dto';
import { Observable } from 'rxjs';
import { Errand } from '../models/errand.model';

@Injectable({
  providedIn: 'root',
})
export class ErrandsService {
  private readonly _url = `/api/Errands`;

  constructor(private readonly _http: HttpClient) {}

  getAll(): Observable<Errand[]> {
    return this._http.get<Errand[]>(this._url);
  }

  create(dto: CreateErrandDto): Observable<Errand> {
    return this._http.post<Errand>(this._url, dto);
  }
}
