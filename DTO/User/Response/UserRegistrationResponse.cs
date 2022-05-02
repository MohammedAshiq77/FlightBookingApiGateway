using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.User.Response
{
   public class UserRegistrationResponse
    {
        
            public bool isDataAvailable { get; set; }
        public UserRegistrationResponse()
        {
            isDataAvailable = false;
        }
        public int ErrorStatus { get; set; }
        public string message { get; set; }

        }
}
