import { Component, OnInit } from '@angular/core';
import { UserRole } from '../../models/user.model';
import { ErrandsService } from '../../services/errands.service';
import { Observable } from 'rxjs';
import { Errand } from '../../models/errand.model';
import { FormBuilder } from '@angular/forms';
import { FilesService } from '../../services/files.service';

@Component({
  selector: 'app-progress-report',
  templateUrl: './progress-report.component.html',
  styleUrls: ['./progress-report.component.scss'],
})
export class ProgressReportComponent implements OnInit {
  readonly reportEntries$: Observable<Errand[]>;
  readonly form = this._fb.group({
    since: this._fb.control(null),
    till: this._fb.control(null),
  });

  constructor(
    private readonly _errandsService: ErrandsService,
    private readonly _fb: FormBuilder,
    private readonly _filesService: FilesService,
  ) {
    this.reportEntries$ = this._errandsService.getInfo();
  }

  ngOnInit(): void {}

  export() {
    const formValue = this.form.value;
    this._filesService.exportErrandsInfo(formValue.since, formValue.till);
  }
}
