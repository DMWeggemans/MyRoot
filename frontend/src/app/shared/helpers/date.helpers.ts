export class DateHelper {
    public static formatDateAgo(date?: Date) {
        if (date === undefined)
            return "?";

        const now = new Date();

        const secondsAgo = Math.floor((now.getTime() - date.getTime()) / 1000);
        const daysAgo = Math.floor(secondsAgo / 86400);

        if (secondsAgo < 3600) {
            const minutesAgo = Math.floor(secondsAgo / 60);
            if (minutesAgo <= 0)
                return `now`;

            return `${minutesAgo === 1 ? "1 minute" : `${minutesAgo} minutes`} ago`;
        }

        if (secondsAgo < 86400) {
            const hoursAgo = Math.floor(secondsAgo / 3600);
            return `${hoursAgo === 1 ? "1 hour" : `${hoursAgo} hours`} ago`;
        }

        if (daysAgo < 30)
            return daysAgo === 1 ? "yesterday" : `${daysAgo} days ago`;

        return DateHelper.format(date, {
            dateStyle: 'medium',
        });
    }

    public static formatSimple(date?: Date) {
        if (date === undefined)
            return "?";

        return new Intl.DateTimeFormat(navigator.language, {
            dateStyle: 'medium',
            timeStyle: 'short'
        }).format(date);
    }

    public static format(date?: Date, options?: Intl.DateTimeFormatOptions) {
        if (date === undefined)
            return "?";

        return new Intl.DateTimeFormat(navigator.language, options).format(date);
    }
}