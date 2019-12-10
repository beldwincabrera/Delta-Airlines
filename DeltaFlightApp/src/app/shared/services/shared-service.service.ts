import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { FlightSearch } from 'src/app/core/models/flight-search.model';

@Injectable({
  providedIn: 'root'
})
export class SharedService {

  flightSearchBehaviorEmitter: BehaviorSubject<FlightSearch> = new BehaviorSubject<FlightSearch>(null);

  constructor() { }
}
