import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { AuthService, AuthUserData } from '../../services/auth.service';
import { UserRole } from '../../models/user.model';
import { MatDialog } from '@angular/material/dialog';
import { ErrandFormComponent } from '../errand-form/errand-form.component';
import { Errand } from '../../models/errand.model';
import { CompletedReportFormComponent } from '../completed-report-form/completed-report-form.component';
import { UsersService } from '../../services/users.service';
import { ErrandsService } from '../../services/errands.service';

@Component({
  selector: 'app-errand',
  templateUrl: './errand.component.html',
  styleUrls: ['./errand.component.scss'],
})
export class ErrandComponent implements OnInit {
  @Output() deleted = new EventEmitter();
  @Input() errand: Errand;

  readonly UserRole = UserRole;
  readonly authUserData: AuthUserData;

  constructor(
    private readonly _auth: AuthService,
    private readonly _dialog: MatDialog,
    private readonly _usersService: UsersService,
    private readonly _errandsService: ErrandsService,
  ) {
    this.authUserData = this._auth.user;
  }

  ngOnInit(): void {}

  openEditForm() {
    this._dialog
      .open(ErrandFormComponent, {
        minWidth: 600,
        data: this.errand,
      })
      .afterClosed()
      .subscribe(errand => {
        if (errand) {
          this.errand = errand;
        }
      });
  }

  openReportForm() {
    this._dialog
      .open(CompletedReportFormComponent, {
        minWidth: 600,
        data: this.errand,
      })
      .afterClosed()
      .subscribe(report => {
        this.errand.report = report;
      });
  }

  deleteReport() {
    this._usersService.deleteReport(this.errand.id).subscribe(() => {
      this.errand.report = null;
    });
  }

  delete() {
    this._errandsService.delete(this.errand.id).subscribe(() => {
      this.deleted.emit();
    });
  }
}
