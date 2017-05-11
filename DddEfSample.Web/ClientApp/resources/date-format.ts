import * as moment from 'moment';

export class DateFormatValueConverter {
    toView(value: any, format: string): string {
        const m = moment(value);
        if (m.isValid()) {
            return m.format(format);
        }
        return value;
    }
}