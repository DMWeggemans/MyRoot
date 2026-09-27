import { GameCardComponent } from '../../components/game-card/game-card.component';
import { Component } from '@angular/core';

@Component({
    selector: 'app-games.page',
    standalone: true,
    imports: [GameCardComponent],
    templateUrl: './games.page.html',
    styleUrl: './games.page.css',
})
export class GamesPage {
    public games: any = [{
        id: "1",
        image: "https://image.api.playstation.com/vulcan/ap/rnd/202407/1020/91fe046f742042e3b31e57f7731dbe2226e1fd1e02a36223.jpg",
        title: "This is a game title",
        statistics: {
            totalOwned: 508943,
            totalLiked: 508943 - 19552,
            totalViews: 508943 + 129523,
            totalRating: 4.9
        }
    }, {
        id: "2",
        image: "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRkLIX6ZMa2mHSRdgWxOwOHJjAQhI_GffsjqXKoyuZb6KbbMvlhlv5usE5E&s=10",
        title: "This is a game title",
        statistics: {
            totalOwned: 958934,
            totalLiked: 958934 - 4385,
            totalViews: 958934 + 324895,
            totalRating: 5
        }
    }, {
        id: "3",
        image: "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQ-fWlmX2KRTZfBUnbkbhm98gKLdLtPxmdAaxI3DupU55XHUyPtSx2Kyj4&s=10",
        title: "This is a game title",
        statistics: {
            totalOwned: 8949395,
            totalLiked: 8949395 - 584392,
            totalViews: 8949395 + 59472957,
            totalRating: 4.2
        }
    }];

    public array = (number: number) => Array.from({ length: number });

    ngOnInit() {
        for (const game of this.games) {
            const totalTime = new Date();
            totalTime.setUTCDate(totalTime.getUTCDate() - Math.round(Math.random() * 28));

            game.statistics.totalTime = totalTime;
            console.log(game);
        }
    }
}
