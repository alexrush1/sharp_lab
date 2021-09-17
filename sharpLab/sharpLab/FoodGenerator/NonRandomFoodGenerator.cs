using nsu.timofeev.sharpLab;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;
using sharpLab.Database;

namespace sharpLab.FoodGenerator
{
    class NonRandomFoodGenerator : IFoodGenerator
    {
        private readonly IDatabaseFoodLoader _foodDataLoader;

        private List<Point> _foods = new List<Point>();

        public NonRandomFoodGenerator(IDatabaseFoodLoader foodDataLoader)
        {
            _foodDataLoader = foodDataLoader;

            try
            {
                for (int i = 0; i < 100; i++)
                {
                    _foods.Add(_foodDataLoader.Load(i));
                }
            }
            catch (Exception)
            {
                _foods.Clear();
                Console.WriteLine("Failed to load behaviour!");
                _foods.Add(new Point(0, 0));
            }
        }

        public void CreateFood(WorldService worldService)
        {
            worldService.Foods.Add(new Food(_foods[worldService.RoundId - 1]));
        }

        public Food CreateFoodTest(WorldService worldService, Point point)
        {
            throw new NotImplementedException();
        }
    }
}
