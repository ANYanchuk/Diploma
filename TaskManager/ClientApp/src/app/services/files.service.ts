import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { saveAs } from 'file-saver';
import { DatePipe } from '@angular/common';

@Injectable({
  providedIn: 'root',
})
export class FilesService {
  private readonly _url = '/api/Files';

  constructor(
    private readonly _http: HttpClient,
    private readonly _datePipe: DatePipe,
  ) {}

  exportUserInfo(options: any): void {
    const params = new HttpParams().set('userId', options.userId);
    this._http
      .get(`${this._url}/user-info`, { responseType: 'blob', params })
      .subscribe(blob => {
        saveAs(blob, 'Відомість виконання за виконавцями');
      });
  }

  exportErrandsInfo(since: Date, till: Date): void {
    const params = new HttpParams()
      .set('since', this._datePipe.transform(since, 'MM.dd.yyy'))
      .set('till', this._datePipe.transform(till, 'MM.dd.yyy'));
    this._http
      .get(`${this._url}/errand-info`, { responseType: 'blob', params })
      .subscribe(blob => {
        saveAs(blob, 'Відомість виконання за виконавцями');
      });
  }

  exportDistributionInfo(since: Date, till: Date): void {
    const params = new HttpParams()
      .set('since', this._datePipe.transform(since, 'MM.dd.yyy'))
      .set('till', this._datePipe.transform(till, 'MM.dd.yyy'));
    this._http
      .get(`${this._url}/distribution-info`, { responseType: 'blob', params })
      .subscribe(blob => {
        saveAs(blob, 'Відомість розподілу доручень');
      });
  }
}
