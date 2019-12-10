import { Component, OnInit, Input } from '@angular/core';
import { IFlightDetail } from 'src/app/core/models/flight-detail.model';
import { Observable } from 'rxjs';
import { SharedService } from 'src/app/shared/services/shared-service.service';
import { FlightSearch } from 'src/app/core/models/flight-search.model';
import { FlightDataService } from 'src/app/shared/services/flight-data.service';

@Component({
  selector: 'app-flight-list',
  templateUrl: './flight-list.component.html',
  styleUrls: ['./flight-list.component.scss']
})
export class FlightListComponent implements OnInit {

  //Flight Grid Table Columns
  displayedColumns: string[] = ['flightNumber', 'origin', 'destination', 'departure', 'arrival', 'selected'];
  displayFlightResults: boolean = false;
  displayReturningFlights: boolean = false;
  departingFlights: IFlightDetail[];
  returningFlights: IFlightDetail[];

  constructor(private service: FlightDataService, private sharedService: SharedService) { }

  ngOnInit() {
    this.performFlightSearch();
  }

  performFlightSearch(): void {

    //Receives Behavior Subject Event and performs the search operation
    this.sharedService.flightSearchBehaviorEmitter.subscribe((flightSearch: FlightSearch) => {

      if (flightSearch == null || flightSearch.origin == null || flightSearch.destination == null) {
        this.departingFlights = [];
        this.returningFlights = [];
        this.displayFlightResults = false;
        this.displayReturningFlights = false;
        return;
      }

      //Perform Flight Search
      this.service.searchAvailableFlights(flightSearch).subscribe(data => {
        if (data) {
          this.departingFlights = data.departingFlights;
          this.returningFlights = data.returningFlights;
          this.displayFlightResults = data.departingFlights.length > 0 || data.returningFlights.length > 0;
          this.displayReturningFlights = flightSearch.tripType === 'RT';
        }
      });

    });
  }


}
