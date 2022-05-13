using DTO.Admin.Properties;
using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.Admin.Response
{
   public class PostCouponCodeResponse
    {
        public bool isDataAvailable { get; set; }
        public PostCouponCodeResponse()
        {
            isDataAvailable = false;
        }
        public int ErrorStatus { get; set; }
        public string message { get; set; }
    }

    public class GetcodeResponse
    {
        public bool isDataAvailable { get; set; }
        public GetcodeResponse()
        {
            isDataAvailable = false;
        }
        public int ErrorStatus { get; set; }
        public string message { get; set; }

        public List<CouponCodeDtlsProperties> couponCodeDtlsPropertiesList { get; set; }
    }
}
