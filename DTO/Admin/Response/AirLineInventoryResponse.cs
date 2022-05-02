using DTO.Admin.Properties;
using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.Admin.Response
{
   public class AirLineInventoryResponse
    {
        public bool isDataAvailable { get; set; }
        public AirLineInventoryResponse()
        {
            isDataAvailable = false;
        }
        public int ErrorStatus { get; set; }
        public string message { get; set; }
           }
    public class GetAirLineDetailsResponse
    {
        public bool isDataAvailable { get; set; }
        public GetAirLineDetailsResponse()
        {
            isDataAvailable = false;
        }
        public int ErrorStatus { get; set; }
        public string message { get; set; }
        public List<AirLineSearchProperties> airlinInventoryProperties { get; set; }

    }
}
