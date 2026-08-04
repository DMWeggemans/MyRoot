import { FieldTree, form, FormOptions, PathKind, SchemaOrSchemaFn } from "@angular/forms/signals";
import { signal, WritableSignal } from "@angular/core";

export class SignalForm<TValue> {
    public value: WritableSignal<TValue>;
    public field: FieldTree<TValue, string | number, "writable">;

    public constructor(initialValue: TValue, schemaOrOptions: FormOptions<TValue> | SchemaOrSchemaFn<TValue, PathKind.Root>) {
        this.value = signal<TValue>(initialValue);
        this.field = schemaOrOptions ? form(this.value, schemaOrOptions) : form(this.value);
    }
}

export function signalForm<TValue>(initialValue: TValue, schemaOrOptions: FormOptions<TValue> | SchemaOrSchemaFn<TValue, PathKind.Root>) {
    return new SignalForm<TValue>(initialValue, schemaOrOptions);
}