import { catchError, from, switchMap, throwError } from 'rxjs';
import { AuthService } from '../../features/auth/auth.service';
import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';

export const unauthorizedInterceptor: HttpInterceptorFn = (request, next) => {
    const authService = inject(AuthService);

    return next(request).pipe(
        catchError((error) => {
            const result = error.error;

            if (result.message !== "NOT_AUTHORIZED" || error.url.includes("/auth/refresh"))
                return throwError(() => error);

            return from(authService.refresh()).pipe(
                switchMap((value) => {
                    if (!value.succeeded)
                        return throwError(() => error);

                    const newRequest = request.clone({
                        setHeaders: {
                            "X-HAS-RETRIED": "true"
                        }
                    });

                    return next(newRequest);
                })
            );
        })
    );
};
