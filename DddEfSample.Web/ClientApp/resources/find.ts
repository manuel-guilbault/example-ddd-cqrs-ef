export class FindValueConverter {
    toView(items: any[], property: string, value: any) {
        return items.find(x => x[property] === value);
    }
}