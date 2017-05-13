import { autoinject } from 'aurelia-framework';
import { ValidationController, ValidationRules } from 'aurelia-validation';
import { RoutableComponentActivate, Router } from 'aurelia-router';

import { Api } from './api';
import { Model } from './model';

@autoinject
export class Book implements RoutableComponentActivate {

    constructor(
        private readonly apiClient: Api.Client,
        private readonly validator: ValidationController,
        private readonly router: Router
    ) {
        ValidationRules
            .ensure((x: Book) => x.physicalClass).required()
            .ensure(x => x.numberOfSeats).required()
            .on(this);
    }

    flight: Api.FlightSummary;

    physicalClass: Api.PhysicalClassIataCode = 'Y';
    numberOfSeats = 0;

    async activate(params: RouteParams) {
        this.flight = await this.apiClient.getFlightById(params.id);
    }

    async save() {
        const result = await this.validator.validate();
        if (result.valid) {
            await this.apiClient.book({
                flightId: this.flight.id,
                physicalClass: this.physicalClass,
                numberOfSeats: parseInt(this.numberOfSeats.toString())
            });
            this.router.navigateToRoute('details', { id: this.flight.id });
        }
    }
}

export interface RouteParams {
    id: string;
}
