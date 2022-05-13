using DTO.Admin.Properties;
using DTO.Admin.Request;
using DTO.Admin.Response;
using Helper;
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
        Connection con = new Connection();
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
           
            var AirCode = GetRandomPNR();
            var FlyCode = GetRandomPNR();
            int flag = 0;
            try
            {
               

                Connection con = new Connection();

                SqlCommand sql_cmnd = new SqlCommand();

                    sql_cmnd = Connection.con.CreateCommand();
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
                    
                    sql_cmnd.ExecuteNonQuery();
                    
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
                Connection con = new Connection();
                SqlCommand sql_cmnd = new SqlCommand();

                    string strQuery = "update InventoryMastertb set FromPlace='" + airLineInventoryRequest.From+ "',ToPlace='" + airLineInventoryRequest.To + "'  ,DepartureTime=convert(datetime2 ,'" + airLineInventoryRequest.FlightStartDateTime + "',103)  ,ArrivalTime=convert(datetime2 ,'" + airLineInventoryRequest.FlightToDateTime + "',103),TotalBusSeats='" + airLineInventoryRequest.TotalBusinessSeats + "',TotalEcoSeats='" + airLineInventoryRequest.TotalNonBusinessSeats + "',Rows='" + airLineInventoryRequest.FlightSeatRow + "'    where FlightID='" + airLineInventoryRequest.FlightNumber + "' and  AirLineCode='" + airLineInventoryRequest.AirLineId + "'";
                    sql_cmnd = Connection.con.CreateCommand();
                    sql_cmnd.CommandText = strQuery;
                    sql_cmnd.CommandType = CommandType.Text;
                    int i = sql_cmnd.ExecuteNonQuery();
                       if (i > 0)
                    {
                        airLineInventoryResponse.message = "Inventory Updated Successfully";
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
                
                var searchList = new List<FlightDetailsTableProperty>();
                Connection con = new Connection();
                SqlCommand sql_cmnd = new SqlCommand();
                    string strQuery = "select a.AirLineName,f.FlightID,f.AirLineCode,f.AirplanType,f.BusinessFare,f.EconnmyFare,f.MaxSeat,f.Status from FlightMaster as f,AirLineMaster as a where f.AirLineCode = a.AirLineCode";
                sql_cmnd = Connection.con.CreateCommand();
                sql_cmnd.CommandText = strQuery;
                sql_cmnd.CommandType = CommandType.Text;
                SqlDataReader reader = sql_cmnd.ExecuteReader();
                if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            FlightDetailsTableProperty flightDetailsTableProperty = new FlightDetailsTableProperty();
                            flightDetailsTableProperty.AirLineName = (string)reader[0];
                            flightDetailsTableProperty.FlightID = (string)reader[1];
                            flightDetailsTableProperty.AirLineCode = (string)reader[2];
                            flightDetailsTableProperty.AirplanType = (string)reader[3];
                            flightDetailsTableProperty.BusinessFare = (decimal)reader[4];
                            flightDetailsTableProperty.EconnmyFare = (decimal)reader[5];
                            flightDetailsTableProperty.MaxSeat = (int)reader[6];
                            flightDetailsTableProperty.Status = (int)reader[7];




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

                Connection con = new Connection();
                SqlCommand sql_cmnd = new SqlCommand();
                    string strQuery = "update FlightMaster set Status='" + airlineBlockAndUnblockRequest.status + "'  where FlightID='" + airlineBlockAndUnblockRequest.FlightNumber + "' and  AirLineCode='" + airlineBlockAndUnblockRequest.AirlinCode + "'";
                sql_cmnd = Connection.con.CreateCommand();
                sql_cmnd.CommandText = strQuery;
                sql_cmnd.CommandType = CommandType.Text;
                int i = sql_cmnd.ExecuteNonQuery();
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
            catch (Exception ex)
            {
                // Log exception
                //Display Error message
            }


            return airlineBlockAndUnblockResponse;
        }

        public PostCouponCodeResponse AddCoupon(AirlineCouponCodeRequest airlineCouponCodeRequest)
        {
            PostCouponCodeResponse userRegResponse = new PostCouponCodeResponse();

            int flag = 0;
            try
            {
                Connection con = new Connection();

                SqlCommand sql_cmnd = new SqlCommand();

                sql_cmnd = Connection.con.CreateCommand();
 
                    sql_cmnd.CommandText = "sp_Discount";
                    sql_cmnd.CommandType = CommandType.StoredProcedure;
                    sql_cmnd.Parameters.Add("@CouponId", SqlDbType.Int).Value = airlineCouponCodeRequest.CouponId;
                    sql_cmnd.Parameters.Add("@CouponCode", SqlDbType.NVarChar).Value = airlineCouponCodeRequest.CouponCode;
                    sql_cmnd.Parameters.Add("@CouponValue", SqlDbType.Int).Value = airlineCouponCodeRequest.Couponvalue;
                    sql_cmnd.Parameters.Add("@Couponvalidity", SqlDbType.Date).Value = airlineCouponCodeRequest.CouponValidty;
                    sql_cmnd.Parameters.Add("@Message", SqlDbType.NVarChar, 32000).Direction = ParameterDirection.Output;
                     sql_cmnd.ExecuteNonQuery();
                    string result = (string)sql_cmnd.Parameters["@Message"].Value;
                    string[] ab = result.Split("^");
                    if (ab[0] == "0")
                    {
                        userRegResponse.message = "Successfully Generated";
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
            catch (Exception ex)
            {
                // Log exception
                //Display Error message
            }
            return userRegResponse;
        }




        public GetcodeResponse GetCouponDtls()
        {
            GetcodeResponse getcodeResponse = new GetcodeResponse();


            try
            {
                var searchList = new List<CouponCodeDtlsProperties>();
                Connection con = new Connection();
                SqlCommand sql_cmnd = new SqlCommand();
                    string strQuery = "select CouponId,CouponCode,CouponValue,Status from DiscountCoupons";
                sql_cmnd = Connection.con.CreateCommand();
                sql_cmnd.CommandText = strQuery;
                sql_cmnd.CommandType = CommandType.Text;
                SqlDataReader reader = sql_cmnd.ExecuteReader();
                if (reader.HasRows)
                    
                       

                            while (reader.Read())
                            {
                            CouponCodeDtlsProperties couponCodeDtlsProperties = new CouponCodeDtlsProperties();

                            couponCodeDtlsProperties.CouponId = (int)reader[0];
                            couponCodeDtlsProperties.CouponCode = (string)reader[1];
                            couponCodeDtlsProperties.Couponvalue = (int)reader[2];
                            couponCodeDtlsProperties.Status = (int)reader[3];
                            searchList.Add(couponCodeDtlsProperties);

                        }
                             

                        



                    
                    if (searchList.Count > 0)
                    {
                        getcodeResponse.couponCodeDtlsPropertiesList = searchList;

                        getcodeResponse.isDataAvailable = true;
                        getcodeResponse.message = "Sucess";
                    }

                    else
                    {
                        getcodeResponse.message = "Contact IT";

                        getcodeResponse.isDataAvailable = false;

                        getcodeResponse.message = "Fail";
                    }

                    reader.Close();


               
            }
            catch (Exception ex)
            {
                // Log exception
                //Display Error message
            }


            return getcodeResponse;
        }

        public CouponActiveandDeactiveResponse ActivateAndDeactivate(CouponCodeActivateAndDeactivateRequest codeActivateAndDeactivateRequest)
        {
            CouponActiveandDeactiveResponse couponActiveandDeactiveResponse = new CouponActiveandDeactiveResponse();
            try
            {


                Connection con = new Connection();
                SqlCommand sql_cmnd = new SqlCommand();
               
                      string strQuery = "update DiscountCoupons set Status='" + codeActivateAndDeactivateRequest.Status + "'  where CouponId='" + codeActivateAndDeactivateRequest.CouponId + "'";
                sql_cmnd = Connection.con.CreateCommand();
                sql_cmnd.CommandText = strQuery;
                sql_cmnd.CommandType = CommandType.Text;
                int i = sql_cmnd.ExecuteNonQuery();

                if (i > 0)
                    {
                        couponActiveandDeactiveResponse.message = "Success";
                        couponActiveandDeactiveResponse.ErrorStatus = 0;
                        couponActiveandDeactiveResponse.isDataAvailable = true;
                    }

                    else
                    {
                        couponActiveandDeactiveResponse.message = "Contact IT";
                        couponActiveandDeactiveResponse.ErrorStatus = 1;
                        couponActiveandDeactiveResponse.isDataAvailable = false;
                    }


                
            }
            catch (Exception ex)
            {
                // Log exception
                //Display Error message
            }


            return couponActiveandDeactiveResponse;
        }
    }
}
