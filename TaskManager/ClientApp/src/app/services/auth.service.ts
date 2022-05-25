import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { Router } from '@angular/router';
import { UserRole } from '../components/models/user.model';

const mockLead = { id: 1, firstName: 'Андрій', lastName: 'Янчук', role: UserRole.LEAD };
const mockWorker = { id: 1, firstName: 'Владислав', lastName: 'Григорович', role: UserRole.LECTURER };

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
    this.authUser.next(mockLead);
    this.router.navigateByUrl('/errands');
  }

  logout() {
    this.authUser.next(null);
  }
}
