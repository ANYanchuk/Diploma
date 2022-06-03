import { Component, OnInit } from '@angular/core';
import { AuthService } from './services/auth.service';
import { Router } from '@angular/router';
import { User } from './models/user.model';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent implements OnInit {
  private readonly _titles: Record<string, string> = {
    '/login': 'Вхід в систему',
    '/errands': 'Доручення',
    '/distribution-report': 'Відомість розподілу доручень',
    '/progress-report': 'Відомість виконання доручень',
    '/progress-report-errand': 'Відомість по дорученням',
    '/progress-report-user': 'Видомість по виконавцям',
  };

  get title() {
    return this._titles[this._router.url];
  }

  authenticatedUser: User | null;

  constructor(
    private readonly _auth: AuthService,
    private readonly _router: Router,
  ) {}

  ngOnInit() {
    this.subscribeOnUserChanges();
  }

  subscribeOnUserChanges() {
    this._auth.user$.subscribe(u => {
      this.authenticatedUser = u;
    });
  }

  logout() {
    this._auth.logout();
  }
}
