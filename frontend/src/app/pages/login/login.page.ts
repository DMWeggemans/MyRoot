import { ValidationMessageComponent } from '../../components/validation-message.component/validation-message.component';
import { LoginUserRequest } from '../../shared/clients/auth/auth.types';
import { AuthService } from '../../features/auth/auth.service';
import { signalForm } from '../../shared/helpers/form.helpers';
import { FormField, FormRoot } from '@angular/forms/signals';
import { Component, inject } from '@angular/core';
import { Router } from '@angular/router';

@Component({
    selector: 'login-page',
    standalone: true,
    imports: [FormRoot, FormField, ValidationMessageComponent],
    templateUrl: './login.page.html',
    styleUrl: './login.page.css',
})
export class LoginPage {
    private readonly authService = inject(AuthService);
    private readonly router = inject(Router);

    public readonly request = signalForm<LoginUserRequest>({
        user: "",
        password: ""
    }, { submission: { action: async () => this.login() } });

    async login() {
        const result = await this.authService.login(this.request.value());

        if (!result.succeeded) {
            switch (result.message) {
                case 'INCORRECT_CREDENTIALS':
                    this.request.createError(this.request.field.password, 'The e-mail or password is incorrect');
                    break;
                default:
                    this.request.createError(this.request.field.password);
                    break;
            }
            
            return;
        }

        this.router.navigate(['/dashboard']);
    }
}
