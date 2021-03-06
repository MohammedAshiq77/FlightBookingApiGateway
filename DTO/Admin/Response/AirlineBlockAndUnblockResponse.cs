using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.Admin.Response
{
  public  class AirlineBlockAndUnblockResponse
    {
        public bool isDataAvailable { get; set; }
        public AirlineBlockAndUnblockResponse()
        {
            isDataAvailable = false;
        }
        public string message { get; set; }
        public int ErrorStatus { get; set; }
    }

    public class CouponActiveandDeactiveResponse
    {
        public bool isDataAvailable { get; set; }
        public CouponActiveandDeactiveResponse()
        {
            isDataAvailable = false;
        }
        public string message { get; set; }
        public int ErrorStatus { get; set; }
    }
}
