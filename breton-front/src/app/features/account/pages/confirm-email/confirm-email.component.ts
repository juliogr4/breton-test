import { Component, OnInit, inject } from '@angular/core';
import { AccountService } from '../../services/account.service';
import { catchError, map, of, switchMap, tap } from 'rxjs';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-confirm-email',
  templateUrl: './confirm-email.component.html',
  styleUrls: ['./confirm-email.component.css']
})
export class ConfirmEmailComponent implements OnInit {
  accountService: AccountService = inject(AccountService);
  activedRoute : ActivatedRoute = inject(ActivatedRoute);
  message = "";

  ngOnInit(): void {
    this.activedRoute.params.pipe(
      map(param => param["emailToken"]),
      switchMap((emailToken: string | undefined) => {
        if(!!emailToken) {
          return this.accountService.confirmEmail(emailToken).pipe(
            catchError(error => {
              this.message = "Error to confirm the email"
              return of();
            }),
            tap(() => {
              this.message = "Email confirmed successfully"
              alert("Email confirmed successfully")
            })
          )
        }
        return of();
      })
    ).subscribe();
  }

}
