import { HttpEvent, HttpInterceptorFn, HttpResponse, HttpErrorResponse } from '@angular/common/http';
import { map, catchError, throwError } from 'rxjs';
import { Result } from '../clients/base.types';

const ISO_DATE_REGEX = /^\d{4}-\d{2}-\d{2}T\d{2}:\d{2}:\d{2}(?:\.\d+)?(?:Z|[+-]\d{2}:\d{2})?$/;

function convertDates(body: any): any {
    if (body === null || body === undefined)
        return body;

    if (typeof body === 'string' && ISO_DATE_REGEX.test(body))
        return new Date(body);

    if (Array.isArray(body))
        return body.map(item => convertDates(item));

    if (typeof body !== 'object')
        return body;

    const cloned = { ...body };
    for (const key of Object.keys(cloned))
        cloned[key] = convertDates(cloned[key]);

    return cloned;
}

export const dateInterceptor: HttpInterceptorFn = (request, next) => {
    return next(request).pipe(
        map((event: HttpEvent<any>) => {
            if (!(event instanceof HttpResponse) || !event.body)
                return event;

            const body = event.body as Result;
            if (body.value === undefined || body.value === null)
                return event;

            return event.clone({
                body: {
                    ...body,
                    value: convertDates(body.value)
                }
            });
        }),
        catchError((error: unknown) => {
            if (!(error instanceof HttpErrorResponse) || !error.error)
                return throwError(() => error);

            let newBody = error.error;
            if (typeof newBody === 'object' && 'value' in newBody) {
                newBody = {
                    ...newBody,
                    value: convertDates(newBody.value)
                };
            }

            const newError = new HttpErrorResponse({
                url: error.url ?? undefined,
                headers: error.headers,
                status: error.status,
                error: newBody,
            });

            return throwError(() => newError);
        })
    );
};
