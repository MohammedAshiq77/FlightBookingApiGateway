using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.Admin.Request
{
  public  class AirLineRegisterRequest
    {
        public string AirlineName { get; set; }

        // public string AirlineCode { get; set; }

        //public string FightId { get; set; }

        public string AirplanType { get; set; }
 
        public decimal BusinessFare { get; set; }

        public decimal EconnmyFare { get; set; }

        public int MaxSeat { get; set; }

        public DateTime StartTime { get; set; }
        public int Status { get; set; } //0 active & 1 Nonactive

      
    }
    
}
