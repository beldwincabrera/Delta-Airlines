import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatAutocompleteModule, MatInputModule, MatCardModule, MatGridListModule, MatRadioModule, MatDividerModule, MatDatepickerModule, MatNativeDateModule, MatButtonModule, MatSliderModule, MatTableModule, MatCheckboxModule } from '@angular/material';
import { SharedModule } from './shared/shared.module';
import { FlightBookingComponent } from './booking/flight-booking/flight-booking.component';
import { FlightListComponent } from './booking/flight-list/flight-list.component';



@NgModule({
  declarations: [
    AppComponent,
    FlightBookingComponent,
    FlightListComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    SharedModule,

    //Angular Material
    MatGridListModule,
    MatAutocompleteModule,
    MatInputModule,
    MatCardModule,
    MatRadioModule,
    MatDividerModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatButtonModule,
    MatSliderModule,
    MatTableModule,
    MatCheckboxModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
