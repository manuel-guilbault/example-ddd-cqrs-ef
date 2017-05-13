import { ValidationRules } from 'aurelia-validation';

function isJsonParsable(value: string) {
    try {
        JSON.parse(value);
        return true;
    } catch (_) {
        return false;
    }
}

ValidationRules.customRule(
    'json',
    (value, _) =>
        value === null || value === undefined || value === '' || isJsonParsable(value),
    `\${$displayName} must be valid JSON`
);


const identifierPattern = '[a-z_][a-z0-9_]*';
const fullyQualifiedNamePattern = `(${identifierPattern}\\.)*${identifierPattern}`;
const csharpClassNameMatcher = new RegExp(`^\\s*${fullyQualifiedNamePattern}\\s*\\,\\s*${fullyQualifiedNamePattern}\\s*$`, 'i');

ValidationRules.customRule(
    'csharpClassName',
    (value, _) => value === null || value === undefined || value === '' || csharpClassNameMatcher.test(value),
    `\${$displayName} must be a valid C# fully qualified class name`
);

ValidationRules.customRule(
    'unique',
    (value: any[], _, keySelector: (v: any) => any) => {
        if (value === null || value === undefined) {
            return true;
        }

        keySelector = keySelector || (v => v);
        const grouped = value.reduce((map, v) => {
            const key = keySelector(v);
            const nbr = map.get(key) || 0;
            map.set(key, nbr + 1)
        }, new Map<any, number>());

        return Array.from(grouped.values())
            .filter(x => x > 1)
            .length === 0;
    },
    `\${$displayName} must contain only unique values`
);
