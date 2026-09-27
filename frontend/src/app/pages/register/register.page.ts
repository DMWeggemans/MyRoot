import { ValidationMessageComponent } from '../../components/validation-message/validation-message.component';
import { RegisterUserRequest } from '../../shared/clients/auth/auth.types';
import { FieldTree, FormField, FormRoot } from '@angular/forms/signals';
import { createError } from '../../shared/helpers/validation.helpers';
import { AuthService } from '../../shared/services/auth.service';
import { signalForm } from '../../shared/helpers/form.helpers';
import { Router, RouterLink } from '@angular/router';
import { Component, inject } from '@angular/core';

@Component({
    selector: 'register-page',
    standalone: true,
    imports: [FormRoot, FormField, ValidationMessageComponent, RouterLink],
    templateUrl: './register.page.html',
    styleUrl: './register.page.css',
})
export class RegisterPage {
    private readonly authService = inject(AuthService);
    private readonly router = inject(Router);

    public readonly request = signalForm<RegisterUserRequest>({
        userName: "",
        email: "",
        password: ""
    }, { submission: { action: async (fieldtree) => this.register(fieldtree) } });

    async register(fieldtree: FieldTree<RegisterUserRequest>) {
        const result = await this.authService.register(this.request.value());

        if (!result.succeeded) {
            let message = '';
            switch (result.message) {
                case 'INVALID_USERNAME':
                    message = this.request.value().password.length < 4
                        ? 'The username cannot be below 4 characters'
                        : 'The username cannot exceed 32 characters';
                    return [createError(fieldtree.userName, message)];
                case 'USERNAME_EXISTS':
                    return [createError(fieldtree.userName, 'The username is already in use')];
                case 'INVALID_EMAIL':
                    return [createError(fieldtree.email, 'The email must be valid')];
                case 'EMAIL_EXISTS':
                    return [createError(fieldtree.email, 'The e-mail is already in use')];
                case 'INVALID_PASSWORD':
                    message = this.request.value().password.length < 8
                        ? 'The password cannot be below 8 characters'
                        : 'The password cannot exceed 128 characters';
                    return [createError(fieldtree.password, message)];
                default:
                    return [createError(fieldtree.password)];
            }
        }

        this.router.navigate(['/dashboard']);
        return [];
    }
}
