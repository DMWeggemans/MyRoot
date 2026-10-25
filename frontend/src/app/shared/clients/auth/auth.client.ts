import { AuthorizedValues, LoginUserRequest, RegisterUserRequest } from "./auth.types";
import { BaseClient } from "../base.client";
import { Injectable } from "@angular/core";

@Injectable({ providedIn: 'root' })
export class AuthClient extends BaseClient {
    public async login(request: LoginUserRequest) {
        return await this.post('/auth/login', request);
    }

    public async register(request: RegisterUserRequest) {
        return await this.post('/auth/register', request);
    }

    public async refresh() {
        return await this.post('/auth/refresh', null);
    }

    public async logout() {
        return await this.post('/auth/logout', null);
    }

    public async authorize() {
        return await this.get<AuthorizedValues>('/auth/authorize');
    }
}