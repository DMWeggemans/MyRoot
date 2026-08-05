import { HttpInterceptorFn } from '@angular/common/http';

export const credentialsInterceptor: HttpInterceptorFn = (request, next) => {
    const newRequest = request.clone({
        withCredentials: true
    });

    return next(newRequest);
};
