using nsu.timofeev.sharpLab;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace sharpLabBehaviorGenerator
{
    public class BehaviorGenerator
    {
        private string _behaviorName;
        private SqlConnection sqlConnection;
        private Random _random = new Random();
        private List<Point> foods = new List<Point>();

        public BehaviorGenerator(SqlConnection sqlConnection, string behaviorName)
        {
            this.sqlConnection = sqlConnection;
            _behaviorName = behaviorName;
        }

        public void CreateTable() {
            SqlCommand sqlCommand = new SqlCommand("CREATE TABLE [" + _behaviorName + "] ( [id] INT NOT NULL PRIMARY KEY IDENTITY, [pointX] INT NOT NULL, [pointY] INT NOT NULL)", sqlConnection);
            sqlCommand.ExecuteNonQuery();
        }

        private Point FindFreeField()
        {
            int x = Normal.NextNormal(_random);
            int y = Normal.NextNormal(_random);
            return new Point(x, y);
        }

        public void CreateFood(int count)
        {
            for (int i = 0; i < count; i++)
            {
                Point newPoint;
                Boolean done = true;
                while (true)
                {
                    newPoint = FindFreeField();
                    foreach (var food in foods)
                    {
                        if (newPoint.Equals(food))
                        {
                            done = false;
                            break;
                        }
                        else
                        {
                            done = true;
                        }
                    }

                    if (done)
                    {
                        break;
                    }
                }

                foods.Add(newPoint);
            }
        }

        public void FillTable() 
        {
            foreach (var food in foods)
            {
                SqlCommand sqlCommand = new SqlCommand("INSERT INTO [" + _behaviorName + "] (pointX, pointY) values ("+ food.X + ", " + food.Y + ")", sqlConnection);
                sqlCommand.ExecuteNonQuery();
            }
        }
    }
}