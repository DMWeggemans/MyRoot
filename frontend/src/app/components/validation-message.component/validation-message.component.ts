import { Component, input } from '@angular/core';
import { FieldState } from '@angular/forms/signals';

@Component({
  selector: 'validation-message',
  standalone: true,
  imports: [],
  templateUrl: './validation-message.component.html',
  styleUrl: './validation-message.component.css',
})
export class ValidationMessageComponent {
  public readonly field = input.required<FieldState<any, any>>();
}