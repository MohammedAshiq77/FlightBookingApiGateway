using DTO.Admin.Properties;
using DTO.Admin.Request;
using DTO.Admin.Response;
using MiNET.Net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace DataAcess.DataSource.Admin
{
    public class AdminDataSource
    {
        public static Random random = new Random();

        public static string GetRandomPNR()
        {
            int length = 7;
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public AirLineRegisterResponse RegisterAirline(AirLineRegisterRequest airLineRegisterRequest)
        {

            AirLineRegisterResponse airLineRegisterResponse = new AirLineRegisterResponse();
           // AirLineInventoryResponse airLineInvResponse = new AirLineInventoryResponse();
            //AirLineInventoryRequest airLineInvReq = new AirLineInventoryRequest();
            var AirCode = GetRandomPNR();
            var FlyCode = GetRandomPNR();
            int flag = 0;
            try
            {
                //SqlConnection sqlCon = null;

                string connectionString = "Server=JRDOTNETFSECO-2;Database=AirlineTickecting;Trusted_Connection=False;MultipleActiveResultSets=true;user id=sa;password=pass@word1;";

                using (SqlConnection sqlCon = new SqlConnection(connectionString))
                {

                    SqlCommand sql_cmnd = new SqlCommand();
                    sql_cmnd.Connection = sqlCon;
                    sql_cmnd.CommandText = "sp_FlightRegistration";
                    sql_cmnd.CommandType = CommandType.StoredProcedure;
                    sql_cmnd.Parameters.Add("@AirlineName", SqlDbType.NVarChar).Value = airLineRegisterRequest.AirlineName;
                    sql_cmnd.Parameters.Add("@AirlineCode", SqlDbType.NVarChar).Value = AirCode;
                    sql_cmnd.Parameters.Add("@FightId", SqlDbType.NVarChar).Value = FlyCode;
                    sql_cmnd.Parameters.Add("@AirplanType", SqlDbType.NVarChar).Value = airLineRegisterRequest.AirplanType;
                    sql_cmnd.Parameters.Add("@BusinessFare", SqlDbType.Decimal).Value = airLineRegisterRequest.BusinessFare;
                    sql_cmnd.Parameters.Add("@EconnmyFare", SqlDbType.Decimal).Value = airLineRegisterRequest.EconnmyFare;
                    sql_cmnd.Parameters.Add("@MaxSeat", SqlDbType.Int).Value = airLineRegisterRequest.MaxSeat;
                    sql_cmnd.Parameters.Add("@StartTime", SqlDbType.DateTime).Value = airLineRegisterRequest.StartTime;
                    sql_cmnd.Parameters.Add("@Message", SqlDbType.NVarChar, 32000).Direction = ParameterDirection.Output;
                    sqlCon.Open();
                    sql_cmnd.ExecuteNonQuery();
                    sqlCon.Close();
                    string result = (string)sql_cmnd.Parameters["@Message"].Value;
                    string[] ab = result.Split("^");
                    if (ab[0] == "0")
                    {
                        airLineRegisterResponse.message = "Successfully Registered";
                        airLineRegisterResponse.ErrorStatus = 0;
                        airLineRegisterResponse.isDataAvailable = true;
                    }
                    else
                    {
                        airLineRegisterResponse.message = "Contact IT";
                        airLineRegisterResponse.ErrorStatus = 1;
                        airLineRegisterResponse.isDataAvailable = false;
                    }


                }
            }
            catch (Exception ex)
            {
                // Log exception
                //Display Error message
            }
            return airLineRegisterResponse;
        }


        public AirLineInventoryResponse AddAirlineInventory(AirLineInventoryRequest airLineInventoryRequest)
        {
            AirLineInventoryResponse airLineInventoryResponse = new AirLineInventoryResponse();
            try
            {
                //SqlConnection sqlCon = null;

                string connectionString = "Server=JRDOTNETFSECO-2;Database=AirlineTickecting;Trusted_Connection=False;MultipleActiveResultSets=true;user id=sa;password=pass@word1;";

                using (SqlConnection sqlCon = new SqlConnection(connectionString))
                {

                    SqlCommand sql_cmnd = new SqlCommand();
                   
                    sql_cmnd.Connection = sqlCon;
                   

                    
                    string strQuery = "update InventoryMastertb set FromPlace='" + airLineInventoryRequest.From+ "',ToPlace='" + airLineInventoryRequest.To + "'  ,DepartureTime=convert(datetime2 ,'" + airLineInventoryRequest.FlightStartDateTime + "',103)  ,ArrivalTime=convert(datetime2 ,'" + airLineInventoryRequest.FlightToDateTime + "',103),TotalBusSeats='" + airLineInventoryRequest.TotalBusinessSeats + "',TotalEcoSeats='" + airLineInventoryRequest.TotalNonBusinessSeats + "',Rows='" + airLineInventoryRequest.FlightSeatRow + "'    where FlightID='" + airLineInventoryRequest.FlightNumber + "' and  AirLineCode='" + airLineInventoryRequest.AirLineId + "'";
                   
                    sql_cmnd = new SqlCommand(strQuery, sqlCon);
                   
                    sqlCon.Open();
                    int i=sql_cmnd.ExecuteNonQuery();
                     sqlCon.Close();

                       if (i > 0)
                    {
                        airLineInventoryResponse.message = "Successfully Login User";
                        airLineInventoryResponse.ErrorStatus = 0;
                        airLineInventoryResponse.isDataAvailable = true;
                    }
                   
                    else
                    {
                        airLineInventoryResponse.message = "Contact IT";
                        airLineInventoryResponse.ErrorStatus = 1;
                        airLineInventoryResponse.isDataAvailable = false;
                    }


                }
            }
            catch (Exception ex)
            {
                // Log exception
                //Display Error message
            }


            return airLineInventoryResponse;
        }

        public FlightTableDetailsResponse FlightDetailsdt()
        {
            FlightTableDetailsResponse flightTableDetailsResponse = new FlightTableDetailsResponse();


            try
            {
                FlightDetailsTableProperty flightDetailsTableProperty = new FlightDetailsTableProperty();
                //SqlConnection sqlCon = null;
                var searchList = new List<FlightDetailsTableProperty>();

                string connectionString = "Server=JRDOTNETFSECO-2;Database=AirlineTickecting;Trusted_Connection=False;MultipleActiveResultSets=true;user id=sa;password=pass@word1;";

                using (SqlConnection sqlCon = new SqlConnection(connectionString))
                {

                    //SqlCommand sql_cmnd = new SqlCommand();

                    //sql_cmnd.Connection = sqlCon;



                    string strQuery = "select FlightID,AirLineCode,AirplanType,BusinessFare,EconnmyFare,MaxSeat,Status from FlightMaster";

                    SqlCommand command = new SqlCommand(strQuery, sqlCon);
                    sqlCon.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            flightDetailsTableProperty.FlightID = (string)reader[0];
                            flightDetailsTableProperty.AirLineCode = (string)reader[1];
                            flightDetailsTableProperty.AirplanType = (string)reader[2];
                            flightDetailsTableProperty.BusinessFare = (decimal)reader[3];
                            flightDetailsTableProperty.EconnmyFare = (decimal)reader[4];
                            flightDetailsTableProperty.MaxSeat = (int)reader[5];
                            flightDetailsTableProperty.Status = (int)reader[6];




                            searchList.Add(flightDetailsTableProperty);
                        }

                        
                    }
                    if (searchList.Count > 0)
                    {
                        flightTableDetailsResponse.FlightDetailsTablelist = searchList;

                        flightTableDetailsResponse.isDataAvailable = true;
                        flightTableDetailsResponse.message = "Sucess";
                    }

                    else
                    {
                        flightTableDetailsResponse.message = "Contact IT";

                        flightTableDetailsResponse.isDataAvailable = false;

                        flightTableDetailsResponse.message = "Fail";
                    }

                    reader.Close();


                }
            }
            catch (Exception ex)
            {
                // Log exception
                //Display Error message
            }


            return flightTableDetailsResponse;
        }

        public AirlineBlockAndUnblockResponse BlockAndUnblockAirLine(AirlineBlockAndUnblockRequest airlineBlockAndUnblockRequest)
        {
            AirlineBlockAndUnblockResponse airlineBlockAndUnblockResponse = new AirlineBlockAndUnblockResponse();
            try
            {
                //SqlConnection sqlCon = null;

                string connectionString = "Server=JRDOTNETFSECO-2;Database=AirlineTickecting;Trusted_Connection=False;MultipleActiveResultSets=true;user id=sa;password=pass@word1;";

                using (SqlConnection sqlCon = new SqlConnection(connectionString))
                {

                    SqlCommand sql_cmnd = new SqlCommand();

                    sql_cmnd.Connection = sqlCon;


                    //string strQuery = "INSERT INTO Student(Sid,st_name) VALUES (@id,@name)";

                   //string strQuery = "update InventoryMastertb set FromPlace='" + airLineInventoryRequest.From + "',ToPlace='" + airLineInventoryRequest.To + "'  ,DepartureTime=convert(datetime2 ,'" + airLineInventoryRequest.FlightStartDateTime + "',103)  ,ArrivalTime=convert(datetime2 ,'" + airLineInventoryRequest.FlightToDateTime + "',103),TotalBusSeats='" + airLineInventoryRequest.TotalBusinessSeats + "',TotalEcoSeats='" + airLineInventoryRequest.TotalNonBusinessSeats + "',Meal='" + airLineInventoryRequest.Meal + "'  ,Rows='" + airLineInventoryRequest.FlightSeatRow + "'    where FlightID='" + airLineInventoryRequest.FlightNumber + "' and  AirLineCode='" + airLineInventoryRequest.AirLineId + "'";
                    string strQuery = "update FlightMaster set Status='" + airlineBlockAndUnblockRequest.status + "'  where FlightID='" + airlineBlockAndUnblockRequest.FlightNumber + "' and  AirLineCode='" + airlineBlockAndUnblockRequest.AirlinCode + "'";

                    sql_cmnd = new SqlCommand(strQuery, sqlCon);
                   

                    sqlCon.Open();
                    int i = sql_cmnd.ExecuteNonQuery();
                    //int j = (int)sql_cmnd1.ExecuteScalar();
                    sqlCon.Close();

                 
                    if (i > 0)
                    {
                        airlineBlockAndUnblockResponse.message = "Success";
                        airlineBlockAndUnblockResponse.ErrorStatus = 0;
                        airlineBlockAndUnblockResponse.isDataAvailable = true;
                    }

                    else
                    {
                        airlineBlockAndUnblockResponse.message = "Contact IT";
                        airlineBlockAndUnblockResponse.ErrorStatus = 1;
                        airlineBlockAndUnblockResponse.isDataAvailable = false;
                    }


                }
            }
            catch (Exception ex)
            {
                // Log exception
                //Display Error message
            }


            return airlineBlockAndUnblockResponse;
        }





    }
}
