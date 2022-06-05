import { Component, OnInit } from '@angular/core';
import { errands } from '../../mock-data/errands';
import { MatDialog } from '@angular/material/dialog';
import { ErrandFormComponent } from '../errand-form/errand-form.component';
import { ErrandsService } from '../../services/errands.service';
import { Observable, Subject } from 'rxjs';
import { Errand } from '../../models/errand.model';
import { startWith, switchMap } from 'rxjs/operators';
import { AuthService, AuthUserData } from '../../services/auth.service';
import {UserRole} from "../../models/user.model";

@Component({
  selector: 'app-errands',
  templateUrl: './errands.component.html',
  styleUrls: ['./errands.component.scss'],
})
export class ErrandsComponent implements OnInit {
  private readonly _errandsLoadTrigger = new Subject();

  readonly UserRole = UserRole;

  readonly errands$: Observable<Errand[]>;
  readonly authUserData: AuthUserData;

  constructor(
    private readonly _dialog: MatDialog,
    private readonly _errandsService: ErrandsService,
    private readonly _auth: AuthService,
  ) {
    this.errands$ = this._errandsLoadTrigger.pipe(
      startWith(0),
      switchMap(() => this._errandsService.getAll()),
    );
    this.authUserData = this._auth.user;
  }

  ngOnInit(): void {}

  openErrandsForm() {
    this._dialog
      .open(ErrandFormComponent, {
        minWidth: 600,
      })
      .afterClosed()
      .subscribe(() => {
        this._errandsLoadTrigger.next();
      });
  }
}
