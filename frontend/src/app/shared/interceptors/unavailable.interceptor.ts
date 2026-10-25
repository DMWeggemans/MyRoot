import { HttpInterceptorFn, HttpErrorResponse } from '@angular/common/http';
import { catchError, throwError } from 'rxjs';

export const unavailableInterceptor: HttpInterceptorFn = (request, next) => {
    return next(request).pipe(
        catchError((error: unknown) => {
            if (!(error instanceof HttpErrorResponse) || error.status !== 0)
                return throwError(() => error);

            const newError = new HttpErrorResponse({
                url: error.url ?? undefined,
                headers: error.headers,
                status: 503,
                error: {
                    succeeded: false,
                    message: 'SERVER_UNREACHABLE',
                    value: null
                },
            });
            
            return throwError(() => newError);
        })
    );
};
