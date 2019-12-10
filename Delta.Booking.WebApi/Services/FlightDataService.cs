using System;
using System.Collections.Generic;
using System.Linq;

using Delta.Booking.WebApi.Model;

using Newtonsoft.Json;

namespace Delta.Booking.WebApi.Services
{

    public sealed class FlightDataService
    {
        private static FlightDataService instance;
        private static volatile object _lock = new Object();
        private FlightDataService() { }

        public static FlightDataService Instance
        {
            get
            {
                // 4- all threads that did not reach lock will fail this check
                if (instance == null)
                {
                    // 1- all threads wait here except for the first one that gets here
                    lock (_lock)
                    {
                        // 3- all threads waiting at the lock will fail this check
                        if (instance == null)
                        {
                            // 2- only the first thread will reach here
                            instance = new FlightDataService();
                        }
                    }
                }
                // 5- all threads will eventually get here
                return instance;
                // The instance above has been created by the
                // first thread to reach the lock
                // all other threads, weather they reached
                // the lock or not, will return the
                // Instance created  by the first thread.
            }
        }

        public List<FlightModel> LoadFlightData()
        {
            string serializedJsonData = System.IO.File.ReadAllText(@"data/airport_data.json");

            var flights = JsonConvert.DeserializeObject<List<FlightModel>>(serializedJsonData);

            return flights;
        }

        public List<AirportModel> LoadAirportData()
        {
            var flights = LoadFlightData();
            var airports = new List<AirportModel>();

            foreach (var airport in flights)
            {
                //Find All Origins
                var exists = airports.Any(x => x.AirportCode.Equals(airport.Origin, StringComparison.InvariantCultureIgnoreCase));
                if (!exists)
                {
                    airports.Add(new AirportModel
                    {
                        Id = airport.Id,
                        AirportCode = airport.Origin,
                        AirportName = airport.OriginName,
                        DisplayName = $"{airport.OriginName} ({airport.Origin})"
                    });
                }

                //Find All Destinations
                exists = airports.Any(x => x.AirportCode.Equals(airport.Destination, StringComparison.InvariantCultureIgnoreCase));
                if (!exists)
                {
                    airports.Add(new AirportModel
                    {
                        Id = airport.Id,
                        AirportCode = airport.Destination,
                        AirportName = airport.DestinationName,
                        DisplayName = $"{airport.DestinationName} ({airport.Destination})"
                    });
                }
            }

            return airports;
        }
    }
}
