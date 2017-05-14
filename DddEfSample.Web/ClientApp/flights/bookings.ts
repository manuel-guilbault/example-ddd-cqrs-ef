import { autoinject } from 'aurelia-framework';
import { RoutableComponentActivate } from 'aurelia-router';

import { Api } from './api';

@autoinject
export class Bookings implements RoutableComponentActivate {

    constructor(private readonly apiClient: Api.Client) { }

    flightId: string;
    bookings: Api.Booking[];

    async activate(params: RouteParams) {
        this.bookings = await this.apiClient.getBookings(this.flightId = params.id);
    }
}

export interface RouteParams {
    id: string;
}
