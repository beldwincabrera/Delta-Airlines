import { IFlight } from './flight.model';

export interface IFlightDetail {
    departingFlights: IFlight[];
    returningFlights: IFlight[];
}

