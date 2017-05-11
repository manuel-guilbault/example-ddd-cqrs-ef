import { bindable, noView } from 'aurelia-framework';
import * as NProgress from 'nprogress';

@noView(['./progress-bar.css'])
export class ProgressBarCustomElement {

    @bindable visible = false;
    
    visibleChanged() {
        if (this.visible) {
            NProgress.start();
        } else {
            NProgress.done();
        }
    }
}