using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.Admin.Request
{
  public  class AirlineBlockAndUnblockRequest
    {
        public string AirlinCode { get; set; }

        public string FlightNumber { get; set; }
        public int status { get; set; }
    }
}
