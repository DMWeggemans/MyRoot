import { AuthService } from '../../features/auth/auth.service';
import { CanActivateFn, Router } from '@angular/router';
import { inject } from '@angular/core';

export const authGuard: CanActivateFn = async (route, state) => {
    const authService = inject(AuthService);
    const router = inject(Router);

    if (authService.authorized() !== null)
        return !!authService.authorized() || router.createUrlTree(["/login"]);

    if (!authService.isAuthorized())
        await authService.authorize();

    return authService.isAuthorized() || router.createUrlTree(["/login"]);
};
