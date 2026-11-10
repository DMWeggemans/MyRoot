import { SteamClient } from '../../shared/clients/games/steam.client';
import { LinkSteamResponse } from '../../features/games/steam.types';
import { DateHelper } from '../../shared/helpers/date.helpers';
import { Component, inject, model, signal } from '@angular/core';

@Component({
    selector: 'link-steam',
    standalone: true,
    imports: [],
    styleUrl: './link-steam.component.css',
    templateUrl: './link-steam.component.html',
})
export class LinkSteamComponent {
    private readonly steamClient = inject(SteamClient);
    public readonly DateHelper = DateHelper;

    public response = model.required<LinkSteamResponse | null>();
    public isRefreshing = signal<boolean>(false);

    public async link(answer: boolean) {
        const request = this.response();
        if (!answer || request === null) {
            this.response.set(null);
            return;
        }

        const result = await this.steamClient.link(request);
        if (!result.succeeded)
            return;

        this.response.set(null);
    }

    public async refresh() {
        this.isRefreshing.set(true);
        const response = this.response();
        if (response === null) {
            this.isRefreshing.set(false);
            return;
        }

        const result = await this.steamClient.getLinkSteamResponse({
            steamId: response.steamId
        });

        if (!result.succeeded && !["PRIVATE_PLAYER", "NO_GAMES_FOUND"].includes(result.message ?? "")){
            this.isRefreshing.set(false);
            return;
        }
        
        this.response.set(result.value);
        this.isRefreshing.set(false);
    }
}
