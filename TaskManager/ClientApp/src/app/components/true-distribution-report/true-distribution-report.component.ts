import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { UsersService } from '../../services/users.service';
import { User } from '../../models/user.model';
import { FormBuilder } from '@angular/forms';
import { FilesService } from '../../services/files.service';

@Component({
  selector: 'app-true-distribution-report',
  templateUrl: './true-distribution-report.component.html',
  styleUrls: ['./true-distribution-report.component.scss'],
})
export class TrueDistributionReportComponent implements OnInit {
  readonly errandsInfo$: Observable<User[]>;

  readonly form = this._fb.group({
    from: this._fb.control(null),
    to: this._fb.control(null),
  });

  constructor(
    private readonly _usersService: UsersService,
    private readonly _fb: FormBuilder,
    private readonly _filesService: FilesService,
  ) {
    this.errandsInfo$ = this._usersService.getErrandsInfo();
  }

  ngOnInit(): void {}

  export(): void {
    const value = this.form.value;
    this._filesService.exportDistributionInfo(value.from, value.to);
  }
}
