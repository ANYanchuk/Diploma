import { Component } from '@angular/core';
import { AuthService } from './services/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent {
  title = 'app';

  currentUser: null | any;

  constructor(private auth: AuthService) {}

  subscribeOnUserChanges() {
    this.auth.user$.subscribe(u => {
      this.currentUser = u;
    });
  }
}
