import { autoinject } from 'aurelia-framework';
import { ValidationController, ValidationRules } from 'aurelia-validation';
import { Router } from 'aurelia-router';

import { Api } from './api';
import { Model } from './model';

@autoinject
export class Create {

    constructor(
        private readonly apiClient: Api.Client,
        private readonly validator: ValidationController,
        private readonly router: Router
    ) { }

    readonly form = new Model.FlightCreationForm();

    async save() {
        const result = await this.validator.validate();
        if (result.valid) {
            await this.apiClient.createFlight(this.form);
            this.router.navigateToRoute('list');
        }
    }
}
