import { FieldTree, form, FormOptions, PathKind, SchemaOrSchemaFn } from "@angular/forms/signals";
import { signal, WritableSignal } from "@angular/core";

export class SignalForm<TValue> {
    public value: WritableSignal<TValue>;
    public field: FieldTree<TValue, string | number, "writable">;

    public constructor(initialValue: TValue, schemaOrOptions?: SchemaOrSchemaFn<TValue, PathKind.Root> | FormOptions<TValue>, options?: FormOptions<TValue>) {
        this.value = signal<TValue>(initialValue);
        if (!schemaOrOptions && !options) {
            this.field = form(this.value);
            return;
        }

        if (schemaOrOptions && !options) {
            this.field = form(this.value, schemaOrOptions);
            return;
        }

        if (!schemaOrOptions && options) {
            this.field = form(this.value, options);
            return;
        }

        this.field = form(this.value, schemaOrOptions as SchemaOrSchemaFn<TValue, PathKind.Root>, options!);
    }
}

export function signalForm<TValue>(initialValue: TValue): SignalForm<TValue>;
export function signalForm<TValue>(initialValue: TValue, schemaOrOptions: SchemaOrSchemaFn<TValue, PathKind.Root> | FormOptions<TValue>): SignalForm<TValue>;
export function signalForm<TValue>(initialValue: TValue, schema: SchemaOrSchemaFn<TValue, PathKind.Root>, options: FormOptions<TValue>): SignalForm<TValue>;
export function signalForm<TValue>(initialValue: TValue, schemaOrOptions?: SchemaOrSchemaFn<TValue, PathKind.Root> | FormOptions<TValue>, options?: FormOptions<TValue>): SignalForm<TValue> {
  return new SignalForm(initialValue, schemaOrOptions as any, options as any);
}