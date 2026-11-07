import { LinkSteamRequest } from "../../../features/games/steam.types";
import { BaseClient } from "../base.client";
import { Injectable } from "@angular/core";

@Injectable({ providedIn: 'root' })
export class SteamClient extends BaseClient {
    public async link(request: LinkSteamRequest) {
        return this.post("/steam/link", request);
    }
}