
using System;

using Newtonsoft.Json;

namespace Delta.Booking.WebApi
{
    public class FlightModel
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("flight_identifier")]
        public string FlightIdentifier { get; set; }

        [JsonProperty("flt_num")]
        public string FlightNumber { get; set; }

        [JsonProperty("scheduled_origin_gate")]
        public string ScheduledOriginGate { get; set; }

        [JsonProperty("scheduled_destination_gate")]
        public string ScheduledDestinationGate { get; set; }

        [JsonProperty("out_gmt")]
        public DateTime Departure { get; set; }

        [JsonProperty("in_gmt")]
        public DateTime Arrival { get; set; }

        [JsonProperty("created_at")]
        public DateTime DateCreated { get; set; }

        [JsonProperty("updated_at")]
        public DateTime DateUpdated { get; set; }


        [JsonProperty("destination")]
        public string Destination { get; set; }

        [JsonProperty("origin")]

        public string Origin { get; set; }


        [JsonProperty("destination_full_name")]

        public string DestinationName { get; set; }

        [JsonProperty("origin_full_name")]
        public string OriginName { get; set; }
    }
}
