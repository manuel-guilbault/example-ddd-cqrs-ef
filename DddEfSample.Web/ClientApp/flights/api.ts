import { autoinject } from 'aurelia-framework';
import { HttpClient, json } from 'aurelia-fetch-client';

export module Api {

    @autoinject
    export class Client {

        constructor(private readonly http: HttpClient) {
            http.configure(c => c.withBaseUrl(`api/`));
        }

        async getFlights() {
            const response = await this.http.fetch(`flights`);
            const flightSummaries = await response.json();
            return flightSummaries as FlightSummary[];
        }

        async getFlightById(id: string) {
            const response = await this.http.fetch(`flights/${id}`);
            const flightSummary = await response.json();
            return flightSummary as FlightSummary;
        }

        async createFlight(model: FlightCreationModel) {
            await this.http.fetch(`flights`, { method: 'POST', body: json(model) });
        }

        async updateFlight(id: string, model: FlightUpdateModel) {
            await this.http.fetch(`flights/${id}`, { method: 'PUT', body: json(model) });
        }

        async getBookings(flightId: string) {
            const response = await this.http.fetch(`bookings?flightId=${flightId}`);
            const bookings = await response.json();
            return bookings as Booking[];
        }

        async book(model: BookingCreationModel) {
            await this.http.fetch(`bookings`, { method: 'POST', body: json(model) });
        }
    }

    export type PhysicalClassIataCode = 'Y' | 'M' | 'C';

    export interface FlightSummary {
        id: string;
        eTag: string;
        routing: Routing;
        schedule: Schedule;
        configuration: Configuration;
        bookingSummary: FlightBookingsSummary;
    }

    export interface Routing {
        departureCity: string;
        arrivalCity: string;
    }

    export interface Schedule {
        checkInAt: Date;
        departureAt: Date;
        arrivalAt: Date;
    }

    export type Configuration = PhysicalClassCapacity[];

    export interface PhysicalClassCapacity {
        physicalClass: PhysicalClassIataCode;
        capacity: number;
    }

    export type FlightBookingsSummary = PhysicalClassBookingsSummary[];

    export interface PhysicalClassBookingsSummary {
        physicalClass: PhysicalClassIataCode;
        numberOfBookedSeats: number;
    }

    export interface FlightCreationModel {
        routing: Routing;
        schedule: Schedule;
        configuration: Configuration;
    }

    export interface FlightUpdateModel {
        eTag: string;
        configuration: Configuration;
    }

    export interface Booking {
        id: string;
        flightId: string;
        bookedAt: Date;
        physicalClass: PhysicalClassIataCode;
        numberOfSeats: number;
    }

    export interface BookingCreationModel {
        flightId: string;
        physicalClass: PhysicalClassIataCode;
        numberOfSeats: number;
    }
}