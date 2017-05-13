import { autoinject } from 'aurelia-framework';
import { RoutableComponentActivate } from 'aurelia-router';

import { Api } from './api';

@autoinject
export class Details implements RoutableComponentActivate {

    constructor(private readonly apiClient: Api.Client) { }

    flight: Api.FlightSummary;

    async activate(params: RouteParams) {
        this.flight = await this.apiClient.getFlightById(params.id);
    }
}

export interface RouteParams {
    id: string;
}
