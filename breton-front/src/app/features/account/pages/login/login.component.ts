import { Component, Inject, OnInit, inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AccountService } from '../../services/account.service';
import { ActivatedRoute, Route, Router } from '@angular/router';
import { catchError, of, tap } from 'rxjs';
import { AuthService } from 'src/app/core/services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  formBuilder: FormBuilder = inject(FormBuilder);
  accountService: AccountService = inject(AccountService);
  authService: AuthService = inject(AuthService);
  router: Router = inject(Router);

  loginForm: FormGroup = this.formBuilder.group({
    email: ["",Validators.required],
    password: ["",Validators.required]
  });

  get isValid(): boolean { return this.loginForm.valid }


  ngOnInit(): void {
    if(this.authService.getToken("token")) {
      this.router.navigate(['customer']);
    }
  }

  onLogin() {
    if(!this.loginForm.valid) {
      alert("fillout every field");
      return;
    }

    this.accountService.authenticate(
      this.loginForm.get("email")?.value,
      this.loginForm.get("password")?.value).pipe(
        catchError((error) => {
          console.log(error);
          alert("error")
          return of();
        }),
        tap(response => {
          const token: string = response;
          this.authService.setToken("token", token);
          this.router.navigate(['customer']);
        })
      ).subscribe()
  }
}
