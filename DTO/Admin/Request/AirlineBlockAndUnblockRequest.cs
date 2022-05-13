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

    public class AirlineCouponCodeRequest
    {
        public int CouponId { get; set; }

        public string CouponCode { get; set; }

        public int Couponvalue { get; set; }

        public int Status { get; set; }
        public DateTime CouponValidty { get; set; }
    }

    public class CouponCodeActivateAndDeactivateRequest
    {
        public int CouponId { get; set; }
        public int Status { get; set; }
      
    }
}
