using System.Collections.Generic;

namespace Delta.Booking.WebApi
{
    public class FlightListModel
    {
        public ICollection<FlightModel> DepartingFlights { get; set; } = new List<FlightModel>();

        public ICollection<FlightModel> ReturningFlights { get; set; } = new List<FlightModel>();
    }
}
