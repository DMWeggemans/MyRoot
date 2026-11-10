export interface LinkSteamResponse {
    steamId: string;
    userName: string;
    avatar: string;
    isPublic: boolean;
    profileUrl: string;
    totalGames: number;
    lastLogOff: Date;
    timeCreated: Date;
}