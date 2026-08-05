import { catchError, firstValueFrom, of, switchMap } from "rxjs";
import { inject, InjectionToken } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Result } from "./base.types";

export const API_BASE_URL = new InjectionToken<string>('');

export class BaseClient {
    protected readonly baseUrl = inject(API_BASE_URL);
    protected readonly http = inject(HttpClient);

    protected async get<TResponse = null>(url: string) {
        const response$ = this.http.get(`${this.baseUrl}${url}`).pipe(
            switchMap((value) => of(value)),
            catchError((value) => of(value.error))
        );
        
        const response = await firstValueFrom(response$);
        return response as Result<TResponse>;
    }

    protected async query<TResponse = null>(url: string, request: any) {
        const response$ = this.http.request('QUERY', `${this.baseUrl}${url}`, { body: request }).pipe(
            switchMap((value) => of(value)),
            catchError((value) => of(value.error))
        );
        
        const response = await firstValueFrom(response$);
        return response as Result<TResponse>;
    }

    protected async post<TResponse = null>(url: string, request: any) {
        const response$ = this.http.post(`${this.baseUrl}${url}`, request).pipe(
            switchMap((value) => of(value)),
            catchError((value) => of(value.error))
        );
        
        const response = await firstValueFrom(response$);
        return response as Result<TResponse>;
    }

    protected async put<TResponse = null>(url: string, request: any) {
        const response$ = this.http.put(`${this.baseUrl}${url}`, request).pipe(
            switchMap((value) => of(value)),
            catchError((value) => of(value.error))
        );
        
        const response = await firstValueFrom(response$);
        return response as Result<TResponse>;
    }

    protected async delete<TResponse = null>(url: string) {
        const response$ = this.http.delete(`${this.baseUrl}${url}`).pipe(
            switchMap((value) => of(value)),
            catchError((value) => of(value.error))
        );
        
        const response = await firstValueFrom(response$);
        return response as Result<TResponse>;
    }
}