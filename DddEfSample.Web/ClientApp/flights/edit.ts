import { autoinject } from 'aurelia-framework';
import { ValidationController } from 'aurelia-validation';
import { RoutableComponentActivate, Router } from 'aurelia-router';

import { Api } from './api';
import { Model } from './model';

@autoinject
export class Edit implements RoutableComponentActivate {

    constructor(
        private readonly apiClient: Api.Client,
        private readonly validator: ValidationController,
        private readonly router: Router
    ) { }

    flight: Api.FlightSummary;
    readonly form = new Model.FlightUpdateForm();
    
    async activate(params: RouteParams) {
        this.flight = await this.apiClient.getFlightById(params.id);

        for (let item of this.flight.configuration) {
            const physicalClassForm = this.form.configuration.find(x => x.physicalClass === item.physicalClass);
            if (physicalClassForm) {
                physicalClassForm.capacity = item.capacity;
            } else {
                this.form.configuration.push(new Model.PhysicalClassCapacityForm(item.physicalClass, item.capacity));
            }
        }
    }

    async save() {
        const result = await this.validator.validate();
        if (result.valid) {
            await this.apiClient.updateFlight(this.flight.id, {
                eTag: this.flight.eTag,
                configuration: this.form.configuration,
            });
            this.router.navigateToRoute('details', { id: this.flight.id });
        }
    }
}

export interface RouteParams {
    id: string;
}
