using DTO.User.Property;
using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.User.Response
{
   public class GetTicketDetailsResponse
    {

        public bool isDataAvailable { get; set; }
        public GetTicketDetailsResponse()
        {
            isDataAvailable = false;
        }
        public int ErrorStatus { get; set; }
        public string message { get; set; }
        public List<GetTicketBookedDtProperties> getTicketBookedDtPropertiesList { get; set; }
    }
}
