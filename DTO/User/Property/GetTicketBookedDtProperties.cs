using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.User.Property
{
    public class GetTicketBookedDtProperties
    {
        public string FlightId { get; set; } //pk of a AirlineFlightDetails

        public string? BookingId { get; set; } //pk of a FlightBookingDetails
        public int UserId { get; set; }

        public int Journey { get; set; } // 1=One Way and 2= Round Trip

        public decimal TicketCost { get; set; }

        public string FromPlace { get; set; }

        public string ToPlace { get; set; }

        public int TotalBookSeats { get; set; }

        public string Name { get; set; }

        public int Gender { get; set; }

        public string Email { get; set; }

        public int Meal { get; set; }

        public int Age { get; set; }

        public DateTime BookedDate { get; set; }
    }
}
