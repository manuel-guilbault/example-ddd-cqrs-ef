export class NumberFormatValueConverter {
    toView(value: number): string {
        return value.toLocaleString();
    }
}