import { Component, Input, OnInit } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { UserRole } from '../models/user.model';
import { MatDialog } from '@angular/material/dialog';
import { ErrandFormComponent } from '../errand-form/errand-form.component';

@Component({
  selector: 'app-errand',
  templateUrl: './errand.component.html',
  styleUrls: ['./errand.component.css'],
})
export class ErrandComponent implements OnInit {
  @Input() options: any;
  role = this.auth.role;
  UserRole = UserRole;
  constructor(private auth: AuthService, private dialog: MatDialog) {}

  ngOnInit(): void {}

  openErrandForm() {
    this.dialog.open(ErrandFormComponent, {
      minWidth: 600,
    });
  }
}
