using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.User.Property
{
    public class UserDetailsProperty
    {
        public string UserName { get; set; }

        public string UserEmail { get; set; }

        public int Gender { get; set; }

        public int Age { get; set; }
        public int Meal { get; set; }

        public int SeatNumber { get; set; }

        public DateTime CreateDate { get; set; }
        public int Flag { get; set; }
    }
}
