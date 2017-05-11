import { FrameworkConfiguration } from 'aurelia-framework';
import { BootstrapFormRenderer } from './renderer';
import './rules';

export function configure(config: FrameworkConfiguration) {
    config.plugin('aurelia-validation');
    config.container.registerSingleton('bootstrap-validation-renderer', BootstrapFormRenderer);
}