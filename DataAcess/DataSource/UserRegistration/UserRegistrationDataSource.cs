
using DTO.User.Response;
using DTO.User.Request;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using DTO.Response;
using DTO.Admin.Request;
using DTO.Admin.Response;
using DTO.Admin.Properties;
using DTO.User.Property;
using Helper;

namespace DataAcess.DataSource.UserRegistration
{
    public class UserRegistrationDataSource
    {
        Connection con = new Connection();
        public UserRegistrationResponse Registeruser(UserRegistrationRequest userRegistrationRequest)
        {
            UserRegistrationResponse userRegResponse = new UserRegistrationResponse();

            int flag = 0;
            try
            {
                Connection con = new Connection();
                 
                    SqlCommand sql_cmnd = new SqlCommand();
                
                sql_cmnd= Connection.con.CreateCommand();
                    sql_cmnd.CommandText = "sp_userRegistration";
                    sql_cmnd.CommandType = CommandType.StoredProcedure;
                    sql_cmnd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = userRegistrationRequest.Name;
                    sql_cmnd.Parameters.Add("@EmailID", SqlDbType.NVarChar).Value = userRegistrationRequest.EmailId;
                    sql_cmnd.Parameters.Add("@UserName", SqlDbType.NVarChar).Value = userRegistrationRequest.UserName;
                    sql_cmnd.Parameters.Add("@Password", SqlDbType.NVarChar).Value = userRegistrationRequest.PassWord;
                    sql_cmnd.Parameters.Add("@Type", SqlDbType.Int).Value = userRegistrationRequest.Type;
                    sql_cmnd.Parameters.Add("@Message", SqlDbType.NVarChar,32000).Direction = ParameterDirection.Output;
                    sql_cmnd.ExecuteNonQuery();
                    string result=(string)sql_cmnd.Parameters["@Message"].Value;
                   string[] ab= result.Split("^");
                    if(ab[0]=="0")
                    {
                        userRegResponse.message = "Successfully Registered";
                        userRegResponse.ErrorStatus = 0;
                        userRegResponse.isDataAvailable = true;
                    }
                    else
                    {
                        userRegResponse.message = "Contact IT";
                        userRegResponse.ErrorStatus = 1;
                        userRegResponse.isDataAvailable = false;
                    }


                //}
            }
            catch (Exception ex)
            {
              
            }
            return userRegResponse;
        }

        public UserLoginResponse Login(UserLoginRequest userLoginRequest)
        {
            UserLoginResponse userLoginResponse = new UserLoginResponse();
            try
            {
                jwtTokenManager jwtToken = new jwtTokenManager();
                string token = string.Empty;
                token = jwtToken.CreateToken(userLoginRequest.EmailId, userLoginRequest.PassWord);
                if (!string.IsNullOrEmpty(token))
                {
                    try
                    {
                        UserLogDataProperties userLogDataProperties = new UserLogDataProperties();
                        Connection con = new Connection();
                        SqlCommand sql_cmnd = new SqlCommand();
                        sql_cmnd = Connection.con.CreateCommand();
                        string strqry = "SELECT count(*) FROM UserRegiatrationMaster2 where  EmailId = '" + userLoginRequest.EmailId + "' and Password = '" + userLoginRequest.PassWord + "'";
                        sql_cmnd.CommandText = strqry;
                        sql_cmnd.CommandType = CommandType.Text;
                        int j = (int)sql_cmnd.ExecuteScalar();
                        if (j > 0)
                        {
                            string strQuery = "SELECT type FROM UserRegiatrationMaster2 where  EmailId = '" + userLoginRequest.EmailId + "' and Password = '" + userLoginRequest.PassWord + "'";
                            sql_cmnd.CommandText = strQuery;
                            sql_cmnd.CommandType = CommandType.Text;
                            int i = (int)sql_cmnd.ExecuteScalar();
                            if (i == 0)
                            {
                                string strQuery1 = "select EmailID,UserId,Username from UserRegiatrationMaster2 where EmailId = '" + userLoginRequest.EmailId + "' and Password = '" + userLoginRequest.PassWord + "'";
                                sql_cmnd = Connection.con.CreateCommand();
                                var searchList = new List<UserLogDataProperties>();

                                sql_cmnd.CommandText = strQuery1;
                                sql_cmnd.CommandType = CommandType.Text;
                                SqlDataReader reader = sql_cmnd.ExecuteReader();

                                if (reader.HasRows)
                                {
                                    while (reader.Read())
                                    {

                                        userLogDataProperties.UserEmail = (string)reader[0];
                                        userLogDataProperties.UserId = (int)reader[1];
                                        userLogDataProperties.UserName = (string)reader[2];


                                        searchList.Add(userLogDataProperties);

                                    }
                                }
                                userLoginResponse.message = "Successfully Login as User";
                                userLoginResponse.ErrorStatus = 0;
                                userLoginResponse.isDataAvailable = true;
                                userLoginResponse.UserId = userLogDataProperties.UserId;
                                userLoginResponse.UserName = userLogDataProperties.UserName;
                                userLoginResponse.UserEmail = userLogDataProperties.UserEmail;
                                userLoginResponse.Token = token;
                            }
                            else
                            {

                                userLoginResponse.message = "Successfully Login as Admin";
                                userLoginResponse.ErrorStatus = 0;
                                userLoginResponse.isDataAvailable = true;
                                userLoginResponse.Token = token;
                            }
                        }
                        else
                        {
                            //userLoginResponse.message = "Contact IT";
                            userLoginResponse.ErrorStatus = 1;
                            userLoginResponse.isDataAvailable = false;
                            userLoginResponse.message = "Invalid user";
                        }                    
                    }
                    catch (Exception ex)
                    {
                        // Log exception
                        //Display Error message
                    }
                }
                else
                {
                    userLoginResponse.ErrorStatus = 1;
                    userLoginResponse.isDataAvailable = false;
                    userLoginResponse.message = "Failed to generate token";
                }
            }
            catch(Exception ex)
            {

            }

            return userLoginResponse;
        }

