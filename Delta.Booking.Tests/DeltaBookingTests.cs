using System.Linq;

using Delta.Booking.WebApi.Model;
using Delta.Booking.WebApi.Services;

using NUnit.Framework;

namespace Delta.Booking.Tests
{
    [TestFixture()]
    public class DeltaBookingTests
    {
        private IFlightRepository _repository;

        [SetUp]
        public void Setup()
        {
            _repository = new FlightReposiroty();
        }

        [Test]
        public void Should_Return_Single_Rountrip_Flight()
        {
            //Arrange
            var expected = "1746"; //Flight Number

            var flightSearchModel = new FlightSearchModel
            {
                TripType = "RT",
                Origin = "LGA",
                Destination = "ATL",
                DepartureDate = "2/9/2019",
                ReturnDate = "2/9/2019",
                TotalPassengers = 1
            };

            //Act
            var actual = _repository.GetFlights(flightSearchModel).DepartingFlights.FirstOrDefault().FlightNumber;


            //Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Should_Return_Multiple_OneWay_Flights()
        {
            //Arrange
            var expected = 1;

            var flightSearchModel = new FlightSearchModel
            {
                TripType = "OW",
                Origin = "LGA",
                Destination = "ATL",
                DepartureDate = "2/9/2019",
                ReturnDate = "2/10/2019",
                TotalPassengers = 1
            };

            //Act
            var actual = _repository.GetFlights(flightSearchModel).DepartingFlights.Count;


            //Assert
            Assert.True(actual > expected);
        }

        [Test]
        public void Should_Return_Airport_Name()
        {
            //Arrange
            var airportCodeOrCityName = "LGA";
            var expected = "La Guardia";

            //Act
            var actual = _repository.GetAirports(airportCodeOrCityName).FirstOrDefault().AirportName;


            //Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Should_Return_Single_Airport()
        {
            //Arrange
            var airportCodeOrCityName = "LGA";
            var expected = 1;

            //Act
            var actual = _repository.GetAirports(airportCodeOrCityName).Count;


            //Assert
            Assert.IsTrue(actual.Equals(expected), "Single Airport search");
        }
    }
}