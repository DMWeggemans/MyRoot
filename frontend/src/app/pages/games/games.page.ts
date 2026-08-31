import { HeaderComponent } from '../../components/header/header.component';
import { ArrowRightIcon } from '../../icons/arrow-right/arrow-right.icon';
import { ArrowLeftIcon } from '../../icons/arrow-left/arrow-left.icon';
import { OwnedIcon } from '../../icons/owned/owned.icon';
import { Component } from '@angular/core';

@Component({
  selector: 'app-games.page',
  imports: [ArrowLeftIcon, ArrowRightIcon, OwnedIcon, HeaderComponent],
  templateUrl: './games.page.html',
  styleUrl: './games.page.css',
})
export class GamesPage {

}
