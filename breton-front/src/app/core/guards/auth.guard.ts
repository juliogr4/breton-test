import { CanActivateFn, Router } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { inject } from '@angular/core';

export const authGuard: CanActivateFn = (route, state) => {
  const router: Router = inject(Router);
  const authService: AuthService = inject(AuthService);
  const token = authService.getToken("token");

  if(token && !authService.isTokenExpired(token)) {
    return true;
  } else {
    authService.removeToken("token");
    router.navigate(['']);
    return false;
  }

};
