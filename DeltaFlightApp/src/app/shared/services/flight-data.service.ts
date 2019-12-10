import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { map, tap, retry, catchError } from 'rxjs/operators';
import { debounceTime } from 'rxjs/internal/operators/debounceTime';
import { throwError, Observable, of } from 'rxjs';
import { IAirportDetail } from 'src/app/core/models/airport-detail.model';
import { FlightSearch } from 'src/app/core/models/flight-search.model';
import { IFlightDetail } from 'src/app/core/models/flight-detail.model';

@Injectable({
  providedIn: 'root'
})
export class FlightDataService {

  baseUrl: string = "https://localhost:44392";

  constructor(private http: HttpClient) { }

  searchAirportByCodeOrCity(code: string): Observable<IAirportDetail[]> {
    return this.http.get<IAirportDetail[]>(`${this.baseUrl}/airports?code=${code}`)
      .pipe(catchError(this.handleError<IAirportDetail[]>('airports', [])));
  }



  searchAvailableFlights(flightSearch: FlightSearch): Observable<IFlightDetail> {

    let headers = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    };

    let body = JSON.stringify(flightSearch);

    return this.http.post<IFlightDetail>(`${this.baseUrl}/flights`, body, headers)
      .pipe(catchError(this.handleError<IFlightDetail>('flights', null)));
  }

  /**
 * Handle Http operation that failed.
 * Let the app continue.
 * @param operation - name of the operation that failed
 * @param result - optional value to return as the observable result
 */
  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {

      // TODO: send the error to remote logging infrastructure
      console.error(error); // log to console instead

      // TODO: better job of transforming error for user consumption
      console.log(`${operation} failed: ${error.message}`);

      // Let the app keep running by returning an empty result.
      return of(result as T);
    };
  }
}
