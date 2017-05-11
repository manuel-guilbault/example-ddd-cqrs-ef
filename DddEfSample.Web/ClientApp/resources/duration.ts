import * as moment from 'moment';

export class DurationValueConverter {
    toView(value: any, format: string): string {
        const d = moment.duration(value);
        if (moment.isDuration(d)) {
            return d.humanize();
        }
        return value;
    }
}