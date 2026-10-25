import { ValidationMessageComponent } from '../../components/validation-message/validation-message.component';
import { LoginUserRequest } from '../../shared/clients/auth/auth.types';
import { FieldTree, FormField, FormRoot } from '@angular/forms/signals';
import { createError } from '../../shared/helpers/validation.helpers';
import { AuthService } from '../../features/auth/auth.service';
import { signalForm } from '../../shared/helpers/form.helpers';
import { Router, RouterLink } from '@angular/router';
import { Component, inject } from '@angular/core';

@Component({
    selector: 'login-page',
    standalone: true,
    imports: [FormRoot, FormField, ValidationMessageComponent, RouterLink],
    templateUrl: './login.page.html',
    styleUrl: './login.page.css',
})
export class LoginPage {
    private readonly authService = inject(AuthService);
    private readonly router = inject(Router);

    public readonly request = signalForm<LoginUserRequest>({
        user: "",
        password: ""
    }, { submission: { action: async (fieldtree) => this.login(fieldtree) } });

    async login(fieldtree: FieldTree<LoginUserRequest>) {
        const result = await this.authService.login(this.request.value());

        if (!result.succeeded) {
            switch (result.message) {
                case 'INCORRECT_CREDENTIALS':
                    return createError(fieldtree.password, 'The e-mail or password is incorrect');
                default:
                    return createError(fieldtree.password);
            }
        }

        this.router.navigate(['/dashboard']);
        return [];
    }
}
