import { GamepadIcon } from '../../icons/gamepad/gamepad.icon';
import { RouterLink } from "@angular/router";
import { Component } from '@angular/core';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [GamepadIcon, RouterLink],
  templateUrl: './header.component.html',
  styleUrl: './header.component.css',
})
export class HeaderComponent {

}
