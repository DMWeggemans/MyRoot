import { AbstractGame, GameFilters, GetGamesResponse } from "./games.types";
import { BaseClient } from "../base.client";
import { Injectable } from "@angular/core";
import { Result } from "../base.types";

@Injectable({ providedIn: 'root' })
export class GamesClient extends BaseClient {
    private readonly setOfGames = [{
        id: "1",
        title: "This is a game title",
        imageUrl: "https://image.api.playstation.com/vulcan/ap/rnd/202407/1020/91fe046f742042e3b31e57f7731dbe2226e1fd1e02a36223.jpg"
    }, {
        id: "2",
        title: "This is a game title",
        imageUrl: "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRkLIX6ZMa2mHSRdgWxOwOHJjAQhI_GffsjqXKoyuZb6KbbMvlhlv5usE5E&s=10"
    }, {
        id: "3",
        title: "This is a game title",
        imageUrl: "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQ-fWlmX2KRTZfBUnbkbhm98gKLdLtPxmdAaxI3DupU55XHUyPtSx2Kyj4&s=10",
    }];

    public async getAllGames(filters: GameFilters) {
        return new Promise(resolve => {
            setTimeout(() => {
                const games: AbstractGame[] = Array.from({ length: filters.pageSize }, (_, index) => {
                    const randomGame = this.setOfGames[index % this.setOfGames.length];

                    const uploadedOn = new Date();
                    uploadedOn.setUTCDate(uploadedOn.getUTCDate() - this.randomBetween(2, 695));
                    const totalOwned = this.randomBetween(10000, 9999999);
                    const totalViews = totalOwned + this.randomBetween(10000, 999999);
                    const totalLiked = totalOwned - this.randomBetween(10000, totalOwned);

                    const game: AbstractGame = {
                        ...randomGame,
                        statistics: {
                            uploadedOn: uploadedOn,
                            totalOwned: totalOwned,
                            totalViews: totalViews,
                            totalLiked: totalLiked,
                            rating: Math.max(totalLiked / totalOwned * 5, 1)
                        }
                    }

                    return game;
                });

                const totalItems = filters.pageSize + 80;
                const response: Result<GetGamesResponse> = {
                    succeeded: true,
                    message: null,
                    value: {
                        games: games,
                        filters: {
                            totalItems: totalItems,
                            totalPages: Math.ceil(totalItems / filters.pageSize)
                        }
                    }
                };

                resolve(response);
            }, this.randomBetween(100, 1000));
        }) as Promise<Result<GetGamesResponse>>;
    }

    private randomBetween(min: number, max: number) {
        min = Math.ceil(min);
        max = Math.floor(max);

        return Math.floor(Math.random() * (max - min + 1)) + min;
    }
}