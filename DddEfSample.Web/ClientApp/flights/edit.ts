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
    form: Model.FlightUpdateForm;
    error: Api.FlightUpdateError;
    
    async activate(params: RouteParams) {
        await this.loadFlight(params.id);
    }

    private async loadFlight(id: string) {
        this.flight = await this.apiClient.getFlightById(id);

        this.form = new Model.FlightUpdateForm();
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
        this.error = undefined;
        const result = await this.validator.validate();
        if (result.valid) {
            const result = await this.apiClient.updateFlight(this.flight.id, this.flight.eTag, {
                configuration: this.form.configuration,
            });
            if (result === 'conflict') {
                this.error = result;
            } else if (result === 'concurrent-update') {
                this.error = result;
                await this.loadFlight(this.flight.id);
            } else {
                this.router.navigateToRoute('details', { id: this.flight.id });
            }
        }
    }
}

export interface RouteParams {
    id: string;
}
