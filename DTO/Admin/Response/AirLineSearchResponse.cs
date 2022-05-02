using DTO.Admin.Properties;
using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.Admin.Response
{
    public class AirLineSearchResponse
    {
        public bool isDataAvailable { get; set; }
        public AirLineSearchResponse()
        {
            isDataAvailable = false;
        }
        public string message { get; set; }
       
        public List<AirLineSearchProperties> airlinInventoryProperties { get; set; }
    }
}
