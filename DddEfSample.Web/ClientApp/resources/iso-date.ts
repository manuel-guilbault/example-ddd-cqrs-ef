function pad(value: number, char = '0', size = 2) {
    let result = '' + value;
    while (result.length < size) {
        result = char + result;
    }
    return result;
}

export class IsoDateValueConverter {
    toView(value: Date) {
        if (!value) {
            return '';
        }
        const year = value.getFullYear();
        const month = pad(value.getUTCMonth());
        const day = pad(value.getUTCDay());
        const hours = pad(value.getUTCHours());
        const minutes = pad(value.getUTCMinutes());
        return `${year}-${month}-${day}T${hours}:${minutes}`;
    }

    fromView(value: string) {
        return value ? new Date(value) : null;
    }
}