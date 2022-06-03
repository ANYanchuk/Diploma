import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.scss'],
})
export class NavMenuComponent {
  private readonly _reportPaths = [
    '/distribution-report',
    '/progress-report-errand',
    '/progress-report-user',
  ];
  constructor(private readonly _router: Router) {}

  isReportPageOpened(): boolean {
    return this._reportPaths.includes(this._router.url);
  }
}
