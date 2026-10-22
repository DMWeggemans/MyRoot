import { GameStateService } from '../../../../shared/services/game-state.service';
import { Component, computed, inject } from '@angular/core';

@Component({
    selector: 'app-game-info.page',
    standalone: true,
    imports: [],
    templateUrl: './game-info.page.html',
    styleUrl: './game-info.page.css',
})
export class GameInfoPage {
    public readonly gameState = inject(GameStateService);

    public gameObject = computed(() => {
        const game = this.gameState.game();
        if (!game || typeof game !== 'object')
            return "?";

        return JSON.stringify(game);
    });
}
