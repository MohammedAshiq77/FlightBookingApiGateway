using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.User.Request
{
  public  class UserRegistrationRequest
    {
        
        public string Name { get; set; }

        public string EmailId { get; set; }

        public string UserName { get; set; }

        public string PassWord { get; set; }

        public string ConfirmPassword { get; set; }
        public int Type { get; set; }
    }
}
