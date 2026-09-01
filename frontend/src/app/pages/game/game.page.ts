import { HeaderComponent } from '../../components/header/header.component';
import { Component, input } from '@angular/core';

@Component({
  selector: 'app-game.page',
  standalone: true,
  imports: [HeaderComponent],
  templateUrl: './game.page.html',
  styleUrl: './game.page.css',
})
export class GamePage {
  public id = input.required<string>();

  ngOnInit() {
    console.log(this.id());
  }
}
