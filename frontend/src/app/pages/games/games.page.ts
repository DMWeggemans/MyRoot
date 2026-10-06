import { GamePaginationComponent } from '../../components/game-pagination/game-pagination.component';
import { GameFiltersComponent } from '../../components/game-filters/game-filters.component';
import { GameCardComponent } from '../../components/game-card/game-card.component';
import { AbstractGame, GameFilters } from '../../shared/clients/games/games.types';
import { GamesClient } from '../../shared/clients/games/games.client';
import { Component, inject, OnInit, signal } from '@angular/core';
import { FilterResponse } from '../../shared/clients/base.types';

@Component({
    selector: 'app-games.page',
    standalone: true,
    imports: [GameCardComponent, GameFiltersComponent, GamePaginationComponent],
    templateUrl: './games.page.html',
    styleUrl: './games.page.css',
})
export class GamesPage implements OnInit {
    private readonly gamesClient = inject(GamesClient);

    public filters = signal<GameFilters>({ sortBy: "updated-on", sortDirection: "ascending", searchBy: "", includeTags: [], includeTagsProcess: 'and', excludeTags: [], pageSize: 12, page: 1 });
    public filterResponse = signal<FilterResponse | null>(null);
    public games = signal<AbstractGame[]>([]);
    public isLoading = signal<boolean>(true);

    private timeoutId: number | null = null;

    ngOnInit() {
        this.getGames();
    }

    public async waitAndGetGames() {
        if (this.timeoutId)
            clearTimeout(this.timeoutId);

        this.timeoutId = setTimeout(async () => await this.getGames(), 250);
    }

    public async getGames() {
        this.isLoading.set(true);
        const filters = this.filters();

        const response = await this.gamesClient.getAllGames(filters);
        if (!response.succeeded || response.value === null) {
            this.games.set([]);
            this.isLoading.set(false);
            return;
        }

        this.games.set(response.value.games);
        this.filterResponse.set(response.value.filters);
        this.isLoading.set(false);
    }
}
