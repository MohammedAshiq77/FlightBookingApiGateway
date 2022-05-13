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

        public decimal DiscountPrice { get; set; }

        public DateTime BookedDate { get; set; }

        public string UserName { get; set; }

        public string Pnr { get; set; }

        public string UserEmail { get; set; }

        public int Gender { get; set; }

        public int Age { get; set; }
        public int Meal { get; set; }

        public int SeatNumber { get; set; }

        public DateTime CreateDate { get; set; }

        public int type { get; set; }
        public int Flag { get; set; }



       // public List<TicketDetailsProperty> TicketDetailsPropertiesList { get; set; }
        //public List<UserDetailsProperty> userBookingDetailsPropertyList { get; set; }
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
