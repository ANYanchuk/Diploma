import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Errand } from '../../models/errand.model';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { UsersService } from '../../services/users.service';

@Component({
  selector: 'app-completed-report-form',
  templateUrl: './completed-report-form.component.html',
  styleUrls: ['./completed-report-form.component.scss'],
})
export class CompletedReportFormComponent implements OnInit {
  readonly reportForm = this._fb.group({
    comment: this._fb.control(''),
  });

  uploadedFile: File | null = null;

  constructor(
    @Inject(MAT_DIALOG_DATA) public readonly errand: Errand,
    private readonly _ref: MatDialogRef<CompletedReportFormComponent>,
    private readonly _fb: FormBuilder,
    private readonly _usersService: UsersService,
  ) {}

  ngOnInit(): void {}

  submit(): void {
    const formValue = this.reportForm.value;
    this._usersService
      .uploadReport(this.errand.id, formValue.comment, this.uploadedFile)
      .subscribe((report) => {
        this._ref.close(report);
      });
  }

  onFileUpload($event: Event): void {
    this.uploadedFile = ($event.target as HTMLInputElement).files[0];
  }
}
