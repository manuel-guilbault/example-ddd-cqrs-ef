import 'isomorphic-fetch';
//import 'jquery';
//import 'bootstrap';
import 'bootstrap/dist/css/bootstrap.css';
import 'font-awesome/css/font-awesome.css';
import { Aurelia } from 'aurelia-framework';
import { HttpClient } from 'aurelia-fetch-client';

declare const IS_DEV_BUILD: boolean; // The value is supplied by Webpack during the build

export async function configure(aurelia: Aurelia) {
    aurelia.use
        .standardConfiguration()
        .feature('resources')
        .feature('validation')
        .instance(HttpClient, new HttpClient().configure(c => c
            .useStandardConfiguration()
        ));

    if (IS_DEV_BUILD) {
        aurelia.use.developmentLogging();
    }
    
    await aurelia.start();
    aurelia.setRoot('app');
}
