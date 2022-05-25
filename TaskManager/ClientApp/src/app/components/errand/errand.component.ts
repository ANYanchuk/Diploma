import { Component, Input, OnInit } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import {UserRole} from "../models/user.model";

@Component({
  selector: 'app-errand',
  templateUrl: './errand.component.html',
  styleUrls: ['./errand.component.css'],
})
export class ErrandComponent implements OnInit {
  @Input() options: any;
  role = this.auth.role;
  UserRole = UserRole;
  constructor(private auth: AuthService) {}

  ngOnInit(): void {}
}
