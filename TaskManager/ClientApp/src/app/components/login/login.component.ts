import { Component, OnInit } from '@angular/core';
import { AuthService, LoginResult } from '../../services/auth.service';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent implements OnInit {
  readonly loginForm = this._fb.group({
    email: this._fb.control('', Validators.required),
    password: this._fb.control('', Validators.required),
  });

  constructor(
    private readonly _auth: AuthService,
    private readonly _fb: FormBuilder,
    private readonly _router: Router,
  ) {}

  ngOnInit(): void {}

  async login() {
    if (!this.loginForm.valid) {
      return;
    }
    const formValue = this.loginForm.value;
    const result = await this._auth.login(formValue.email, formValue.password);
    if (result === LoginResult.SUCCESS) {
      await this._router.navigateByUrl('/errands');
    }
  }
}
