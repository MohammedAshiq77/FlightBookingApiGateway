using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.Admin.Request
{
   public class GetAirlineDetailsRequest
    {
        public string FromPlace { get; set; }

        public string ToPlace { get; set; }

        public DateTime StartDate { get; set; }
    }
}
