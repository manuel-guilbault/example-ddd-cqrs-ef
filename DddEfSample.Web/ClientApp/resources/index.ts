import { FrameworkConfiguration } from 'aurelia-framework';

export function configure(config: FrameworkConfiguration) {
    config.globalResources([
        './progress-bar',
        './async-button',

        './iso-date',
        './date-format',
        './duration',
        './number-format',
        './sort',
        './filter',
        './date-from-now',
        './from-base-64',
        './pretty-json'
    ]);
}