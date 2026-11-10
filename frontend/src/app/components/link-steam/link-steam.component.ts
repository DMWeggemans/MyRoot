import { SteamClient } from '../../shared/clients/games/steam.client';
import { LinkSteamResponse } from '../../features/games/steam.types';
import { DateHelper } from '../../shared/helpers/date.helpers';
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
    public readonly DateHelper = DateHelper;

    public response = model.required<LinkSteamResponse | null>();

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
}
