using System;

namespace nsu.timofeev.sharpLab
{
    public class FoodGenerator : IFoodGenerator
    {
        private Random _random = new Random();

        private Point FindFreeField()
        {
            int x = Normal.NextNormal(_random);
            int y = Normal.NextNormal(_random);
            return new Point(x, y);
        }

        public void CreateFood(WorldService worldService)
        {
            Point newPoint;
            Boolean done = true;
            while (true)
            {
                newPoint = FindFreeField();
                foreach (var food in worldService.Foods)
                {
                    if (newPoint.Equals(food.Position))
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

            worldService.Foods.Add(new Food(newPoint));
        }

        public Food CreateFoodTest(WorldService worldService, Point point)
        {
            worldService.Foods.Add(new Food(point));
            return worldService.Foods[0];
        }
    }
}