import { FilterResponse } from "../base.types";

export interface AbstractGame {
    id: string;
    title: string;
    imageUrl: string;
    statistics: GameStatistics;
}

export interface GameStatistics {
    uploadedOn: Date;
    totalOwned: number;
    totalLiked: number;
    totalViews: number;
    rating: number;
}

export interface GameFilters {
    sortBy: string;
    sortDirection: 'ascending' | 'descending';
    searchBy: string;
    includeTags: string[];
    includeTagsProcess: 'and' | 'or'
    excludeTags: string[];
    pageSize: number;
    page: number;
}

// Responses
export interface GetGamesResponse {
    games: AbstractGame[];
    filters: FilterResponse;
}