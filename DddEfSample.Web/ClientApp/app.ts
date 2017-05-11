import { RouterConfiguration, Router } from 'aurelia-router';

export class App {
    
    router: Router;

    configureRouter(config: RouterConfiguration, router: Router): void {
        config.title = 'DDD EF Sample';
        config.options.pushState = true;
        config.map([
            { route: '', redirect: 'flights' },
            { route: 'flights', name: 'flights', moduleId: './flights/main', title: 'Flights', nav: true },
        ]);
        this.router = router;
    }
}
