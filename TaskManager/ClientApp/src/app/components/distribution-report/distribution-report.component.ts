import { Component, OnInit } from '@angular/core';
import { UserRole } from '../../models/user.model';
import { errands } from '../../mock-data/errands';
import { UsersService } from '../../services/users.service';
import { FilesService } from '../../services/files.service';
import { FormBuilder, Validators } from '@angular/forms';

@Component({
  selector: 'app-distribution-report',
  templateUrl: './distribution-report.component.html',
  styleUrls: ['./distribution-report.component.scss'],
})
export class DistributionReportComponent implements OnInit {
  readonly errandsInfo$;
  readonly users$;

  readonly form = this._fb.group({
    from: this._fb.control(null),
    to: this._fb.control(null),
    userId: this._fb.control(null, Validators.required),
  });

  get userId(): number | null {
    return this.form.value.userId;
  }

  constructor(
    private readonly _usersService: UsersService,
    private readonly _filesService: FilesService,
    private readonly _fb: FormBuilder,
  ) {
    this.errandsInfo$ = this._usersService.getErrandsInfo();
    this.users$ = this._usersService.getAll();
  }

  ngOnInit() {}

  export() {
    const value = this.form.value;
    this._filesService.exportUserInfo({ userId: value.userId });
  }
}
