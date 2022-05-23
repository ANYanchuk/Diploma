import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private readonly authUser = new BehaviorSubject<any>(null);

  get user$() {
    return this.authUser.asObservable();
  }

  get user() {
    return this.authUser.value;
  }

  constructor(private router: Router) {
    this.checkStorage();
  }

  checkStorage() {
    const token = localStorage.getItem('token');
    if (token) {
      const user = { id: 1, name: 'Андрій' };
      this.authUser.next(user);
      return;
    }
    this.router.navigateByUrl('/login');
  }

  login() {
    const user = { id: 1, name: 'Андрій' };
    this.authUser.next(user);
  }

  logout() {
    this.authUser.next(null);
  }
}
