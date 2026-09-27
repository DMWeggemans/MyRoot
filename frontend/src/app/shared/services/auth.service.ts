import { AuthorizedValues, LoginUserRequest, RegisterUserRequest } from "../clients/auth/auth.types";
import { AuthClient } from "../clients/auth/auth.client";
import { inject, Injectable, signal } from "@angular/core";

@Injectable({ providedIn: 'root' })
export class AuthService {
    private readonly authClient = inject(AuthClient);
    public values = signal<AuthorizedValues | null>(null);
    public setAuthorized = signal<boolean | null>(null);

    public async login(request: LoginUserRequest) {
        const result = await this.authClient.login(request);
        if (!result.succeeded) {
            this.values.set(null);
            return result;
        }

        await this.authorize();
        return result;
    }

    public async register(request: RegisterUserRequest) {
        const result = await this.authClient.register(request);
        if (!result.succeeded) {
            this.values.set(null);
            return result;
        }

        await this.authorize();
        return result;
    }

    public async refresh() {
        const result = await this.authClient.refresh();
        if (!result.succeeded) {
            this.values.set(null);
            return result;
        }

        await this.authorize();
        return result;
    }

    public async logout() {
        await this.authClient.logout();
        this.values.set(null);
    }

    public async authorize() {
        const result = await this.authClient.authorize();
        switch (result.message) {
            case null:
                this.setAuthorized.set(true);
                break;
            case 'NOT_AUTHORIZED':
                this.setAuthorized.set(false);
                break;
            default:
                this.setAuthorized.set(null);
                break;
        }

        if (!result.succeeded || result.value === null) {
            this.values.set(null);
            return;
        }

        this.values.set(result.value);
    }

    public isAuthorized() {
        const isAuthorized = (this.values()?.expiresOn.getTime() ?? 0) > Date.now();
        if (!isAuthorized) this.values.set(null);

        return isAuthorized;
    }
}