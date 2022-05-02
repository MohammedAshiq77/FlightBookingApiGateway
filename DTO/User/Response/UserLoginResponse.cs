using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.User.Response
{
   public class UserLoginResponse
    {
        public bool isDataAvailable { get; set; }
        public UserLoginResponse()
        {
            isDataAvailable = false;
        }
        public int ErrorStatus { get; set; }
        public string message { get; set; }
    }
}
