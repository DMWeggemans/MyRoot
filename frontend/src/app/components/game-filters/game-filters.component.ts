import { ThumbsUpFillIcon } from '../../icons/thumbs-up-fill/thumbs-up-fill.icon';
import { searchableSignal } from '../../shared/helpers/signal.helpers';
import { GameFilters } from '../../shared/clients/games/games.types';
import { StarFillIcon } from '../../icons/star-fill/star-fill.icon';
import { AlphabetIcon } from '../../icons/alphabet/alphabet.icon';
import { Component, model, signal } from '@angular/core';
import { ClockIcon } from '../../icons/clock/clock';
import { EyeIcon } from '../../icons/eye/eye.icon';

@Component({
  selector: 'game-filters',
  standalone: true,
  imports: [ClockIcon, ThumbsUpFillIcon, EyeIcon, AlphabetIcon, StarFillIcon],
  templateUrl: './game-filters.component.html',
  styleUrl: './game-filters.component.css',
})
export class GameFiltersComponent {
    public filters = model.required<GameFilters>();
    
    public tags = signal<string[]>(["aa", "ab", "ac", "ad", "ba", "bb", "bc", "bd"]);

    public includeTags = searchableSignal<string>(this.tags.asReadonly(), (tag, searchBy) => tag.includes(searchBy) && ![...this.filters().includeTags, ...this.filters().excludeTags].includes(tag));
    public excludeTags = searchableSignal<string>(this.tags.asReadonly(), (tag, searchBy) => tag.includes(searchBy) && ![...this.filters().includeTags, ...this.filters().excludeTags].includes(tag));

    public setValue(key: keyof GameFilters, value: string) {
        this.filters.update(x => ({
            ...x,
            [key]: value
        }));
    }

    public setTag(type: 'include' | 'exclude', tag: string, event?: Event) {
        const key: keyof GameFilters = `${type}Tags`;

        this.filters.update(x => {
            const index = x[key].findIndex(y => y === tag);
            if (index === undefined || index < 0) {
                return {
                    ...x,
                    [key]: [...x[key], tag]
                };
            }

            x[key].splice(index, 1);
            return { ...x };
        });

        if (event === undefined)
            return;

        const target = event.target as HTMLElement;
        const parent = target.parentElement;
        if (parent === null)
            return;

        parent.classList.add("closing");
        setTimeout(() => {
            parent.classList.remove("closing");
        }, 1);
    }
}
