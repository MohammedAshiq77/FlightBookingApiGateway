
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

namespace DataAcess.DataSource.UserRegistration
{
    public class UserRegistrationDataSource
    {
        public UserRegistrationResponse Registeruser(UserRegistrationRequest userRegistrationRequest)
        {
            UserRegistrationResponse userRegResponse = new UserRegistrationResponse();

            int flag = 0;
            try
            {
                //SqlConnection sqlCon = null;

                string connectionString = "Server=JRDOTNETFSECO-2;Database=AirlineTickecting;Trusted_Connection=False;MultipleActiveResultSets=true;user id=sa;password=pass@word1;";

                using (SqlConnection sqlCon = new SqlConnection(connectionString))
                {
                 
                    SqlCommand sql_cmnd = new SqlCommand();
                    sql_cmnd.Connection = sqlCon;
                    sql_cmnd.CommandText = "sp_userRegistration";
                    sql_cmnd.CommandType = CommandType.StoredProcedure;
                    sql_cmnd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = userRegistrationRequest.Name;
                    sql_cmnd.Parameters.Add("@EmailID", SqlDbType.NVarChar).Value = userRegistrationRequest.EmailId;
                    sql_cmnd.Parameters.Add("@UserName", SqlDbType.NVarChar).Value = userRegistrationRequest.UserName;
                    sql_cmnd.Parameters.Add("@Password", SqlDbType.NVarChar).Value = userRegistrationRequest.PassWord;
                    sql_cmnd.Parameters.Add("@Type", SqlDbType.Int).Value = userRegistrationRequest.Type;
                    sql_cmnd.Parameters.Add("@Message", SqlDbType.NVarChar,32000).Direction = ParameterDirection.Output;
                    sqlCon.Open();
                    sql_cmnd.ExecuteNonQuery();
                    sqlCon.Close();
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


                }
            }
            catch (Exception ex)
            {
               // Log exception
                //Display Error message
            }
            return userRegResponse;
        }

        public UserLoginResponse Login(UserLoginRequest userLoginRequest)
        {
            UserLoginResponse userLoginResponse = new UserLoginResponse();
            try
            {
                //SqlConnection sqlCon = null;

                string connectionString = "Server=JRDOTNETFSECO-2;Database=AirlineTickecting;Trusted_Connection=False;MultipleActiveResultSets=true;user id=sa;password=pass@word1;";

                using (SqlConnection sqlCon = new SqlConnection(connectionString))
                {

                   SqlCommand sql_cmnd = new SqlCommand();
                    SqlCommand sql_cmnd1 = new SqlCommand();
                    sql_cmnd.Connection = sqlCon;
                    sql_cmnd1.Connection = sqlCon;

                    //string strQuery = "INSERT INTO Student(Sid,st_name) VALUES (@id,@name)";

                    string strQuery = "SELECT count(*) FROM UserRegiatrationMaster2 where type = 0 and EmailId = '" + userLoginRequest.EmailId + "' and Password = '" + userLoginRequest.PassWord + "'";
                    string strQuery1 = "SELECT count(*) FROM Admin where type = 1 and status = 1 and username = '" + userLoginRequest.EmailId + "' and Password = '" + userLoginRequest.PassWord + "' ";

                    sql_cmnd = new SqlCommand(strQuery, sqlCon);
                    sql_cmnd1 = new SqlCommand(strQuery1, sqlCon);

                    sqlCon.Open();
                  int i= (int) sql_cmnd.ExecuteScalar();
                  int j = (int)sql_cmnd1.ExecuteScalar();
                    sqlCon.Close();
                  
                    //string[] ab = result.Split("^");
                    if (i>0)
                    {
                        userLoginResponse.message = "Successfully Login as User";
                        userLoginResponse.ErrorStatus = 0;
                        userLoginResponse.isDataAvailable = true;
                    }
                    else if(j>0)
                    {
                        userLoginResponse.message = "Successfully Login as Admin";
                        userLoginResponse.ErrorStatus = 0;
                        userLoginResponse.isDataAvailable = true;

                    }
                    else
                    {
                        userLoginResponse.message = "Contact IT";
                        userLoginResponse.ErrorStatus = 1;
                        userLoginResponse.isDataAvailable = false;
                    }


                }
            }
            catch (Exception ex)
            {
                // Log exception
                //Display Error message
            }


            return userLoginResponse;
        }

