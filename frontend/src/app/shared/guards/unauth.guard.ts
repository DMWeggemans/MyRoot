import { AuthService } from '../../features/auth/auth.service';
import { CanActivateFn, Router } from '@angular/router';
import { inject } from '@angular/core';

export const unauthGuard: CanActivateFn = async (route, state) => {
  const authService = inject(AuthService);
  const router = inject(Router);

  if (authService.isAuthorized())
    return router.createUrlTree(["/dashboard"]);

  await authService.authorize();
  return !authService.isAuthorized() || router.createUrlTree(["/dashboard"]);
};