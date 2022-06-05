import { Component, OnInit } from '@angular/core';
import { errands } from '../../mock-data/errands';
import { MatDialog } from '@angular/material/dialog';
import { ErrandFormComponent } from '../errand-form/errand-form.component';
import { ErrandsService } from '../../services/errands.service';
import { Observable, Subject } from 'rxjs';
import { Errand } from '../../models/errand.model';
import { startWith, switchMap } from 'rxjs/operators';
import { AuthService, AuthUserData } from '../../services/auth.service';
import { UserRole } from "../../models/user.model";
import { RolesService } from 'src/app/services/roles.service';
import { RoleFormComponent } from '../role-form/role-form.component';
import { ReportFormatsService } from 'src/app/services/report-formats.service';
import { ReportFormatFormComponent } from '../report-format-form/report-format-form.component';

@Component({
  selector: 'app-report-formats',
  templateUrl: './report-formats.component.html',
  styleUrls: ['./report-formats.component.scss']
})
export class ReportFormatsComponent implements OnInit {

  private readonly _formatsLoadTrigger = new Subject();

  readonly UserRole = UserRole;

  readonly formats$: Observable<string[]>;
  readonly authUserData: AuthUserData;

  constructor(
    private readonly _dialog: MatDialog,
    private readonly _reportFormatService: ReportFormatsService,
    private readonly _auth: AuthService,
  ) {
    this.formats$ = this._formatsLoadTrigger.pipe(
      startWith(0),
      switchMap(() => this._reportFormatService.getAll()),
    );
    this.authUserData = this._auth.user;
  }

  ngOnInit(): void { }

  openRoleForm() {
    this._dialog
      .open(ReportFormatFormComponent, {
        minWidth: 400,
      })
      .afterClosed()
      .subscribe(() => {
        this._formatsLoadTrigger.next();
      });
  }

  deleteRole(role: string) {
    this._reportFormatService
      .delete(role)
      .subscribe(() => this._formatsLoadTrigger.next());
  }

}
