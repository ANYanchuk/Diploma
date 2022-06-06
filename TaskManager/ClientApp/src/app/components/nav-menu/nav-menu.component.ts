import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService, AuthUserData } from '../../services/auth.service';
import { UserRole } from '../../models/user.model';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.scss'],
})
export class NavMenuComponent implements OnInit {
  readonly UserRole = UserRole;
  private readonly _reportPaths = [
    '/distribution-report',
    '/progress-report-errand',
    '/progress-report-user',
  ];

  authUserData: AuthUserData = null;

  constructor(
    private readonly _router: Router,
    private readonly _auth: AuthService,
  ) {}

  ngOnInit() {
    this.subscribeOnAuthChange();
  }

  subscribeOnAuthChange(): void {
    this._auth.user$.subscribe(u => {
      this.authUserData = u;
    });
  }

  isReportPageOpened(): boolean {
    return this._reportPaths.includes(this._router.url);
  }
}
