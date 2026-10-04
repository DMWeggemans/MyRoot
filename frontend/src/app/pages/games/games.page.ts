import { GameFiltersComponent } from '../../components/game-filters/game-filters.component';
import { GameCardComponent } from '../../components/game-card/game-card.component';
import { AbstractGame, GameFilters } from '../../shared/clients/games/games.types';
import { GamesClient } from '../../shared/clients/games/games.client';
import { Component, inject, OnInit, signal } from '@angular/core';

@Component({
    selector: 'app-games.page',
    standalone: true,
    imports: [GameCardComponent, GameFiltersComponent],
    templateUrl: './games.page.html',
    styleUrl: './games.page.css',
})
export class GamesPage implements OnInit {
    private readonly gamesClient = inject(GamesClient);

    public filters = signal<GameFilters>({ sortBy: "created-on", sortDirection: "ascending", searchBy: "", includeTags: [], includeTagsProcess: 'and', excludeTags: [] });
    public games = signal<AbstractGame[]>([]);
    public isLoading = signal<boolean>(true);

    ngOnInit() {
        this.getGames();
    }

    public async getGames() {
        this.isLoading.set(true);
        const filters = this.filters();
        console.log(filters);
        const response = await this.gamesClient.getAllGames(filters);

        if (!response.succeeded || response.value === null) {
            this.games.set([]);
            this.isLoading.set(false);
            return;
        }

        this.games.set(response.value);
        this.isLoading.set(false);
    }
}
