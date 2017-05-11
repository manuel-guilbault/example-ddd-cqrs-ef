import { autoinject } from 'aurelia-framework';
import { ValidationController, ValidationRules } from 'aurelia-validation';
import { Router } from 'aurelia-router';

import { Api } from './api';

@autoinject
export class Create {

    constructor(private readonly apiClient: Api.Client, private readonly validator: ValidationController, private readonly router: Router) { }

    form = new FlightForm();
}

export class FlightForm implements Api.FlightProperties {

    constructor() {
        ValidationRules
            .ensure((x: FlightForm) => x.departureCity).required().maxLength(100)
            .ensure(x => x.arrivalCity).required().maxLength(100)
            .ensure(x => x.departingAt).required()
            .ensure(x => x.configuration).required().satisfies(value => {
                const byPhysicalClass = value.reduce(
                    (existing, pc) => existing.set(pc.physicalClass, (existing.get(pc.physicalClass) || 0) + 1),
                    new Map<Api.PhysicalClassIataCode, number>());
                return Array.from(byPhysicalClass.values())
                    .filter(x => x > 1)
                    .length === 0;
            }).withMessage(`\${$displayName} must not contain multiple capacity values for the same physical class`)
            .on(this);
    }

    departureCity = '';
    arrivalCity = '';
    departingAt = new Date();
    configuration: PhysicalClassCapacityForm[] = [];

    addPhysicalClassCapacity() {
        this.configuration.push(new PhysicalClassCapacityForm());
    }

    removePhysicalClassCapacity(index: number) {
        this.configuration.remove(
    }
}

export class PhysicalClassCapacityForm implements Api.PhysicalClassCapacity {

    constructor() {
        ValidationRules
            .ensure((x: PhysicalClassCapacityForm) => x.physicalClass).required()
            .ensure(x => x.capacity).required()
            .on(this);
    }

    physicalClass: Api.PhysicalClassIataCode = 'Y';
    capacity = 0;
}
