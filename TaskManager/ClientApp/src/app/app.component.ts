import { Component, OnInit } from '@angular/core';
import { AuthService } from './services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent implements OnInit {
  TITLE: Record<string, string> = {
    '/errands': 'Доручення',
    '/distribution-report': 'Відомість розподілу доручень',
    '/progress-report': 'Відомість виконання доручень',
  };

  get title() {
    return this.TITLE[this.router.url];
  }

  currentUser: null | any;

  constructor(private auth: AuthService, private router: Router) {}

  ngOnInit() {
    this.subscribeOnUserChanges();
  }

  subscribeOnUserChanges() {
    this.auth.user$.subscribe(u => {
      this.currentUser = u;
    });
  }
}
