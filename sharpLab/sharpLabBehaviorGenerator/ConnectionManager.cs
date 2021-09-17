using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace sharpLabBehaviorGenerator
{
    public class ConnectionManager
    {
        private SqlConnection sqlConnection = null;

        public SqlConnection Connect()
        {
            sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["WormDB"].ConnectionString);
            sqlConnection.Open();
            if (sqlConnection.State == ConnectionState.Open)
            {
                Console.WriteLine("Connection successfully!");
            }
            return sqlConnection;
        }
    }
}