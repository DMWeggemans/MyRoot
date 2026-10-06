import { Component, input, OnInit } from '@angular/core';

@Component({
    selector: 'app-game.page',
    standalone: true,
    imports: [],
    templateUrl: './game.page.html',
    styleUrl: './game.page.css',
})
export class GamePage implements OnInit {
    public id = input.required<string>();

    ngOnInit() {
        console.log(this.id());
    }
}
