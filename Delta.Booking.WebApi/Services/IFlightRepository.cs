using System.Collections.Generic;

using Delta.Booking.WebApi.Model;

namespace Delta.Booking.WebApi.Services
{
    public interface IFlightRepository
    {
        ICollection<AirportModel> GetAirports(string airportCodeOrCityName);

        FlightListModel GetFlights(FlightSearchModel flightSearch);
    }
}
