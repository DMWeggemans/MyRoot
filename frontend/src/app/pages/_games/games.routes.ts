import { GameAchievementsPage } from "./game/game-achievements/game-achievements.page";
import { GameInfoPage } from "./game/game-info/game-info.page";
import { GamesPage } from "./games/games.page";
import { GamePage } from "./game/game.page";
import { Routes } from "@angular/router";

export const routes: Routes = [
    {
        path: "",
        component: GamesPage
    },
    {
        path: ":id",
        component: GamePage,
        children: [
            {
                path: '',
                component: GameInfoPage,
            },
            {
                path: 'achievements',
                component: GameAchievementsPage,
            }
        ]
    }
];