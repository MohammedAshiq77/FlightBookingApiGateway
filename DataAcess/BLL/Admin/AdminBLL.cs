using DataAcess.DataSource.Admin;
using DTO.Admin.Request;
using DTO.Admin.Response;
using DTO.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAcess.BLL.Admin
{
 public   class AdminBLL
    {
        public readonly static Lazy<AdminBLL> m_instance;
        public static AdminBLL Instance
        {
            get
            {
                return AdminBLL.m_instance.Value;
            }
        }
        static AdminBLL()
        {
            AdminBLL.m_instance = new Lazy<AdminBLL>(() => new AdminBLL());
        }

        public Response<AirLineRegisterResponse> RegisterAirline(AirLineRegisterRequest airLineRegisterRequest)
        {
            AirLineRegisterResponse airLineRegisterResponse = new AirLineRegisterResponse();
            Response<AirLineRegisterResponse> response = new Response<AirLineRegisterResponse>();
            try
            {
                airLineRegisterResponse = new AdminDataSource().RegisterAirline(airLineRegisterRequest);
                if (airLineRegisterResponse.ErrorStatus == 0)
                {
                    response.Data = airLineRegisterResponse;
                    response.status = ResponseTypeContants.SUCCESS;
                    response.apiStatus = ApiStatusConstants.COMPLETED;
                    response.responseMsg = ResponseTypeContants.SUCCESS;
                }
                else
                {
                    response.apiStatus = ApiStatusConstants.COMPLETED;
                    response.responseMsg = "No Data Found";
                }
            }
            catch (Exception ex)
            {
                Exception exception = ex;
                response.status = "exception";
                response.responseMsg = "internal server error";
                response.SetExceptionError(ex.Message);
            }
            return response;
        }

        public Response<AirLineInventoryResponse> AddAirLineInventory(AirLineInventoryRequest airLineInventoryRequest)
        {
            AirLineInventoryResponse airLineInventoryResponse = new AirLineInventoryResponse();
            Response<AirLineInventoryResponse> response = new Response<AirLineInventoryResponse>();
            try
            {
                airLineInventoryResponse = new AdminDataSource().AddAirlineInventory(airLineInventoryRequest);
                if (airLineInventoryResponse.ErrorStatus == 0)
                {
                    response.Data = airLineInventoryResponse;
                    response.status = ResponseTypeContants.SUCCESS;
                    response.apiStatus = ApiStatusConstants.COMPLETED;
                    response.responseMsg = ResponseTypeContants.SUCCESS;
                }
                else
                {
                    response.apiStatus = ApiStatusConstants.COMPLETED;
                    response.responseMsg = "No Data Found";
                }
            }
            catch (Exception ex)
            {
                Exception exception = ex;
                response.status = "exception";
                response.responseMsg = "internal server error";
                response.SetExceptionError(ex.Message);
            }
            return response;
        }

        public Response<FlightTableDetailsResponse> GetFlidetails( )
        {
            FlightTableDetailsResponse flightTableDetailsResponse = new FlightTableDetailsResponse();
            Response<FlightTableDetailsResponse> response = new Response<FlightTableDetailsResponse>();
            try
            {
                flightTableDetailsResponse = new AdminDataSource().FlightDetailsdt();
                if (flightTableDetailsResponse.isDataAvailable == true)
                {
                    response.Data = flightTableDetailsResponse;
                    response.status = ResponseTypeContants.SUCCESS;
                    response.apiStatus = ApiStatusConstants.COMPLETED;
                    response.responseMsg = ResponseTypeContants.SUCCESS;
                }
                else
                {
                    response.apiStatus = ApiStatusConstants.COMPLETED;
                    response.responseMsg = "No Data Found";
                }
            }
            catch (Exception ex)
            {
                Exception exception = ex;
                response.status = "exception";
                response.responseMsg = "internal server error";
                response.SetExceptionError(ex.Message);
            }
            return response;
        }

        public Response<AirlineBlockAndUnblockResponse> BlockAndUnblockAirLine(AirlineBlockAndUnblockRequest airlineBlockAndUnblockRequest)
        {
            AirlineBlockAndUnblockResponse airlineBlockAndUnblockResponse = new AirlineBlockAndUnblockResponse();
            Response<AirlineBlockAndUnblockResponse> response = new Response<AirlineBlockAndUnblockResponse>();
            try
            {
                airlineBlockAndUnblockResponse = new AdminDataSource().BlockAndUnblockAirLine(airlineBlockAndUnblockRequest);
                if (airlineBlockAndUnblockResponse.isDataAvailable == true)
                {
                    response.Data = airlineBlockAndUnblockResponse;
                    response.status = ResponseTypeContants.SUCCESS;
                    response.apiStatus = ApiStatusConstants.COMPLETED;
                    response.responseMsg = ResponseTypeContants.SUCCESS;
                }
                else
                {
                    response.apiStatus = ApiStatusConstants.COMPLETED;
                    response.responseMsg = "No Data Found";
                }
            }
            catch (Exception ex)
            {
                Exception exception = ex;
                response.status = "exception";
                response.responseMsg = "internal server error";
                response.SetExceptionError(ex.Message);
            }
            return response;
        }


    }
}
