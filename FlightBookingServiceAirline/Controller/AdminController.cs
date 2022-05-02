using DataAcess.BLL.Admin;
using DTO.Admin.Request;
using DTO.Admin.Response;
using DTO.Response;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightBookingServiceAirline.Controller
{
    [Produces("application/json")]
    [Route("api/AdminController")]
    public class AdminController : ControllerBase
    {
        [HttpPost("AirLineRegister")]
        public ActionResult<Response<AirLineRegisterResponse>> AirLineRegister([FromBody] AirLineRegisterRequest airLineRegisterRequest)
        {
            if (ModelState.IsValid)
            {
                return AdminBLL.Instance.RegisterAirline(airLineRegisterRequest);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPost("AddAirLineInventory")]
        public ActionResult<Response<AirLineInventoryResponse>> AddAirLineInventory([FromBody] AirLineInventoryRequest airLineInventoryRequest)
        {
            if (ModelState.IsValid)
            {
                return AdminBLL.Instance.AddAirLineInventory(airLineInventoryRequest);
            }
            else
            {
                return BadRequest(ModelState);
            }

        }

        [HttpGet("GetFlightDetails")]
        public ActionResult<Response<FlightTableDetailsResponse>> AddAirLineInventory( )
        {
            if (ModelState.IsValid)
            {
                return AdminBLL.Instance.GetFlidetails();
            }
            else
            {
                return BadRequest(ModelState);
            }


        }

        [HttpPost("BlockAndUnblockAirline")]
        public ActionResult<Response<AirlineBlockAndUnblockResponse>> BlockAndUnblockAirline([FromBody] AirlineBlockAndUnblockRequest airlineBlockAndUnblockRequest)
        {
            if (ModelState.IsValid)
            {
                return AdminBLL.Instance.BlockAndUnblockAirLine(airlineBlockAndUnblockRequest);
            }
            else
            {
                return BadRequest(ModelState);
            }


        }
    }
}
