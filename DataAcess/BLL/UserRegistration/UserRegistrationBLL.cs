using DataAcess.DataSource.UserRegistration;
using DTO.Response;

using DTO.User.Response;
using DTO.User.Request;
using System;
using System.Collections.Generic;
using System.Text;
using DTO.Admin.Response;
using DTO.Admin.Request;

namespace DataAcess.BLL.UserRegistration
{
    public class UserRegistration
    {
        public readonly static Lazy<UserRegistration> m_instance;
        public static UserRegistration Instance
        {
            get
            {
                return UserRegistration.m_instance.Value;
            }
        }
        static UserRegistration()
        {
            UserRegistration.m_instance = new Lazy<UserRegistration>(() => new UserRegistration());
        }
        public Response<UserRegistrationResponse> RegisterUser (UserRegistrationRequest userRegistrationRequest)
        {
            UserRegistrationResponse userRegistrationResponse = new UserRegistrationResponse();
            Response<UserRegistrationResponse> response = new Response<UserRegistrationResponse>();
            try
            {
                userRegistrationResponse = new UserRegistrationDataSource().Registeruser(userRegistrationRequest);
                if (userRegistrationResponse.ErrorStatus == 0)
                {
                    response.Data = userRegistrationResponse;
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

        public Response<UserLoginResponse> UserLogin(UserLoginRequest userLoginRequest)
        {
            UserLoginResponse userLoginResponse = new UserLoginResponse();
            Response<UserLoginResponse> response = new Response<UserLoginResponse>();
            try
            {
                userLoginResponse = new UserRegistrationDataSource().Login(userLoginRequest);
                if (userLoginResponse.ErrorStatus == 0)
                {
                    response.Data = userLoginResponse;
                    response.status = ResponseTypeContants.SUCCESS;
                    response.apiStatus = ApiStatusConstants.COMPLETED;
                    response.responseMsg = ResponseTypeContants.SUCCESS;
                }
                else
                {
                    response.Data = userLoginResponse;
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
        
        public Response<AirlineTicketBookingResponse> AirLineTicketBooking(AirLineTicketBookingRequest airLineTicketBookingRequest)
        {
            AirlineTicketBookingResponse airlineTicketBookingResponse = new AirlineTicketBookingResponse();
            Response<AirlineTicketBookingResponse> response = new Response<AirlineTicketBookingResponse>();
            try
            {
                airlineTicketBookingResponse = new UserRegistrationDataSource().AirTicketBooking(airLineTicketBookingRequest);
                if (airlineTicketBookingResponse.ErrorStatus == 0)
                {
                    response.Data = airlineTicketBookingResponse;
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

        public Response<AirLineSearchResponse> SearchAirLine(AirlineSearchRequest airlineSearchRequest)
        {
            AirLineSearchResponse airLineSearchResponse = new AirLineSearchResponse();
            Response<AirLineSearchResponse> response = new Response<AirLineSearchResponse>();
            try
            {
                airLineSearchResponse = new UserRegistrationDataSource().SearchFlight(airlineSearchRequest);
                if (airLineSearchResponse.isDataAvailable == true)
                {
                    response.Data = airLineSearchResponse;
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

        public Response<GetTicketDetailsResponse> GetTicketDt(GetBookedTicketDTRequest getBookedTicketDTRequest)
        {
            GetTicketDetailsResponse getTicketDetailsResponse = new GetTicketDetailsResponse();
            Response<GetTicketDetailsResponse> response = new Response<GetTicketDetailsResponse>();
            try
            {
                getTicketDetailsResponse = new UserRegistrationDataSource().GetBookedTicketDetails(getBookedTicketDTRequest);
                if (getTicketDetailsResponse.isDataAvailable == true)
                {
                    response.Data = getTicketDetailsResponse;
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

        public Response<GetTicketDetailsResponse> GetTicketDThistory(GetBookedTicketDTHisRequest getBookedTicketDTHisRequest)
        {
            GetTicketDetailsResponse getTicketDetailsResponse = new GetTicketDetailsResponse();
            Response<GetTicketDetailsResponse> response = new Response<GetTicketDetailsResponse>();
            try
            {
                getTicketDetailsResponse = new UserRegistrationDataSource().GetBookedTicketDetailsHIs(getBookedTicketDTHisRequest);
                if (getTicketDetailsResponse.isDataAvailable == true)
                {
                    response.Data = getTicketDetailsResponse;
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

        public Response<AirlineTicketBookingCancelResponse> CancelBookedTicket(GetBookedTicketDTRequest getBookedTicketDTRequest)
        {
            AirlineTicketBookingCancelResponse airlineTicketBookingCancelResponse = new AirlineTicketBookingCancelResponse();
            Response<AirlineTicketBookingCancelResponse> response = new Response<AirlineTicketBookingCancelResponse>();
            try
            {
                airlineTicketBookingCancelResponse = new UserRegistrationDataSource().CancelBookedTicket(getBookedTicketDTRequest);
                if (airlineTicketBookingCancelResponse.isDataAvailable == true)
                {
                    response.Data = airlineTicketBookingCancelResponse;
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
