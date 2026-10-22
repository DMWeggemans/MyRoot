import { Component, inject, input, model, Renderer2 } from '@angular/core';
import { GameFilters } from '../../../shared/clients/games/games.types';
import { FilterResponse } from '../../../shared/clients/base.types';

@Component({
    selector: 'game-pagination',
    standalone: true,
    imports: [],
    templateUrl: './game-pagination.component.html',
    styleUrl: './game-pagination.component.css',
})
export class GamePaginationComponent {
    private readonly renderer = inject(Renderer2);

    public filterResponse = input.required<FilterResponse | null>();
    public filters = model.required<GameFilters>();

    public get pageSizes() {
        const totalPages = this.filterResponse()?.totalPages;
        const page = this.filters().page;

        const pageSizes = [1];
        if (totalPages !== undefined) {
            const temp = [1, totalPages];
            const previousPage = page - 1;
            if (previousPage > 0 && !temp.includes(previousPage))
                pageSizes.push(previousPage);

            if (!temp.includes(page))
                pageSizes.push(page);

            const nextPage = page + 1;
            if (nextPage <= totalPages && !temp.includes(nextPage))
                pageSizes.push(nextPage);

            pageSizes.push(totalPages);
            let skipNext = false;
            for (const [index, page] of pageSizes.entries()) {
                if (page + 1 === pageSizes[index + 1] || skipNext || index + 1 === pageSizes.length) {
                    skipNext = false;
                    continue;
                }

                pageSizes.splice(index + 1, 0, -1);
                skipNext = true;
            }
        }

        return pageSizes;
    }

    public toReadableNumber = (number: number) => new Intl.NumberFormat(undefined, {
        notation: 'compact',
        maximumFractionDigits: 0
    }).format(number);

    public setPageSize(pageSize: number) {
        if (pageSize === this.filters().pageSize)
            return;

        this.filters.update(x => ({
            ...x,
            pageSize: pageSize
        }));
    }

    public setPage(page: number) {
        if (page > (this.filterResponse()?.totalPages ?? 0) || page === this.filters().page)
            return;

        this.filters.update(x => ({
            ...x,
            page: page
        }));
    }

    public addPage(number: number) {
        const totalPages = this.filterResponse()?.totalPages;
        const page = this.filters().page;

        if (totalPages === undefined)
            return;

        const prediction = page + number;
        if (prediction <= 0 || prediction > totalPages)
            return;

        this.setPage(prediction);
    }

    public setCustomPage(event: Event) {
        const target = event.target as HTMLInputElement;
        const value = Number(target.value);
        if (!target || Number.isNaN(value))
            return;

        target.blur();
        this.setPage(value);
    }

    public enableInput(event: Event) {
        const target = event.target as HTMLButtonElement;
        if (!target)
            return;

        target.innerHTML = "";
        target.classList.add("contains-input");

        const input = this.renderer.createElement('input') as HTMLInputElement;
        input.type = 'text';
        input.classList.add("games-pagination-input");
        input.addEventListener('blur', this.disableInput);
        input.addEventListener('change', (event) => this.setCustomPage(event));

        target.append(input);
        input.focus();
    }

    public disableInput(event: FocusEvent) {
        const target = event.target as HTMLInputElement;
        const parent = target.parentElement as HTMLButtonElement;
        if (!target || !parent)
            return;

        target.classList.remove("contains-input");
        parent.innerHTML = "...";
    }
}
