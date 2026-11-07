import { Component, computed, inject, OnDestroy, OnInit, signal } from '@angular/core';
import { LinkSteamComponent } from '../../components/link-steam/link-steam.component';
import { LinkSteamRequest } from '../../features/games/steam.types';
import { SteamService } from '../../features/games/steam.service';
import { AuthService } from '../../features/auth/auth.service';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';

@Component({
    selector: 'dashboard-page',
    standalone: true,
    imports: [LinkSteamComponent],
    templateUrl: './dashboard.page.html',
    styleUrl: './dashboard.page.css',
})
export class DashboardPage implements OnInit, OnDestroy {
    public readonly steamService = inject(SteamService);
    private readonly authService = inject(AuthService);
    private readonly router = inject(Router);

    public user = computed(() => this.authService.values());
    public linkSteam = signal<LinkSteamRequest | null>(null);

    private subscription!: Subscription;

    ngOnInit() {
        this.subscription = this.steamService.popup$.subscribe({
            next: async (result) => {
                console.log(result);
                this.linkSteam.set(result.value);
            }
        });
    }

    ngOnDestroy() {
        this.subscription.unsubscribe();
    }

    async logout() {
        await this.authService.logout();
        this.router.navigate(['/login']);
    }
}
