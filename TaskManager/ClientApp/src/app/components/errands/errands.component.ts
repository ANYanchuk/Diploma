import { Component, OnInit } from '@angular/core';
import { errands } from '../../mock-data/errands';
import { MatDialog } from '@angular/material/dialog';
import { ErrandFormComponent } from '../errand-form/errand-form.component';

@Component({
  selector: 'app-errands',
  templateUrl: './errands.component.html',
  styleUrls: ['./errands.component.css'],
})
export class ErrandsComponent implements OnInit {
  errands = errands;
  constructor(private dialog: MatDialog) {}

  ngOnInit(): void {}

  openErrandsForm() {
    this.dialog.open(ErrandFormComponent, {
      minWidth: 600,
    });
  }
}
