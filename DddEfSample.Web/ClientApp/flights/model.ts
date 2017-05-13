import { ValidationRules } from 'aurelia-validation';
import { Api } from './api';

export module Model {

    export class FlightCreationForm {
        constructor() {
            ValidationRules
                .ensure((x: FlightCreationForm) => x.configuration)
                    .required()
                    .satisfiesRule('unique', (x: PhysicalClassCapacityForm) => x.physicalClass)
                        .withMessage(`\${$displayName} must not contain multiple capacity values for the same physical class`)
                .on(this);
        }

        readonly routing = new RoutingForm();
        readonly schedule = new ScheduleForm();
        readonly configuration = [
            new PhysicalClassCapacityForm('Y'),
            new PhysicalClassCapacityForm('M'),
            new PhysicalClassCapacityForm('C')
        ];
    }

    export class FlightUpdateForm {
        constructor() {
            ValidationRules
                .ensure((x: FlightCreationForm) => x.configuration)
                .required()
                .satisfiesRule('unique', (x: PhysicalClassCapacityForm) => x.physicalClass)
                .withMessage(`\${$displayName} must not contain multiple capacity values for the same physical class`)
                .on(this);
        }
        
        readonly configuration = [
            new PhysicalClassCapacityForm('Y'),
            new PhysicalClassCapacityForm('M'),
            new PhysicalClassCapacityForm('C')
        ];
    }

    export class RoutingForm {
        constructor() {
            ValidationRules
                .ensure((x: RoutingForm) => x.departureCity).required().maxLength(100)
                .ensure(x => x.arrivalCity).required().maxLength(100)
                .on(this);
        }

        departureCity = '';
        arrivalCity = '';
    }

    export class ScheduleForm {
        constructor() {
            ValidationRules
                .ensure((x: ScheduleForm) => x.checkInAt).required()
                .ensure(x => x.departureAt).required()
                .ensure(x => x.arrivalAt).required()
                .on(this);
        }

        checkInAt = new Date();
        departureAt = new Date();
        arrivalAt = new Date();
    }

    export class PhysicalClassCapacityForm {

        constructor(public physicalClass: Api.PhysicalClassIataCode, public capacity = 0) {
            ValidationRules
                .ensure((x: PhysicalClassCapacityForm) => x.physicalClass).required()
                .ensure(x => x.capacity).required()
                .on(this);
        }
    }
}