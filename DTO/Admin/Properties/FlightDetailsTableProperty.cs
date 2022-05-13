using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.Admin.Properties
{
   public class FlightDetailsTableProperty
    {
        public string AirLineName { get; set; }
        public string FlightID { get; set; }

        public string AirLineCode { get; set; }
        public string AirplanType { get; set; }
        public decimal BusinessFare { get; set; }
        public decimal EconnmyFare { get; set; }
        public int MaxSeat { get; set; }
        public int Status { get; set; }
    }
}
