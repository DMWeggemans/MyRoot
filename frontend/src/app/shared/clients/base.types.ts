export interface Result<TResponse = null> {
    value: TResponse | null;
    message: string | null;
    succeeded: boolean;
}