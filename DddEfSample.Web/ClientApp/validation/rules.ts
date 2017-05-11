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