        public AirLineSearchResponse SearchFlight(AirlineSearchRequest airlineSearchRequest)
        {
            AirLineSearchResponse airLineSearchResponse = new AirLineSearchResponse();

            
            try
            {
                 
                var searchList = new List<AirLineSearchProperties>();

                   Connection con = new Connection();
                SqlCommand sql_cmnd = new SqlCommand();

                 string strQuery = "select a.AirLineName,i.AirLineCode,i.FlightID,i.AirplanType,i.TotalBusSeats,i.TotalEcoSeats,i.TicketBusFare,i.TicketEcoFare,i.Rows from InventoryMastertb as i,FlightMaster as j ,AirLineMaster as a  where j.FlightID = i.FlightID and j.AirLineCode = a.AirLineCode and j.Status = 0 and i.FromPlace = '" + airlineSearchRequest.From + "' and i.ToPlace = '"+airlineSearchRequest.To +"';";
                //string strQuery = "select a.AirLineName,i.AirLineCode,i.FlightID,i.AirplanType,i.TotalBusSeats,i.TotalEcoSeats,i.TicketBusFare,i.TicketEcoFare,i.Rows,i.DepartureTime from InventoryMastertb as i,FlightMaster as j ,AirLineMaster as a  where j.FlightID = i.FlightID and j.AirLineCode = a.AirLineCode and j.Status = 0 and i.FromPlace = '" + airlineSearchRequest.From + "' and i.ToPlace = '" + airlineSearchRequest.To + "';";


                sql_cmnd = Connection.con.CreateCommand();

                sql_cmnd.CommandText = strQuery;
                sql_cmnd.CommandType = CommandType.Text;
                SqlDataReader reader = sql_cmnd.ExecuteReader();

               

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            AirLineSearchProperties airLineSearchProperties = new AirLineSearchProperties();
                            airLineSearchProperties.AirLineName = (string)reader[0];
                            airLineSearchProperties.AirLine = (string)reader[1];
                            airLineSearchProperties.FlightNumber = (string)reader[2];
                            airLineSearchProperties.AirLineType = (string)reader[3];
                            airLineSearchProperties.TotalBusinessSeats = (int)reader[4];
                            airLineSearchProperties.TotalNonBusinessSeats = (int)reader[5];
                            airLineSearchProperties.BusinessTicketCost = (decimal)reader[6];
                            airLineSearchProperties.NonBusinessTicketCost = (decimal)reader[7];
                            airLineSearchProperties.FlightSeatRow = (int)reader[8];
                        //airLineSearchProperties.FlightStartDateTime = (DateTime)reader[9];

                        searchList.Add(airLineSearchProperties);

                        }

                       
                    }
                    if (searchList.Count > 0)
                    {
                        airLineSearchResponse.airlinInventoryProperties = searchList;
                       
                        airLineSearchResponse.isDataAvailable = true;
                        airLineSearchResponse.message = "Sucess";
                    }

