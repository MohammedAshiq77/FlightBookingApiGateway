using DTO.Admin.Properties;
using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.Admin.Response
{
   public class FlightTableDetailsResponse
    {
        public bool isDataAvailable { get; set; }
        public FlightTableDetailsResponse()
        {
            isDataAvailable = false;
        }
        public string message { get; set; }

        public List<FlightDetailsTableProperty> FlightDetailsTablelist { get; set; }
    }
}
