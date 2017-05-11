export class PrettyJsonValueConverter {
    toView(json: string) {
        return JSON.stringify(JSON.parse(json), null, 2);
    }
}