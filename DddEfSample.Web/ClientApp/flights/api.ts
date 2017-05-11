import { autoinject } from 'aurelia-framework';
import { HttpClient, json } from 'aurelia-fetch-client';

export module Api {

    @autoinject
    export class Client {

        constructor(private readonly http: HttpClient) {
            http.configure(c => c.withBaseUrl(`api/Flights/`));
        }

        async getFlights() {
            const response = await this.http.fetch(``);
            const flightSummaries = await response.json();
            return flightSummaries as FlightSummary[];
        }

        async getFlightById(id: string) {
            const response = await this.http.fetch(id);
            const flightSummary = await response.json();
            return flightSummary as FlightSummary;
        }

        async createFlight(properties: FlightProperties) {
            await this.http.fetch(``, { method: 'POST', body: json(properties) });
        }

        async updateConfiguration(flightId: string, configuration: Configuration) {
            await this.http.fetch(`${flightId}/Configuration`, { method: 'PUT', body: json(configuration) });
        }

        async getBookings(flightId: string) {
            const response = await this.http.fetch(`${flightId}/Bookings`);
            const bookings = await response.json();
            return bookings as Booking[];
        }

        async book(flightId: string, properties: BookingProperties) {
            await this.http.fetch(`${flightId}/Bookings`, { method: 'POST', body: json(properties) });
        }
    }

    export type PhysicalClassIataCode = 'Y' | 'M' | 'C';

    export interface FlightSummary {
        id: string;
        createdAt: Date;
        modifiedAt: Date;
        departureCity: string;
        arrivalCity: string;
        departingAt: Date;
        configuration: Configuration;
        bookingSummary: BookingSummary;
    }

    export type Configuration = PhysicalClassCapacity[];

    export interface PhysicalClassCapacity {
        physicalClass: PhysicalClassIataCode;
        capacity: number;
    }

    export type BookingSummary = PhysicalClassBookingSummary[];

    export interface PhysicalClassBookingSummary {
        physicalClass: PhysicalClassIataCode;
        numberOfBookedSeats: number;
    }

    export interface FlightProperties {
        departureCity: string;
        arrivalCity: string;
        departingAt: Date;
        configuration: Configuration;
    }

    export interface Booking {
        id: string;
        bookedAt: Date;
        physicalClass: PhysicalClassIataCode;
        numberOfSeats: number;
    }

    export interface BookingProperties {
        physicalClass: PhysicalClassIataCode;
        numberOfSeats: number;
    }
}