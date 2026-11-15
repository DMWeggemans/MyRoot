import { Injectable } from "@angular/core";
import { BaseClient } from "../base.client";

@Injectable({ providedIn: 'root' })
export class PlayStationClient extends BaseClient {
    public async getUserByOnlineId(request: { onlineId: string }) {
        return this.get<any>(`/playstation/user/onlineid?onlineId=${request.onlineId}`);
    }

    public async getUserByNpsso(request: { npsso: string }) {
        return this.get<any>(`/playstation/user/npsso?npsso=${request.npsso}`);
    }
}