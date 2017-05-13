import { RouterConfiguration, Router } from 'aurelia-router';

export class Flights {

    router: Router;

    configureRouter(config: RouterConfiguration, router: Router): void {
        config.map([
            { route: '', name: 'list', moduleId: './list' },
            { route: 'new', name: 'create', moduleId: './create' },
            { route: ':id', name: 'details', moduleId: './details' },
            { route: ':id/edit', name: 'edit', moduleId: './edit' },
            { route: ':id/book', name: 'book', moduleId: './book' },
        ]);
        this.router = router;
    }
}
