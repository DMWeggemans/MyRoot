import { GameStateService } from '../../../shared/services/game-state.service';
import { GamesClient } from '../../../shared/clients/games/games.client';
import { Router, RouterOutlet, RouterLink } from '@angular/router';
import { DateHelper } from '../../../shared/helpers/date.helpers';
import { Component, inject, input, OnInit } from '@angular/core';
import { ReturnIcon } from '../../../icons/return/return.icon';

@Component({
    selector: 'app-game.page',
    standalone: true,
    imports: [RouterOutlet, RouterLink, ReturnIcon],
    providers: [GameStateService],
    templateUrl: './game.page.html',
    styleUrl: './game.page.css',
})
export class GamePage implements OnInit {
    public readonly gameState = inject(GameStateService);
    public readonly DateHelper = DateHelper;

    private readonly gamesClient = inject(GamesClient);
    private readonly router = inject(Router);

    public id = input.required<string>();

    public ngOnInit() {
        this.getGame();
    }

    private async getGame() {
        const response = await this.gamesClient.getGame(this.id());
        this.gameState.game.set(response);
    }

    public get Route() {
        const router = this.router;
        const id = this.id;

        return class Route {
            public static go(event: Event, tab?: string) {
                event.preventDefault();
                if (tab === undefined) {
                    router.navigate(["/games", id()], { replaceUrl: true });
                    return;
                }

                router.navigate(["/games", id(), tab], { replaceUrl: true });
            }

            public static on(tab?: string) {
                const link = Route.link(tab);
                return router.url.endsWith(link);
            }

            public static link(tab?: string) {
                let link = `/games/${id()}`;
                if (tab !== undefined)
                    link += `/${tab}`;

                return link;
            }
        };
    }

    private random(from: number, to: number) {
        return Math.round(Math.random() * (to - from)) + from;
    }
}
