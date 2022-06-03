import { Component, Inject, Input, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { ErrandsService } from '../../services/errands.service';
import { FormBuilder, Validators } from '@angular/forms';
import { ErrandType, ReportFormat } from '../../models/errand.model';
import { CreateErrandDto } from '../../dto/create-errand.dto';
import { UsersService } from '../../services/users.service';
import { Observable } from 'rxjs';
import { User } from '../../models/user.model';

@Component({
  selector: 'app-errand-form',
  templateUrl: './errand-form.component.html',
  styleUrls: ['./errand-form.component.css'],
})
export class ErrandFormComponent implements OnInit {
  readonly ErrandType = ErrandType;
  readonly ReportFormat = ReportFormat;

  readonly mode: 'create' | 'update';

  readonly users$: Observable<User[]>;

  readonly errandForm = this._fb.group({
    title: this._fb.control('', Validators.required),
    body: this._fb.control(''),
    type: this._fb.control(ErrandType.INDIVIDUAL),
    deadline: this._fb.control(null),
    userIds: this._fb.control([]),
    reportFormat: this._fb.control(ReportFormat.TEXT),
  });

  constructor(
    @Inject(MAT_DIALOG_DATA) public errandId: string | null,
    private readonly _ref: MatDialogRef<ErrandFormComponent>,
    private readonly _errandsService: ErrandsService,
    private readonly _fb: FormBuilder,
    private readonly _usersService: UsersService,
  ) {
    this.mode = this.errandId ? 'update' : 'create';
    this.users$ = this._usersService.getAll();
  }

  ngOnInit(): void {}

  create(): void {
    debugger;
    const formValue = this.errandForm.value;
    const dto: CreateErrandDto = {
      title: formValue.title,
      body: formValue.body,
      type: formValue.type,
      deadline: formValue.deadline,
      users: formValue.userIds.map(id => ({ id })),
      reportFormat: formValue.reportFormat,
    };
    this._errandsService.create(dto).subscribe(() => {
      this._ref.close();
    });
  }

  update(): void {}

  submit(): void {
    this.mode === 'create' ? this.create() : this.update();
  }
}
