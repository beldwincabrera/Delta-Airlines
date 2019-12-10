import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { FormControl, FormGroup, Validators, FormBuilder } from '@angular/forms';
import { MatDatepickerInputEvent } from '@angular/material/datepicker';
import { DatePipe } from '@angular/common';
import { FlightSearch } from 'src/app/core/models/flight-search.model';
import { IAirportDetail } from 'src/app/core/models/airport-detail.model';
import { IFlightDetail } from 'src/app/core/models/flight-detail.model';
import { FlightDataService } from 'src/app/shared/services/flight-data.service';
import { SharedService } from 'src/app/shared/services/shared-service.service';

@Component({
  selector: 'app-flight-booking',
  templateUrl: './flight-booking.component.html',
  styleUrls: ['./flight-booking.component.scss']
})

export class FlightBookingComponent implements OnInit {

  title = 'Delta Airlines';

  flightSearch: FlightSearch;
  originAirportList: IAirportDetail[];
  destinationAirportList: IAirportDetail[];
  flights: IFlightDetail[];
  submitted: boolean = false;

  //Flight Search Form
  form: FormGroup;

  pipe: DatePipe = new DatePipe('en-US');
  returnDateHidden: boolean = false;

  constructor(private formBuilder: FormBuilder, private service: FlightDataService, private sharedService: SharedService) { }

  ngOnInit() {
    this.buildForm();
    this.setConditionalValidators();
  }

  buildForm(): void {

    this.form = this.formBuilder.group({
      tripType: new FormControl('', []),
      origin: new FormControl('', [Validators.required, Validators.minLength(3)]),
      destination: new FormControl('', [Validators.required, Validators.minLength(3)]),
      departureDate: new FormControl('', [Validators.required]),
      returnDate: new FormControl('', [Validators.required]),
      totalPassengers: new FormControl()
    });

    //Set Trip Type to Round Trip by default
    this.form.controls.tripType.setValue("RT");
    this.form.controls.totalPassengers.setValue(1);

    //Origin Airport City Search
    this.form.controls.origin.valueChanges.subscribe(airportOrCityName => {
      if (airportOrCityName && airportOrCityName != '' && airportOrCityName.length >= 3) {
        this.service.searchAirportByCodeOrCity(airportOrCityName).subscribe(airports => {
          this.originAirportList = airports;
        })
      }
    });

    //Destination Airport City Search
    this.form.controls.destination.valueChanges.subscribe(airportOrCityName => {
      if (airportOrCityName && airportOrCityName != '' && airportOrCityName.length >= 3) {
        this.service.searchAirportByCodeOrCity(airportOrCityName).subscribe(airports => {
          this.destinationAirportList = airports;
        })
      }
    });

  }

  setConditionalValidators(): void {
    this.form.controls.tripType.valueChanges.subscribe(tripType => {
      if (tripType === 'RT') {
        this.form.controls.returnDate.setValidators([Validators.required]);
        this.form.updateValueAndValidity({
          onlySelf: true
        });
      } else {
        this.form.controls.returnDate.setValidators(null);
        this.form.controls.returnDate.clearValidators();
        this.form.updateValueAndValidity({
          onlySelf: true
        });
      }
    });
  }


  onDepartureDate(departureDate: MatDatepickerInputEvent<Date>): void {
    this.form.controls.departureDate.setValue(departureDate.value);
  }

  onReturnDate(returnDate: MatDatepickerInputEvent<Date>): void {
    this.form.controls.returnDate.setValue(returnDate.value);
  }

  onTripTypeChange(event: any) {
    this.returnDateHidden = (this.form.controls.tripType.value === 'OW') ? true : false;
  }

  reset(): void {

    //Reset Form
    this.submitted = false;
    this.form.reset();

    //Reset Flight Search Model
    this.flightSearch = null;

    //Broadcast Flight Search Criteria (Null to reset the view)
    this.sharedService.flightSearchBehaviorEmitter.next(this.flightSearch);
  }

  get f() { return this.form.controls; }

  submit(event: any): void {

    event.preventDefault();
    this.submitted = true;

    //Flight Search Criteria Model
    this.flightSearch = new FlightSearch();
    this.flightSearch.tripType = this.form.controls.tripType.value;
    this.flightSearch.origin = this.form.controls.origin.value;
    this.flightSearch.destination = this.form.controls.destination.value;
    this.flightSearch.departureDate = this.pipe.transform(this.form.controls.departureDate.value, 'MM/dd/yyyy');
    this.flightSearch.returnDate = this.pipe.transform(this.form.controls.returnDate.value, 'MM/dd/yyyy');

    //Broadcast Flight Search Criteria
    this.sharedService.flightSearchBehaviorEmitter.next(this.flightSearch);
  }

}
