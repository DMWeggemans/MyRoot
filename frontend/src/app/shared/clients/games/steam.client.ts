import { LinkSteamRequest, LinkSteamResponse } from "../../../features/games/steam.types";
import { BaseClient } from "../base.client";
import { Injectable } from "@angular/core";

@Injectable({ providedIn: 'root' })
export class SteamClient extends BaseClient {
    public async getLinkPreview(request: LinkSteamRequest) {
        return this.query<LinkSteamResponse>("/steam/auth/preview", request);
    }

    public async link(request: LinkSteamRequest) {
        return this.post("/steam/auth/link", request);
    }
}