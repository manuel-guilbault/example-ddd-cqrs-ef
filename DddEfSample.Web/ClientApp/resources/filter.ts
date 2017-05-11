export class FilterValueConverter {
    toView(array: any[], predicate: (o: any) => boolean): any[] {
        return array.filter(o => predicate(o));
    }
}
