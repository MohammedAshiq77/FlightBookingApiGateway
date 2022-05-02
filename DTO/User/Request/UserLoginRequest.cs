using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.User.Request
{
   public class UserLoginRequest
    {
        public string EmailId { get; set; }

        public string PassWord { get; set; }
    }
}
