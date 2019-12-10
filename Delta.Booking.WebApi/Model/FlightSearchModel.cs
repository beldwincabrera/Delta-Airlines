using System.ComponentModel.DataAnnotations;

namespace Delta.Booking.WebApi.Model
{
    public class FlightSearchModel
    {
        [Required]
        public string TripType { get; set; }

        [Required]
        public string Origin { get; set; }

        [Required]
        public string Destination { get; set; }

        [Required]
        public string DepartureDate { get; set; }

        public string ReturnDate { get; set; }

        [Required]
        public int TotalPassengers { get; set; }

    }
}
