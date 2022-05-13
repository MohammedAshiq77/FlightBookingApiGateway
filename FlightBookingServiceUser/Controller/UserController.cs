using DataAcess.BLL.UserRegistration;
using DTO.Response;
using DTO.User.Request;
using DTO.User;
using DTO.User.Response;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DTO.Admin.Request;
using DTO.Admin.Response;
using Microsoft.Extensions.Configuration;

namespace FlightBookingServiceUser.Controller
{

    [Produces("application/json")]
    [Route("api/UserController")]
    public class UserController : ControllerBase

    {
        //checking
        public readonly IConfiguration _configuration; 
        public UserController(IConfiguration configuration) { _configuration = configuration; }
        [HttpPost("UserRegistrations")]
        public ActionResult<Response<UserRegistrationResponse>> UserRegistrations([FromBody] UserRegistrationRequest request)
        {
            if (ModelState.IsValid)
            {
                return UserRegistration.Instance.RegisterUser(request);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }


        [HttpPost("Login")]
        public ActionResult<Response<UserLoginResponse>> Login([FromBody] UserLoginRequest userLoginRequest)
        {
            if (ModelState.IsValid)
            {
                return UserRegistration.Instance.UserLogin(userLoginRequest);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPost("FlightSearch")]
        public ActionResult<Response<AirLineSearchResponse>> FlightSearch([FromBody] AirlineSearchRequest airlineSearchRequest)
        {
            if (ModelState.IsValid)
            {
                return UserRegistration.Instance.SearchAirLine(airlineSearchRequest);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPost("AirlineTicketBooking")]
        public ActionResult<Response<AirlineTicketBookingResponse>> AirlineTicketBooking([FromBody] AirLineTicketBookingRequest airLineTicketBookingRequest)
        {
            if (ModelState.IsValid)
            {
                return UserRegistration.Instance.AirLineTicketBooking(airLineTicketBookingRequest);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPost("GetTicketDetails")]
        public ActionResult<Response<GetTicketDetailsResponse>> GetTicketDetails([FromBody] GetBookedTicketDTRequest getBookedTicketDTRequest)
        {
            if (ModelState.IsValid)
            {
                return UserRegistration.Instance.GetTicketDt(getBookedTicketDTRequest);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPost("GetTicketDetailsHis")]
        public ActionResult<Response<GetTicketDetailsResponse>> GetTicketDetailsHis([FromBody] GetBookedTicketDTHisRequest getBookedTicketDTHisRequest)
        {
            if (ModelState.IsValid)
            {
                return UserRegistration.Instance.GetTicketDThistory(getBookedTicketDTHisRequest);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPost("CancelBookedTicket")]
        public ActionResult<Response<AirlineTicketBookingCancelResponse>> CancelBookedTicket([FromBody] GetBookedTicketDTRequest getBookedTicketDTRequest)
        {
            if (ModelState.IsValid)
            {
                return UserRegistration.Instance.CancelBookedTicket(getBookedTicketDTRequest);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        [HttpGet, ActionName("Test")]
        public string Test()
        {
            return "hello API.";
        }

    }
}
