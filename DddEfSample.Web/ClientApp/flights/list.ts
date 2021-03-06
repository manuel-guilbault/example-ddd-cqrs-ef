﻿import { autoinject } from 'aurelia-framework';
import { Api } from './api';

@autoinject
export class List {

    constructor(private readonly apiClient: Api.Client) { }

    flights: FlightViewModel[];

    async activate() {
        const flights = await this.apiClient.getFlights();
        this.flights = flights.map(f => (<FlightViewModel>{
            id: f.id,
            routing: f.routing,
            schedule: f.schedule,
            capacity: f.configuration
                .map(x => x.capacity)
                .reduce((total, capacity) => total + capacity, 0),
            bookedSeats: f.bookingsSummary
                .map(x => x.numberOfBookedSeats)
                .reduce((total, seats) => total + seats, 0),
        }));
    }
}

export interface FlightViewModel {
    id: string;
    routing: Api.Routing;
    schedule: Api.Schedule;
    capacity: number;
    bookedSeats: number;
}