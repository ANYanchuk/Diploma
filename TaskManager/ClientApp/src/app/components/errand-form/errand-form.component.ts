import { Component, Inject, Input, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { ErrandsService } from '../../services/errands.service';
import { FormBuilder, Validators } from '@angular/forms';
import { Errand, ErrandType, ReportFormat } from '../../models/errand.model';
import { ErrandDto } from '../../dto/errand.dto';
import { UsersService } from '../../services/users.service';
import { Observable } from 'rxjs';
import { User } from '../../models/user.model';

@Component({
  selector: 'app-errand-form',
  templateUrl: './errand-form.component.html',
  styleUrls: ['./errand-form.component.scss'],
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
    @Inject(MAT_DIALOG_DATA) public readonly errand: Errand | null,
    private readonly _ref: MatDialogRef<ErrandFormComponent>,
    private readonly _errandsService: ErrandsService,
    private readonly _fb: FormBuilder,
    private readonly _usersService: UsersService,
  ) {
    this.mode = this.errand ? 'update' : 'create';
    this.users$ = this._usersService.getAll();
  }

  ngOnInit(): void {
    if (this.mode === 'update') {
      this.populateForm();
    }
  }

  populateForm(): void {
    this.errandForm.setValue({
      title: this.errand.title,
      body: this.errand.body,
      type: this.errand.type,
      deadline: this.errand.deadline,
      userIds: this.errand.users.map(u => u.id),
      reportFormat: this.errand.reportFormat,
    });
  }

  create(): void {
    this._errandsService.create(this.constructDto()).subscribe((errand) => {
      this._ref.close(errand);
    });
  }

  update(): void {
    this._errandsService
      .update(this.errand.id, this.constructDto())
      .subscribe(errand => {
        this._ref.close(errand);
      });
  }

  submit(): void {
    this.mode === 'create' ? this.create() : this.update();
  }

  constructDto(): ErrandDto {
    const formValue = this.errandForm.value;
    return {
      title: formValue.title,
      body: formValue.body,
      type: formValue.type,
      deadline: formValue.deadline,
      users: formValue.userIds.map(id => ({ id })),
      reportFormat: formValue.reportFormat,
    };
  }
}
