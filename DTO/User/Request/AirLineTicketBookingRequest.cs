using DTO.User.Property;
using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.User.Request
{
   public class AirLineTicketBookingRequest
    {
        public string FlightId { get; set; } //pk of a AirlineFlightDetails

        public string? BookingId { get; set; } //pk of a FlightBookingDetails
        public int UserId { get; set; }

        public int Journey { get; set; } // 1=One Way and 2= Round Trip

        public decimal OneWayCost { get; set; }

        public decimal TwoWayCost { get; set; }

        public int TotalBookSeats { get; set; }

        public string CouponCode { get; set; }

        public DateTime BookedDate { get; set; }



        public List<UserDetailsProperty> userBookingDetailsPropertyList { get; set; }
    }
    public class GetBookedTicketDTRequest
    {
        public string PNR { get; set; }
    }

    public class GetBookedTicketDTHisRequest
    {
        public string EmailId { get; set; }
    }
}
