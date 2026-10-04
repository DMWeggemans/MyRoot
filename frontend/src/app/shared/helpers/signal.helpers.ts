import { signal, computed, Signal, WritableSignal } from '@angular/core';

export class SearchableSignal<T> {
    public query: WritableSignal<string> = signal('');
    public filtered: Signal<T[]>;

    constructor(private source: Signal<T[]>, private filter: (item: T, query: string) => boolean) {
        this.filtered = computed(() => {
            const searchBy = this.query().toLowerCase().trim();
            if (!searchBy)
                return this.source().filter(item => this.filter(item, ""));

            return this.source().filter(item => this.filter(item, searchBy));
        });
    }
}

export function searchableSignal<T>(source: Signal<T[]>, filter: (item: T, query: string) => boolean) {
    return new SearchableSignal<T>(source, filter);
}
