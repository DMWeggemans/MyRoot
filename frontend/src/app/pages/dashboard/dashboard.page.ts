import { Router } from '@angular/router';
import { AuthService } from '../../features/auth/auth.service';
import { Component, computed, inject } from '@angular/core';

@Component({
    selector: 'dashboard-page',
    standalone: true,
    imports: [],
    templateUrl: './dashboard.page.html',
    styleUrl: './dashboard.page.css',
})
export class DashboardPage {
    private readonly authService = inject(AuthService);
    private readonly router = inject(Router);

    public user = computed(() => this.authService.values());

    async logout() {
        await this.authService.logout();
        this.router.navigate(['/login']);
    }
}
