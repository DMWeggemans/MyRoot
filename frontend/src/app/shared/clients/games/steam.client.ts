import { LinkSteamResponse } from "../../../features/games/steam.types";
import { BaseClient } from "../base.client";
import { Injectable } from "@angular/core";

@Injectable({ providedIn: 'root' })
export class SteamClient extends BaseClient {
    public async link(request: LinkSteamResponse) {
        return this.post("/steam/link", request);
    }

    public async getLinkSteamResponse(request: { steamId: string }) {
        return this.query<LinkSteamResponse>("/steam/link/response", request);
    }
}