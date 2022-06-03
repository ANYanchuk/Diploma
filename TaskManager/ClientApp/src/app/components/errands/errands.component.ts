import { Component, OnInit } from '@angular/core';
import { errands } from '../../mock-data/errands';
import { MatDialog } from '@angular/material/dialog';
import { ErrandFormComponent } from '../errand-form/errand-form.component';
import { ErrandsService } from '../../services/errands.service';
import { Observable } from 'rxjs';
import { Errand } from '../../models/errand.model';

@Component({
  selector: 'app-errands',
  templateUrl: './errands.component.html',
  styleUrls: ['./errands.component.scss'],
})
export class ErrandsComponent implements OnInit {
  readonly errands$: Observable<Errand[]>;

  constructor(
    private readonly _dialog: MatDialog,
    private readonly _errandsService: ErrandsService,
  ) {
    this.errands$ = this._errandsService.getAll();
  }

  ngOnInit(): void {}

  openErrandsForm() {
    this._dialog.open(ErrandFormComponent, {
      minWidth: 600,
    });
  }
}