        public AirLineSearchResponse SearchFlight(AirlineSearchRequest airlineSearchRequest)
        {
            AirLineSearchResponse airLineSearchResponse = new AirLineSearchResponse();

            
            try
            {
                AirLineSearchProperties airLineSearchProperties = new AirLineSearchProperties();
                //SqlConnection sqlCon = null;
                var searchList = new List<AirLineSearchProperties>();

                string connectionString = "Server=JRDOTNETFSECO-2;Database=AirlineTickecting;Trusted_Connection=False;MultipleActiveResultSets=true;user id=sa;password=pass@word1;";

                using (SqlConnection sqlCon = new SqlConnection(connectionString))
                {

                    //SqlCommand sql_cmnd = new SqlCommand();

                    //sql_cmnd.Connection = sqlCon;


                   
                    string strQuery = "select i.AirLineCode,i.FlightID,i.AirplanType,i.TotalBusSeats,i.TotalEcoSeats,i.TicketBusFare,i.TicketEcoFare,i.Rows from InventoryMastertb as i,FlightMaster as j  where j.FlightID = i.FlightID and j.Status = 0 and i.FromPlace = '" + airlineSearchRequest.From + "' and i.ToPlace = '"+airlineSearchRequest.To +"';";

 
                    SqlCommand command = new SqlCommand(strQuery, sqlCon);
                    sqlCon.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            airLineSearchProperties.AirLine = (string)reader[0];
                            airLineSearchProperties.FlightNumber = (string)reader[1];
                            airLineSearchProperties.AirLineType = (string)reader[2];
                            airLineSearchProperties.TotalBusinessSeats = (int)reader[3];
                            airLineSearchProperties.TotalNonBusinessSeats = (int)reader[4];
                            airLineSearchProperties.BusinessTicketCost = (decimal)reader[5];
                            airLineSearchProperties.NonBusinessTicketCost = (decimal)reader[6];
                            airLineSearchProperties.FlightSeatRow = (int)reader[7];

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


                }
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
            int flag = 0;
            try
            {
                //SqlConnection sqlCon = null;

                string connectionString = "Server=JRDOTNETFSECO-2;Database=AirlineTickecting;Trusted_Connection=False;MultipleActiveResultSets=true;user id=sa;password=pass@word1;";

                using (SqlConnection sqlCon = new SqlConnection(connectionString))
                {

                    SqlCommand sql_cmnd = new SqlCommand();
                    sql_cmnd.Connection = sqlCon;
                    sql_cmnd.CommandText = "sp_FlightBooking";
                    sql_cmnd.CommandType = CommandType.StoredProcedure;
                    sql_cmnd.Parameters.Add("@BookingId", SqlDbType.NVarChar).Value = airLineTicketBookingRequest.BookingId;
                    sql_cmnd.Parameters.Add("@UserId", SqlDbType.Int).Value = airLineTicketBookingRequest.UserId;
                    sql_cmnd.Parameters.Add("@FlightId", SqlDbType.NVarChar).Value = airLineTicketBookingRequest.FlightId;
                    sql_cmnd.Parameters.Add("@Journey", SqlDbType.Int).Value = airLineTicketBookingRequest.Journey;
                    sql_cmnd.Parameters.Add("@OneWayCost", SqlDbType.Decimal).Value = airLineTicketBookingRequest.OneWayCost;
                    sql_cmnd.Parameters.Add("@TwoWayCost", SqlDbType.Decimal).Value = airLineTicketBookingRequest.TwoWayCost;
                    sql_cmnd.Parameters.Add("@TotalBookSeats", SqlDbType.Int).Value = airLineTicketBookingRequest.TotalBookSeats;
                    sql_cmnd.Parameters.Add("@PNR", SqlDbType.NVarChar).Value = airLineTicketBookingRequest;
                    sql_cmnd.Parameters.Add("@BookedDate", SqlDbType.NVarChar).Value = airLineTicketBookingRequest.BookedDate;
                    sql_cmnd.Parameters.Add("@DiscountPrice", SqlDbType.NVarChar).Value = airLineTicketBookingRequest;
                    sql_cmnd.Parameters.Add("@UserName", SqlDbType.NVarChar).Value = userDetailsProperty.UserName;

                    sql_cmnd.Parameters.Add("@Message", SqlDbType.NVarChar, 32000).Direction = ParameterDirection.Output;
                    sqlCon.Open();
                    sql_cmnd.ExecuteNonQuery();
                    sqlCon.Close();
                    string result = (string)sql_cmnd.Parameters["@Message"].Value;
                    string[] ab = result.Split("^");
                    if (ab[0] == "0")
                    {
                        airlineTicketBookingResponse.message = "Successfully Registered";
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
                GetTicketBookedDtProperties getTicketBookedDtProperties = new GetTicketBookedDtProperties();
                //SqlConnection sqlCon = null;
                var searchList = new List<GetTicketBookedDtProperties>();

                string connectionString = "Server=JRDOTNETFSECO-2;Database=AirlineTickecting;Trusted_Connection=False;MultipleActiveResultSets=true;user id=sa;password=pass@word1;";

                using (SqlConnection sqlCon = new SqlConnection(connectionString))
                {

                    //SqlCommand sql_cmnd = new SqlCommand();

                    //sql_cmnd.Connection = sqlCon;



                    string strQuery = "";//"select i.AirLineCode,i.FlightID,i.AirplanType,i.TotalBusSeats,i.TotalEcoSeats,i.TicketBusFare,i.TicketEcoFare,i.Rows from InventoryMastertb as i,FlightMaster as j  where j.FlightID = i.FlightID and j.Status = 0 and i.FromPlace = '" + airlineSearchRequest.From + "' and i.ToPlace = '" + airlineSearchRequest.To + "';";


                    SqlCommand command = new SqlCommand(strQuery, sqlCon);
                    sqlCon.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            getTicketBookedDtProperties.FlightId = (string)reader[0];
                            getTicketBookedDtProperties.BookingId = (string)reader[1];
                            getTicketBookedDtProperties.UserId = (int)reader[2];
                            getTicketBookedDtProperties.Journey = (int)reader[3];
                            getTicketBookedDtProperties.TicketCost = (Decimal)reader[4];
                            getTicketBookedDtProperties.FromPlace = (string)reader[5];
                            getTicketBookedDtProperties.ToPlace = (string)reader[6];
                            getTicketBookedDtProperties.TotalBookSeats = (int)reader[7];
                            getTicketBookedDtProperties.Name = (string)reader[8];
                            getTicketBookedDtProperties.Gender = (int)reader[9];
                            getTicketBookedDtProperties.Email = (string)reader[10];
                            getTicketBookedDtProperties.Meal = (int)reader[11];
                            getTicketBookedDtProperties.Age = (int)reader[12];

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
                GetTicketBookedDtProperties getTicketBookedDtProperties = new GetTicketBookedDtProperties();
                //SqlConnection sqlCon = null;
                var searchList = new List<GetTicketBookedDtProperties>();

                string connectionString = "Server=JRDOTNETFSECO-2;Database=AirlineTickecting;Trusted_Connection=False;MultipleActiveResultSets=true;user id=sa;password=pass@word1;";

                using (SqlConnection sqlCon = new SqlConnection(connectionString))
                {

                    //SqlCommand sql_cmnd = new SqlCommand();

                    //sql_cmnd.Connection = sqlCon;



                    string strQuery = "";// "select i.AirLineCode,i.FlightID,i.AirplanType,i.TotalBusSeats,i.TotalEcoSeats,i.TicketBusFare,i.TicketEcoFare,i.Rows from InventoryMastertb as i,FlightMaster as j  where j.FlightID = i.FlightID and j.Status = 0 and i.FromPlace = '" + airlineSearchRequest.From + "' and i.ToPlace = '" + airlineSearchRequest.To + "';";


                    SqlCommand command = new SqlCommand(strQuery, sqlCon);
                    sqlCon.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            getTicketBookedDtProperties.FlightId = (string)reader[0];
                            getTicketBookedDtProperties.BookingId = (string)reader[1];
                            getTicketBookedDtProperties.UserId = (int)reader[2];
                            getTicketBookedDtProperties.Journey = (int)reader[3];
                            getTicketBookedDtProperties.TicketCost = (Decimal)reader[4];
                            getTicketBookedDtProperties.FromPlace = (string)reader[5];
                            getTicketBookedDtProperties.ToPlace = (string)reader[6];
                            getTicketBookedDtProperties.TotalBookSeats = (int)reader[7];
                            getTicketBookedDtProperties.Name = (string)reader[8];
                            getTicketBookedDtProperties.Gender = (int)reader[9];
                            getTicketBookedDtProperties.Email = (string)reader[10];
                            getTicketBookedDtProperties.Meal = (int)reader[11];
                            getTicketBookedDtProperties.Age = (int)reader[12];

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
            }
            catch (Exception ex)
            {
                // Log exception
                //Display Error message
            }


            return getTicketDetailsResponse;
        }




    }

}
