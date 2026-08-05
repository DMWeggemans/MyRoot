import { catchError, from, switchMap, throwError } from 'rxjs';
import { AuthService } from '../../features/auth/auth.service';
import { HttpErrorResponse, HttpInterceptorFn } from '@angular/common/http';
import { Result } from '../clients/base.types';
import { inject } from '@angular/core';

export const unauthorizedInterceptor: HttpInterceptorFn = (request, next) => {
    const authService = inject(AuthService);

    if (request.headers.has('X-HAS-RETRIED'))
        return next(request);

    return next(request).pipe(
        catchError((error: HttpErrorResponse) => {
            const result = error?.error as Result<unknown> | null;
            
            if (result?.message !== "NOT_AUTHORIZED" || error.url?.includes("/auth/refresh"))
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
                }),
                catchError(() => throwError(() => error))
            );
        })
    );
};
