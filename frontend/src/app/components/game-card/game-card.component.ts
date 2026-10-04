import { CommunityFillIcon } from '../../icons/community-fill/community-fill.icon';
import { ThumbsUpFillIcon } from '../../icons/thumbs-up-fill/thumbs-up-fill.icon';
import { getRelativeTimeString } from '../../shared/helpers/date.helpers';
import { AbstractGame } from '../../shared/clients/games/games.types';
import { StarFillIcon } from '../../icons/star-fill/star-fill.icon';
import { ClockIcon } from '../../icons/clock/clock';
import { EyeIcon } from '../../icons/eye/eye.icon';
import { Component, input } from '@angular/core';
import { RouterLink } from '@angular/router';

@Component({
    selector: 'game-card',
    standalone: true,
    imports: [CommunityFillIcon, StarFillIcon, ThumbsUpFillIcon, ClockIcon, EyeIcon, RouterLink],
    templateUrl: './game-card.component.html',
    styleUrl: './game-card.component.css',
})
export class GameCardComponent {
    public game = input.required<AbstractGame>();

    public toReadableDate = (date: Date) => getRelativeTimeString(date);
    public toReadableDigit = (number: number) => new Intl.NumberFormat().format(number);
    public toReadableNumber = (number: number) => new Intl.NumberFormat(undefined, {
        notation: 'compact',
        maximumFractionDigits: 0
    }).format(number);
    public toDecimal = (number: number) => number.toFixed(1);
}
