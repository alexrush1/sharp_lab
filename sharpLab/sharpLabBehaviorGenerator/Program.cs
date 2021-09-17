using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using sharpLabBehaviorGenerator;

namespace ConnectingToSQLServer
{
    class Program
    {

        public static void Main(string[] args)
        {
            ConnectionManager connectionManager = new ConnectionManager();
            SqlConnection connection = connectionManager.Connect();
            BehaviorGenerator behaviorGenerator = new BehaviorGenerator(connection, args[0]);
            behaviorGenerator.CreateTable();
            behaviorGenerator.CreateFood(100);
            behaviorGenerator.FillTable();
        }
    }
}