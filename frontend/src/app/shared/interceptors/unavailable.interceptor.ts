import { HttpInterceptorFn, HttpErrorResponse } from '@angular/common/http';
import { catchError, throwError } from 'rxjs';

export const unavailableInterceptor: HttpInterceptorFn = (request, next) => {
    return next(request).pipe(
        catchError((error: unknown) => {
            if (!(error instanceof HttpErrorResponse) || !(error.status === 0 || (error.status === 404 && error.error === null)))
                return throwError(() => error);

            const newError = new HttpErrorResponse({
                url: error.url ?? undefined,
                headers: error.headers,
                status: 503,
                error: {
                    succeeded: false,
                    message: error.status === 0 ? 'SERVER_UNREACHABLE' : 'ENDPOINT_NOT_FOUND',
                    value: null
                },
            });

            console.error(newError.error.message === 'ENDPOINT_NOT_FOUND'
                ? `\nThe endpoint (${newError.url}) has not been found, please contact us about this issue!`
                : "\nThe server is unreachable at this moment, please try again at a later time!");

            return throwError(() => newError);
        })
    );
};
