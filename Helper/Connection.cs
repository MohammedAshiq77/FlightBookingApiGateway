using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Helper
{
   public class Connection
    {
        private string strconection = "";
        public static SqlConnection con;
       
        public Connection()
        {
            strconection = new AppConfigManager().GetConnectionString;
            con = new SqlConnection(strconection);
                   con.Open();
        }
        public static int sqlquery(string str)
        {
            SqlCommand cmd = new SqlCommand();
            cmd = Connection.con.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = str;
            int i = cmd.ExecuteNonQuery();
            con.Close();
            return i;
        }
        public static SqlDataReader login(string str)
        {
            SqlCommand cmd = new SqlCommand();
            cmd = Connection.con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = str;
            SqlDataReader dr = cmd.ExecuteReader();
            return dr;
        }

        public static DataTable datatab(string str)
        {
            SqlCommand cmd = new SqlCommand();
            cmd = Connection.con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = str;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;

        }
    }
}
