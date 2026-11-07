import { SteamClient } from '../../shared/clients/games/steam.client';
import { LinkSteamRequest } from '../../features/games/steam.types';
import { Component, inject, model } from '@angular/core';

@Component({
    selector: 'link-steam',
    standalone: true,
    imports: [],
    styleUrl: './link-steam.component.css',
    templateUrl: './link-steam.component.html',
})
export class LinkSteamComponent {
    private readonly steamClient = inject(SteamClient);

    public request = model.required<LinkSteamRequest | null>();

    public async link(answer: boolean) {
        const request = this.request();
        if (!answer || request === null) {
            this.request.set(null);
            return;
        }

        const result = await this.steamClient.link(request);
        if (!result.succeeded)
            return;

        this.request.set(null);
    }
}
