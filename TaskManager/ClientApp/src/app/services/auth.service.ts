import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { Router } from '@angular/router';
import { UserRole } from '../components/models/user.model';

const mockUser = { id: 1, firstName: 'Андрій', lastName: 'Янчук', role: UserRole.LEAD };

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

  get role() {
    return this.authUser.value?.role || null;
  }

  constructor(private router: Router) {
    this.checkStorage();
  }

  checkStorage() {
    const token = localStorage.getItem('token') || 'asdasdasd';
    if (token) {
      this.login();
      return;
    }
    this.router.navigateByUrl('/login');
  }

  login() {
    this.authUser.next(mockUser);
    this.router.navigateByUrl('/errands');
  }

  logout() {
    this.authUser.next(null);
  }
}
