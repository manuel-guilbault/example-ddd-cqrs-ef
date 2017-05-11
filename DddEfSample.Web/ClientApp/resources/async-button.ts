import { bindable } from 'aurelia-framework';

export class AsyncButtonCustomElement {
    @bindable type = 'button';
    @bindable cssClass = '';
    @bindable disabled = false;
    @bindable task: Task = () => { };

    isTaskExecuting = false;

    async executeTask() {
        if (this.isTaskExecuting) {
            return;
        }

        this.isTaskExecuting = true;
        await Promise.resolve(this.task());
        this.isTaskExecuting = false;
    }
}

export type Task = () => Promise<void> | void;
