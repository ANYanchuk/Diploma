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

@Component({
  selector: 'app-roles',
  templateUrl: './roles.component.html',
  styleUrls: ['./roles.component.scss']
})
export class RolesComponent implements OnInit {

  private readonly _rolesLoadTrigger = new Subject();

  readonly UserRole = UserRole;

  readonly roles$: Observable<string[]>;
  readonly authUserData: AuthUserData;

  constructor(
    private readonly _dialog: MatDialog,
    private readonly _rolesService: RolesService,
    private readonly _auth: AuthService,
  ) {
    this.roles$ = this._rolesLoadTrigger.pipe(
      startWith(0),
      switchMap(() => this._rolesService.getAll()),
    );
    this.authUserData = this._auth.user;
  }

  ngOnInit(): void { }

  openRoleForm() {
    this._dialog
      .open(RoleFormComponent, {
        minWidth: 400,
      })
      .afterClosed()
      .subscribe(() => {
        this._rolesLoadTrigger.next();
      });
  }

  deleteRole(role: string) {
    this._rolesService.delete(role).subscribe(() => this._rolesLoadTrigger.next());
  }
}
