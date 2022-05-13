using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.User.Property
{
   public class TicketDetailsProperty
    {
        public string FlightId { get; set; } //pk of a AirlineFlightDetails

        public string? BookingId { get; set; } //pk of a FlightBookingDetails
        public int UserId { get; set; }

        public int Journey { get; set; } // 1=One Way and 2= Round Trip

        public decimal OneWayCost { get; set; }

        public decimal TwoWayCost { get; set; }

        public int TotalBookSeats { get; set; }

        public string CouponCode { get; set; }

        public decimal DiscountPrice { get; set; }

        public DateTime BookedDate { get; set; }

        public int type { get; set; }
    }
}
