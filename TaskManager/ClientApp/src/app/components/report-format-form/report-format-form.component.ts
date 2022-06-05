import { Component, Inject, Input, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { ErrandsService } from '../../services/errands.service';
import { FormBuilder, Validators } from '@angular/forms';
import { Errand, ErrandType, ReportFormat } from '../../models/errand.model';
import { ErrandDto } from '../../dto/errand.dto';
import { UsersService } from '../../services/users.service';
import { Observable } from 'rxjs';
import { RolesService } from 'src/app/services/roles.service';
import { ReportFormatsComponent } from '../report-formats/report-formats.component';
import { ReportFormatsService } from 'src/app/services/report-formats.service';

@Component({
  selector: 'app-report-format-form',
  templateUrl: './report-format-form.component.html',
  styleUrls: ['./report-format-form.component.scss']
})
export class ReportFormatFormComponent implements OnInit {

  readonly roleForm = this._fb.group({
    role: this._fb.control('', Validators.required)
  });

  constructor(private readonly _reportFormatService: ReportFormatsService,
    private readonly _fb: FormBuilder,
    private readonly _usersService: UsersService,
    private readonly _ref: MatDialogRef<ReportFormatsComponent>) { }

  submit() {
    this._reportFormatService.create(this.roleForm.value.role).subscribe((role) => {
      this._ref.close(role);
    });
  }

  ngOnInit(): void {
  }
}
