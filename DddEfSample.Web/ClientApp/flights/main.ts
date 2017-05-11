import { RouterConfiguration, Router } from 'aurelia-router';

export class Flights {

    router: Router;

    configureRouter(config: RouterConfiguration, router: Router): void {
        config.map([
            { route: '', name: 'list', moduleId: './list' },
            { route: ':id', name: 'details', moduleId: './details' },
        ]);
        this.router = router;
    }
}
