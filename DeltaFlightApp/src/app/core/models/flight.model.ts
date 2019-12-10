export interface IFlight {

    id: string;
    flightIdentifier: string;
    flightNumber: string;
    scheduledOriginGate: string;
    scheduledDestinationGate: string;
    departure: Date;
    arrival: Date;
    dateCreated: string;
    dateUpdated: string;
    destination: string;
    origin: string;
    destinationName: string;
    originName: string;
    selected: boolean;
}