                    else
                    {
                        airLineSearchResponse.message = "Contact IT";
                       
                        airLineSearchResponse.isDataAvailable = false;

                        airLineSearchResponse.message = "Fail";
                    }

                    reader.Close();


               // }
            }
            catch (Exception ex)
            {
                // Log exception
                //Display Error message
            }


            return airLineSearchResponse;
        }

        public AirlineTicketBookingResponse AirTicketBooking(AirLineTicketBookingRequest airLineTicketBookingRequest)
        {
            AirlineTicketBookingResponse airlineTicketBookingResponse = new AirlineTicketBookingResponse();
            UserDetailsProperty userDetailsProperty = new UserDetailsProperty();
            TicketDetailsProperty ticketDetailsProperty = new TicketDetailsProperty();

            var TickDtl = new List<TicketDetailsProperty>();
            List<UserDetailsProperty> UsrDtl = new List<UserDetailsProperty>();



            int flag = 0;
            try
            {
                
                Connection con = new Connection();
                SqlCommand sql_cmnd = new SqlCommand();


                sql_cmnd = Connection.con.CreateCommand();
               
                    
                    sql_cmnd.CommandText = "sp_FlightBooking";
                    sql_cmnd.CommandType = CommandType.StoredProcedure;


                    

                    sql_cmnd.Parameters.Add("@BookingId", SqlDbType.NVarChar).Value = airLineTicketBookingRequest.BookingId;
                    sql_cmnd.Parameters.Add("@UserId", SqlDbType.Int).Value = airLineTicketBookingRequest.UserId;
                    sql_cmnd.Parameters.Add("@FlightId", SqlDbType.NVarChar).Value = airLineTicketBookingRequest.FlightId;
                    sql_cmnd.Parameters.Add("@Journey", SqlDbType.Int).Value = airLineTicketBookingRequest.Journey;
                    sql_cmnd.Parameters.Add("@OneWayCost", SqlDbType.Decimal).Value = airLineTicketBookingRequest.OneWayCost;
                    sql_cmnd.Parameters.Add("@TwoWayCost", SqlDbType.Decimal).Value = airLineTicketBookingRequest.TwoWayCost;
                    sql_cmnd.Parameters.Add("@TotalBookSeats", SqlDbType.Int).Value = airLineTicketBookingRequest.TotalBookSeats;
                    sql_cmnd.Parameters.Add("@PNR", SqlDbType.NVarChar).Value = airLineTicketBookingRequest.Pnr;
                    sql_cmnd.Parameters.Add("@BookedDate", SqlDbType.NVarChar).Value = airLineTicketBookingRequest.BookedDate;
                    sql_cmnd.Parameters.Add("@DiscountPrice", SqlDbType.NVarChar).Value = airLineTicketBookingRequest.DiscountPrice;
                    sql_cmnd.Parameters.Add("@DiscountCoupon", SqlDbType.NVarChar).Value = airLineTicketBookingRequest.CouponCode;


                    sql_cmnd.Parameters.Add("@UserName", SqlDbType.NVarChar).Value = airLineTicketBookingRequest.UserName;
                    sql_cmnd.Parameters.Add("@UserEmail", SqlDbType.NVarChar).Value = airLineTicketBookingRequest.UserEmail;
                    sql_cmnd.Parameters.Add("@Gender", SqlDbType.Int).Value = airLineTicketBookingRequest.Gender;
                    sql_cmnd.Parameters.Add("@age", SqlDbType.Int).Value = airLineTicketBookingRequest.Age;
                    sql_cmnd.Parameters.Add("@Meal", SqlDbType.Int).Value = airLineTicketBookingRequest.Meal;
                    sql_cmnd.Parameters.Add("@SeatNo", SqlDbType.Int).Value = airLineTicketBookingRequest.SeatNumber;
                    sql_cmnd.Parameters.Add("@Message", SqlDbType.NVarChar, 32000).Direction = ParameterDirection.Output;


                   // sqlCon.Open();
                    sql_cmnd.ExecuteNonQuery();
                   // sqlCon.Close();
                    string result = (string)sql_cmnd.Parameters["@Message"].Value;
                    string[] ab = result.Split("^");
                if (ab[0] == "0")
                {
                    airlineTicketBookingResponse.message = "Sucessfully PnrId Generated  '" + ab[2] + "'";
                    airlineTicketBookingResponse.ErrorStatus = 0;
                    airlineTicketBookingResponse.isDataAvailable = true;
                }

                else
                {
                    airlineTicketBookingResponse.message = "Contact IT";
                    airlineTicketBookingResponse.ErrorStatus = 1;
                    airlineTicketBookingResponse.isDataAvailable = false;

                }


                
            }
            catch (Exception ex)
            {
                // Log exception
                //Display Error message
            }
            return airlineTicketBookingResponse;
        }

        public GetTicketDetailsResponse GetBookedTicketDetails(GetBookedTicketDTRequest getBookedTicketDTRequest)
        {
            GetTicketDetailsResponse getTicketDetailsResponse = new GetTicketDetailsResponse();
            

            try
            {
               
                var searchList = new List<GetTicketBookedDtProperties>();
                Connection con = new Connection();
                SqlCommand sql_cmnd = new SqlCommand();
                string strQuery = "select i.BookingId,i.FlightId,i.UserId,i.Journey,i.DiscountPrice,i.TotalBookSeats,k.FromPlace,k.ToPlace,p.UserName,p.Gender,p.UserEmail,p.Age,p.Meal  from FlightBookingDT as i, InventoryMastertb as k,PassengerDT as p where i.FlightId = k.FlightID and i.BookingId =p.UserBookingId and i.Flag=0 and i.PNR ='" + getBookedTicketDTRequest.PNR + "'";
                sql_cmnd = Connection.con.CreateCommand();
                sql_cmnd.CommandText = strQuery;
                sql_cmnd.CommandType = CommandType.Text;
                SqlDataReader reader = sql_cmnd.ExecuteReader();

                if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            GetTicketBookedDtProperties getTicketBookedDtProperties = new GetTicketBookedDtProperties();

                            getTicketBookedDtProperties.BookingId = (string)reader[0];
                            getTicketBookedDtProperties.FlightId = (string)reader[1];
                            getTicketBookedDtProperties.UserId = (int)reader[2];
                            getTicketBookedDtProperties.Journey = (int)reader[3];
                            getTicketBookedDtProperties.TicketCost = (Decimal)reader[4];
                            getTicketBookedDtProperties.TotalBookSeats = (int)reader[5];
                            getTicketBookedDtProperties.FromPlace = (string)reader[6];
                            getTicketBookedDtProperties.ToPlace = (string)reader[7];
                            getTicketBookedDtProperties.Name = (string)reader[8];
                            getTicketBookedDtProperties.Gender = (int)reader[9];
                            getTicketBookedDtProperties.Email = (string)reader[10];
                            getTicketBookedDtProperties.Age = (int)reader[11];
                            getTicketBookedDtProperties.Meal = (int)reader[12];
                           

                            searchList.Add(getTicketBookedDtProperties);

                        }


                    }
                    if (searchList.Count > 0)
                    {
                        getTicketDetailsResponse.getTicketBookedDtPropertiesList = searchList;

                        getTicketDetailsResponse.isDataAvailable = true;
                        getTicketDetailsResponse.message = "Sucess";
                    }

                    else
                    {
                        getTicketDetailsResponse.message = "Contact IT";

                        getTicketDetailsResponse.isDataAvailable = false;

                        getTicketDetailsResponse.message = "Fail";
                    }

                    reader.Close();


               
            }
            catch (Exception ex)
            {
                // Log exception
                //Display Error message
            }


            return getTicketDetailsResponse;
        }


        public GetTicketDetailsResponse GetBookedTicketDetailsHIs(GetBookedTicketDTHisRequest getBookedTicketDTHis)
        {
            GetTicketDetailsResponse getTicketDetailsResponse = new GetTicketDetailsResponse();
           

            try
            {
                 //SqlConnection sqlCon = null;
                var searchList = new List<GetTicketBookedDtProperties>();

                       Connection con = new Connection();
                SqlCommand sql_cmnd = new SqlCommand();

                    string strQuery = "select i.BookingId,i.FlightId,i.UserId,i.Journey,i.DiscountPrice,i.TotalBookSeats,k.FromPlace,k.ToPlace,p.UserName,p.Gender,p.UserEmail,p.Age,p.Meal  from FlightBookingDT as i, InventoryMastertb as k,PassengerDT as p where i.FlightId = k.FlightID and i.BookingId =p.UserBookingId  and  p.UserEmail ='" + getBookedTicketDTHis.EmailId + "';";
                sql_cmnd = Connection.con.CreateCommand();
                sql_cmnd.CommandText = strQuery;
                sql_cmnd.CommandType = CommandType.Text;
                SqlDataReader reader = sql_cmnd.ExecuteReader();

                if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            GetTicketBookedDtProperties getTicketBookedDtProperties = new GetTicketBookedDtProperties();

                            getTicketBookedDtProperties.BookingId = (string)reader[0];
                            getTicketBookedDtProperties.FlightId = (string)reader[1];
                            getTicketBookedDtProperties.UserId = (int)reader[2];
                            getTicketBookedDtProperties.Journey = (int)reader[3];
                            getTicketBookedDtProperties.TicketCost = (Decimal)reader[4];
                            getTicketBookedDtProperties.TotalBookSeats = (int)reader[5];
                            getTicketBookedDtProperties.FromPlace = (string)reader[6];
                            getTicketBookedDtProperties.ToPlace = (string)reader[7];
                            getTicketBookedDtProperties.Name = (string)reader[8];
                            getTicketBookedDtProperties.Gender = (int)reader[9];
                            getTicketBookedDtProperties.Email = (string)reader[10];
                            getTicketBookedDtProperties.Age = (int)reader[11];
                            getTicketBookedDtProperties.Meal = (int)reader[12];

                            searchList.Add(getTicketBookedDtProperties);

                        }


                    }
                    if (searchList.Count > 0)
                    {
                        getTicketDetailsResponse.getTicketBookedDtPropertiesList = searchList;

                        getTicketDetailsResponse.isDataAvailable = true;
                        getTicketDetailsResponse.message = "Sucess";
                    }

                    else
                    {
                        getTicketDetailsResponse.message = "Contact IT";

                        getTicketDetailsResponse.isDataAvailable = false;

                        getTicketDetailsResponse.message = "Fail";
                    }

                    reader.Close();


                
            }
            catch (Exception ex)
            {
                // Log exception
                //Display Error message
            }


            return getTicketDetailsResponse;
        }

        public AirlineTicketBookingCancelResponse CancelBookedTicket(GetBookedTicketDTRequest getBookedTicketDTRequest)
        {
            AirlineTicketBookingCancelResponse airlineTicketBookingCancelResponse = new AirlineTicketBookingCancelResponse();
            try
            {



                Connection con = new Connection();
                SqlCommand sql_cmnd = new SqlCommand();
               string strQuery = "update FlightBookingDT set Flag= 1 where pnr ='" + getBookedTicketDTRequest.PNR + "'; ";
                sql_cmnd = Connection.con.CreateCommand();
                sql_cmnd.CommandText = strQuery;
                sql_cmnd.CommandType = CommandType.Text;
                int i = sql_cmnd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        airlineTicketBookingCancelResponse.message = "Ticket Cancelled Successfully";
                        airlineTicketBookingCancelResponse.ErrorStatus = 0;
                        airlineTicketBookingCancelResponse.isDataAvailable = true;
                    }

                    else
                    {
                        airlineTicketBookingCancelResponse.message = "Contact IT";
                        airlineTicketBookingCancelResponse.ErrorStatus = 1;
                        airlineTicketBookingCancelResponse.isDataAvailable = false;
                    }


               // }
            }
            catch (Exception ex)
            {
                // Log exception
                //Display Error message
            }


            return airlineTicketBookingCancelResponse;
        }




    }

}
