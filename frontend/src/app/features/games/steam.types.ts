export interface LinkSteamResponse {
    key: string;
    steamId: string;
    userName: string;
    avatar: string;
    isPublic: boolean;
    profileUrl: string;
    totalGames: number;
    lastLogOff: Date;
    timeCreated: Date;
}

export interface LinkSteamRequest {
    key: string;
    steamId: string;
}