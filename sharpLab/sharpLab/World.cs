using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using nsu.timofeev.first_lab.Movers;
using nsu.timofeev.sharpLab.Enum;

namespace nsu.timofeev.sharpLab
{
    public sealed class World
    {

        public readonly List<Worm> Worms = new List<Worm>();
        private StreamWriter _output;
        public readonly List<Food> Foods = new List<Food>();

        public World(StreamWriter output)
        {
            _output = output;
        }

        private String RandName()
        {
            string[] firstPart = { "aaron", "abdul", "abe", "abel", "abraham", "adam", "adan", "adolfo", "adolph", "adrian", "petro", "jorjo", "senjo", "alfredo", "letrejo"};
            string[] secondPart = { "abby", "abigail", "adele", "adrian", "aswer", "qojo", "saran", "bojo", "kambojo", "lejo", "raya", "vaya", "kaya", "peto", "lola"};
        
            Random random = new Random();
            return (string)firstPart.GetValue(random.Next(firstPart.Length)) + "_" + (string)secondPart.GetValue(random.Next(secondPart.Length));
        }

        private bool CheckName(string name)
        {
            foreach (var worm in Worms)
            {
                if (worm.Name.Equals(name))
                {
                    return false;
                }
            }
            return true;
        }

        public Worm AddWorm()
        {
            var randName = RandName();
            while (!CheckName(randName))
            {
                randName = RandName();
            }

            Worm worm = new Worm(randName, new CloseFoodMover(), this);
            Worms.Add(worm);
            return worm;
        }
        
        public void Live()
        {
            for (int i = 0; i < 100; i++)
            {
                CreateFood();
                CheckEatenFood();
                CheckWormWishes();
                CheckDeathWorms();
                FoodLifetime();
                CheckRottenFood();
                Log();
            }
        }
        
        private void Log()
        {
            _output.Write("Worms: [");
            foreach (var worm in Worms)
            {
                _output.Write(" "+ worm.Name + "-" + worm.Health +" (" + worm.Position.X + ", " + worm.Position.Y + "), ");
            }
            _output.Write("], Food: ");
            foreach (var food in Foods)
            {
                _output.Write(" (" + food.Position.X + ", " + food.Position.Y + ") ");
            }
            _output.Write("\n");
        }

        private void CheckDeathWorms()
        {
            Worms.RemoveAll(i => i.Health == 0);
        }

        private void FoodLifetime()
        {
            foreach (var food in Foods)
            {
                food.lifetime--;
            }
        }

        private void CheckRottenFood()
        {
            Foods.RemoveAll(i => i.lifetime < 0);
        }

        private Point FindFreeField()
        {
            Random random = new Random();
            int x = Normal.NextNormal(random);
            int y = Normal.NextNormal(random);
            return new Point(x, y);
        }

        public void CreateFood()
        {
            Point newPoint;
            Boolean done = true;
            while (true)
            {
                newPoint = FindFreeField();
                foreach (var food in Foods)
                {
                    if (newPoint.Equals(food.Position))
                    {
                        done = false;
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
            Foods.Add(new Food(newPoint));
        }

        private void CheckEatenFood()
        {
            foreach (var worm in Worms)
            {
                foreach (var food in Foods.ToList())
                {
                    if (food.Position.Equals(worm.Position))
                    {
                        worm.Health += 10;
                        Foods.Remove(food);
                    }
                }
            }
        }

        public Boolean TestCell(Point target)
        {
            foreach (var worm in Worms.ToList())
            {
                if (worm.Position.Equals(target))
                {
                    return false;
                }
            }

            foreach (var food in Foods.ToList())
            {
                if (food.Position.Equals(target))
                {
                    return false;
                }
            }

            return true;
        }

        public void WormMultiply(Worm worm)
        {
            if (worm.Health > 10)
            {
                worm.Health -= 10;
                Direction direction = worm.GetMultiplyDirection();
                switch (direction)
                {
                    case Direction.UP:
                        if (TestCell(new Point(worm.Position.X, worm.Position.Y + 1)))
                        {
                            AddWorm();
                        }
                        break;
                    case Direction.DOWN:
                        if (TestCell(new Point(worm.Position.X, worm.Position.Y - 1)))
                        {
                            AddWorm();
                        }
                        break;
                    case Direction.LEFT:
                        if (TestCell(new Point(worm.Position.X, worm.Position.Y - 1)))
                        {
                            AddWorm();
                        }
                        break;
                    case Direction.RIGHT:
                        if (TestCell(new Point(worm.Position.X, worm.Position.Y - 1)))
                        {
                            AddWorm();
                        }
                        break;
                }
            }
            
        }

        private void CheckWormWishes()
        {
            foreach (var worm in Worms.ToList())
            {
                Wish wish = worm.GetWish();
                switch (wish)
                {
                    case Wish.MOVE:
                        worm.Move();
                        break;
                    case Wish.STAY:
                        break;
                    case Wish.MULTIPLY:
                        WormMultiply(worm);
                        break;
                }
            }
        }
    }
}