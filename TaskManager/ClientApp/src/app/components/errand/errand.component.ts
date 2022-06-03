import { Component, Input, OnInit } from '@angular/core';
import {AuthService, AuthUserData} from '../../services/auth.service';
import { UserRole } from '../../models/user.model';
import { MatDialog } from '@angular/material/dialog';
import { ErrandFormComponent } from '../errand-form/errand-form.component';
import { Errand } from '../../models/errand.model';

@Component({
  selector: 'app-errand',
  templateUrl: './errand.component.html',
  styleUrls: ['./errand.component.scss'],
})
export class ErrandComponent implements OnInit {
  @Input() errand: Errand;

  readonly UserRole = UserRole;
  readonly authUserData: AuthUserData;

  constructor(
    private readonly _auth: AuthService,
    private readonly _dialog: MatDialog,
  ) {
    this.authUserData = this._auth.user;
  }

  ngOnInit(): void {}

  openErrandForm() {
    this._dialog.open(ErrandFormComponent, {
      minWidth: 600,
      data: this.errand.id,
    });
  }
}
