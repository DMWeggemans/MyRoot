import { FieldTree, form, FormOptions, TreeValidationResult } from "@angular/forms/signals";
import { signal, WritableSignal } from "@angular/core";

export class SignalForm<TValue> {
    public value: WritableSignal<TValue>;
    public field: FieldTree<TValue, string | number, "writable">;
    public errors = signal<TreeValidationResult[]>([]);

    public constructor(initialValue: TValue, options?: FormOptions<TValue>) {
        this.value = signal<TValue>(initialValue);

        if (options?.submission?.action) {
            const action = options.submission.action;
            options.submission.action = this.getActionWithErrors(action);
        }

        this.field = options ? form(this.value, options) : form(this.value);
    }

    public createError<TModel, TKey extends string | number = string | number, TMode extends "writable" | "readonly" = "writable">(field: FieldTree<TModel, TKey, TMode>, message?: string | null) {
        this.errors.update(array => [
            ...array,
            {
                kind: 'error',
                message: message ?? 'An unexpected error has occurred',
                fieldTree: field
            }
        ]);
    }

    private getActionWithErrors(action: (field: FieldTree<TValue, string | number, "writable">, detail: {
        root: FieldTree<TValue, string | number, "writable">;
        submitted: FieldTree<unknown, string | number, "writable">;
    }) => Promise<TreeValidationResult>) {
        return async (field: FieldTree<TValue, string | number, "writable">, detail: {
            root: FieldTree<TValue, string | number, "writable">;
            submitted: FieldTree<unknown, string | number, "writable">;
        }) => {
            this.errors.set([]);
            await action(field, detail);
            if (this.errors().length)
                return [...this.errors()] as TreeValidationResult;

            return undefined;
        }
    }
}

export function signalForm<TValue>(initialValue: TValue, options?: FormOptions<TValue>) {
    return new SignalForm<TValue>(initialValue, options);
}