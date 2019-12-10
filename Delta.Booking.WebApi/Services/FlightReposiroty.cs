using System;
using System.Collections.Generic;
using System.Linq;

using Delta.Booking.WebApi.Model;

namespace Delta.Booking.WebApi.Services
{
    public class FlightReposiroty : IFlightRepository
    {
        private readonly FlightDataService _flightDataService;
        private readonly List<FlightModel> _flightData;
        private readonly List<AirportModel> _airportData;

        public FlightReposiroty()
        {
            _flightDataService = FlightDataService.Instance;
            _airportData = _flightDataService.LoadAirportData();
            _flightData = _flightDataService.LoadFlightData();
        }

        public ICollection<AirportModel> GetAirports(string airportCodeOrCityName)
        {
            var airports = _airportData.Where(x => x.AirportCode.Equals(airportCodeOrCityName, StringComparison.InvariantCultureIgnoreCase) ||
                                                    x.AirportName.StartsWith(airportCodeOrCityName, StringComparison.InvariantCultureIgnoreCase))
                                       .ToList();
            return airports;
        }

        public FlightListModel GetFlights(FlightSearchModel flightSearch)
        {
            var flightsResult = new FlightListModel();

            if (!DateTime.TryParse(flightSearch.DepartureDate, out DateTime departureDate))
            {
                throw new Exception("Failed to parse Departure date.");
            }

            if (_flightData != null && _flightData.Any())
            {
                //Roundtrip
                if (flightSearch.TripType.Equals("RT"))
                {
                    if ((!DateTime.TryParse(flightSearch.ReturnDate, out DateTime returnDate))) { throw new Exception("Failed to parse Return date."); }

                    //Rountrip Query - Departing Flights
                    flightsResult.DepartingFlights = _flightData.Where(x => x.Origin.Equals(flightSearch.Origin, StringComparison.InvariantCultureIgnoreCase) &&
                                                                            x.Destination.Equals(flightSearch.Destination, StringComparison.InvariantCultureIgnoreCase) &&
                                                                            x.Departure.Date.Equals(departureDate.Date))
                                                                .OrderBy(x => x.Departure)
                                                                .ThenBy(x => x.FlightNumber)
                                                                .ThenBy(x => x.OriginName)
                                                                .ThenBy(x => x.Destination)
                                                                .ToList();

                    //Rountrip Query - Returning Flights
                    flightsResult.ReturningFlights = _flightData.Where(x => x.Origin.Equals(flightSearch.Destination, StringComparison.InvariantCultureIgnoreCase) &&
                                                                            x.Destination.Equals(flightSearch.Origin, StringComparison.InvariantCultureIgnoreCase) &&
                                                                            (x.Departure.Date > returnDate.Date && x.Departure.Date >= departureDate.Date))
                                                                .OrderBy(x => x.Departure)
                                                                .ThenBy(x => x.FlightNumber)
                                                                .ThenBy(x => x.OriginName)
                                                                .ThenBy(x => x.Destination)
                                                                .ToList();
                }
                else
                {
                    //One-Way
                    flightsResult.DepartingFlights = _flightData.Where(x => x.Origin.Equals(flightSearch.Origin, StringComparison.InvariantCultureIgnoreCase) &&
                                                           x.Destination.Equals(flightSearch.Destination, StringComparison.InvariantCultureIgnoreCase) &&
                                                           x.Departure.Date.Equals(departureDate.Date))
                                                .OrderBy(x => x.Departure)
                                                .ThenBy(x => x.FlightNumber)
                                                .ThenBy(x => x.OriginName)
                                                .ThenBy(x => x.Destination)
                                               .ToList();
                }


            }
            return flightsResult;
        }
    }
}
