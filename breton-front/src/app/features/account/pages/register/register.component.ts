import { Component, inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AccountService } from '../../services/account.service';
import { RegisterRequest } from '../../models/register/RegisterRequest';
import { catchError, of, tap } from 'rxjs';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {
  formBuilder: FormBuilder = inject(FormBuilder);
  accountService: AccountService = inject(AccountService);
  router: Router = inject(Router);
  registerForm: FormGroup = this.formBuilder.group({
    name: ["", Validators.required],
    email: ["", Validators.required],
    password: ["", Validators.required],
    confirmPassword: ["", Validators.required],
  });

  get isValid(): boolean {
    return this.registerForm.valid
  }

  onRegister() {
    const register: RegisterRequest = this.registerForm.value;

    this.accountService.register(this.registerForm.value).pipe(
        catchError((error) => {
          console.log(error);
          alert("error")
          return of();
        }),
        tap(x => {
          alert("User registered successfully. Confirm your email");
          this.registerForm.reset();
          this.router.navigate([''])
        })
      ).subscribe()
  }
}
