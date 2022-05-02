using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.Admin.Response
{
  public  class AirLineRegisterResponse
    {
        public bool isDataAvailable { get; set; }
        public AirLineRegisterResponse()
        {
            isDataAvailable = false;
        }
        public int ErrorStatus { get; set; }
        public string message { get; set; }

    }
}

