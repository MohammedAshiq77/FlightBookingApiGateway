using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.Admin.Properties
{
   public class AirLineSearchProperties
    {
        public string FlightNumber { get; set; }

        public string AirLine { get; set; }

        public string AirLineType { get; set; }
      //  public string From { get; set; }

       // public string To { get; set; }

       // public DateTime FlightStartDateTime { get; set; }

       // public DateTime FlightToDateTime { get; set; }

        public int TotalBusinessSeats { get; set; }

        public int TotalNonBusinessSeats { get; set; }

        public decimal BusinessTicketCost { get; set; }

        public decimal NonBusinessTicketCost { get; set; }

        public int FlightSeatRow { get; set; }

        //public string Meal { get; set; }
    }
}
