import { FieldTree, FieldValidator, PathKind, SchemaPath, validate } from "@angular/forms/signals";

export function createError<TModel, TKey extends string | number = string | number, TMode extends "writable" | "readonly" = "writable">(field: FieldTree<TModel, TKey, TMode>, message?: string | null) {
    return {
        kind: 'error',
        message: message ?? 'An unexpected error has occurred',
        fieldTree: field
    };
}

export const NPSSO_REGEX = /^[a-zA-Z0-9]{64}$/;
export function npsso(path: SchemaPath<string, 1, PathKind.Child>) {
    validate(path, ({ value }) => {
        const json = value().trim();
        if (NPSSO_REGEX.test(json))
            return null;

        if (!json.startsWith('{'))
            return {
                kind: 'invalid_npsso',
                message: 'NPSSO must be 64 alphanumeric characters'
            };

        try {
            const object = JSON.parse(json);
            if (typeof object?.npsso !== 'string')
                return {
                    kind: 'missing_npsso',
                    message: "The value does not contain a field named 'npsso'"
                };

            if (!NPSSO_REGEX.test(object.npsso))
                return {
                    kind: 'invalid_npsso_format',
                    message: "The 'npsso' field must be 64 alphanumeric characters"
                };

            return null;
        } catch {
            return {
                kind: 'invalid_json',
                message: 'The pasted JSON is invalid'
            };
        }
    });
}