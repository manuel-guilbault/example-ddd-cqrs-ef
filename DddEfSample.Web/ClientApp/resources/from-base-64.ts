export class FromBase64ValueConverter {
    toView(raw: string) {
        return atob(raw);
    }
}