export class SortValueConverter {
    toView(array: any[], param: ISortConfiguration | ISortConfiguration[]): any[] {
        if (Array.isArray(param)) {
            const configArray = param as ISortConfiguration[];
            const funcs = configArray
                .map(config => (a: any, b: any): number => SortValueConverter.compare(a[config.propertyName], b[config.propertyName]) * ((config.direction || 'ascending') === 'ascending' ? 1 : -1));

            return array.sort((a, b) => {
                for (const func of funcs) {
                    const result: number = func(a, b);
                    if (result !== 0)
                        return result;
                }
                return 0;
            });
        } else {
            const config = param as ISortConfiguration;
            const factor = (config.direction || 'ascending') === 'ascending' ? 1 : -1;
            return array.sort((a, b) => {
                return SortValueConverter.compare(a[config.propertyName], b[config.propertyName]) * factor;
            });
        }
    }

    private static compare(a: any, b: any): number {
        if (typeof a === 'number') {
            const na: number = a;
            const nb: number = b;

            if (na > nb)
                return 1;
            if (na < nb)
                return -1;
        } else if (typeof a === 'string') {
            return (a as string).localeCompare(b as string);
        }

        return 0;
    }
}

interface ISortConfiguration {
    direction: 'ascending' | 'descending';
    propertyName: string;
}