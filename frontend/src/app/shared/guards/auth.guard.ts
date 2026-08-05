import { AuthService } from '../../features/auth/auth.service';
import { CanActivateFn, Router } from '@angular/router';
import { inject } from '@angular/core';

export const authGuard: CanActivateFn = async (route, state) => {
    const authService = inject(AuthService);
    const router = inject(Router);

    if (authService.setAuthorized() !== null)
        return !!authService.setAuthorized();

    if (!authService.isAuthorized())
        await authService.authorize();

    return authService.isAuthorized() || router.createUrlTree(["/login"]);
};
