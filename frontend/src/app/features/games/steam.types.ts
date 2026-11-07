export interface LinkSteamRequest {
    steamId: string;
    userName: string;
    avatar: string;
    isPublic: boolean;
    profileUrl: string;
    lastLogOff: Date;
    timeCreated: Date;
}