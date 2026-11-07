import { SteamClient } from "../../shared/clients/games/steam.client";
import { environment } from "../../../environments/environment";
import { Result } from "../../shared/clients/base.types";
import { inject, Injectable } from "@angular/core";
import { LinkSteamRequest } from "./steam.types";
import { Subject } from "rxjs";

@Injectable({ providedIn: 'root' })
export class SteamService {
    private readonly steamClient = inject(SteamClient);
    public popup$ = new Subject<Result<LinkSteamRequest>>();

    public async popup() {
        const width = 600;
        const height = 650;
        const left = (window.screen.width / 2) - (width / 2);
        const top = (window.screen.height / 2) - (height / 2);

        const trustedUrl = `${environment.api.url}/steam/steamid`
        const parameters = [
            "openid.ns=http://specs.openid.net/auth/2.0",
            "openid.mode=checkid_setup",
            `openid.return_to=${encodeURIComponent(trustedUrl)}`,
            `openid.realm=${encodeURIComponent(trustedUrl)}`,
            "openid.identity=http://specs.openid.net/auth/2.0/identifier_select",
            "openid.claimed_id=http://specs.openid.net/auth/2.0/identifier_select"
        ]

        const url = `https://steamcommunity.com/openid/login?${parameters.join('&')}`;
        const popup = window.open(
            url, "Steam Login Popup",
            `width=${width},
             height=${height},
             top=${top},
             left=${left},
             status=no,
             resizable=yes`
        );

        if (!popup || !popup.focus)
            return;

        popup.focus();
        window.addEventListener('message', this.onSteamMessage);
    }

    private onSteamMessage = (event: MessageEvent) => {
        window.removeEventListener('message', this.onSteamMessage);
        if (event.origin != environment.api.url || !event.data)
            return;

        const result = event.data as Result<LinkSteamRequest>;
        if (result.value === null)
            return;

        result.value.lastLogOff = new Date(result.value.lastLogOff);
        result.value.timeCreated = new Date(result.value.timeCreated);
        this.popup$.next(result);
    }
}