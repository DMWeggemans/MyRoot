import { unauthorizedInterceptor } from './shared/interceptors/unauthorized.interceptor';
import { credentialsInterceptor } from './shared/interceptors/credentials.interceptor';
import { ApplicationConfig, provideBrowserGlobalErrorListeners } from '@angular/core';
import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { API_BASE_URL } from './shared/clients/base.client';
import { environment } from '../environments/environment';
import { provideRouter } from '@angular/router';
import { routes } from './app.routes';

export const appConfig: ApplicationConfig = {
    providers: [
        provideBrowserGlobalErrorListeners(),
        provideRouter(routes),
        provideHttpClient(
            withInterceptors([
                credentialsInterceptor,
                unauthorizedInterceptor
            ])
        ),
        {
            provide: API_BASE_URL,
            useValue: environment.api.url
        }
    ]
};
