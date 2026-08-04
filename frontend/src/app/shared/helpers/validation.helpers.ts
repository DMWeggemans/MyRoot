import { FieldTree } from "@angular/forms/signals";

export function createError<TModel, TKey extends string | number = string | number, TMode extends "writable" | "readonly" = "writable">(field: FieldTree<TModel, TKey, TMode>, message?: string | null) {
    return {
        kind: 'error',
        message: message ?? 'An unexpected error has occurred',
        fieldTree: field
    };
}