using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.Admin.Request
{
   public class AirlineSearchRequest
    {
        public string From { get; set; }

        public string To { get; set; }

        public DateTime FlightStartDateTime { get; set; }
    }
}
