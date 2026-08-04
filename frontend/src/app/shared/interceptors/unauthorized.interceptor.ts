import { catchError, from, switchMap, throwError } from 'rxjs';
import { HttpInterceptorFn } from '@angular/common/http';
import { AuthClient } from '../clients/auth/auth.client';
import { inject } from '@angular/core';

export const unauthorizedInterceptor: HttpInterceptorFn = (request, next) => {
    const authClient = inject(AuthClient);

    return next(request).pipe(
        catchError((error) => {
            const result = error.error;

            if (result.message !== "NOT_AUTHORIZED" || error.url.includes("/auth/refresh"))
                return throwError(() => error);

            return from(authClient.refresh()).pipe(
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
