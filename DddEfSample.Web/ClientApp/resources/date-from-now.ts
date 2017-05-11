import * as moment from 'moment';

export class DateFromNowValueConverter {
    toView(value: any): string {
        const m = moment(value);
        if (m.isValid()) {
            return m.fromNow();
        }
        return value;
    }
}