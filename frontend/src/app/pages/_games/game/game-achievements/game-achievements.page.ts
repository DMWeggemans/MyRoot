import { GameStateService } from '../../../../shared/services/game-state.service';
import { Component, computed, inject } from '@angular/core';

@Component({
    selector: 'game-achievements-page',
    standalone: true,
    imports: [],
    templateUrl: './game-achievements.page.html',
    styleUrl: './game-achievements.page.css',
})
export class GameAchievementsPage {
    public readonly gameState = inject(GameStateService);

    public gameObject = computed(() => {
        const game = this.gameState.game();
        if (!game || typeof game !== 'object')
            return "?";

        return JSON.stringify(game);
    });
}
