export class DateHelper {
    public static getRelativeTimeString(date: Date, locale: string | undefined = undefined) {
        const cutoffs = [60, 3600, 86400, 86400 * 7, 86400 * 30, 86400 * 365, Infinity];
        const units: Intl.RelativeTimeFormatUnit[] = ['second', 'minute', 'hour', 'day', 'week', 'month', 'year'];

        const deltaSeconds = Math.round((date.getTime() - Date.now()) / 1000);
        const unitIndex = cutoffs.findIndex(cutoff => Math.abs(deltaSeconds) < cutoff);

        const divisor = unitIndex ? cutoffs[unitIndex - 1] : 1;

        const formatter = new Intl.RelativeTimeFormat(locale, { numeric: 'auto' });
        return formatter.format(Math.round(deltaSeconds / divisor), units[unitIndex]);
    }

    public static getDateString(date: Date, locale: string | undefined = undefined) {
        return date.toLocaleDateString(locale, {
            dateStyle: 'long'
        });
    }
}