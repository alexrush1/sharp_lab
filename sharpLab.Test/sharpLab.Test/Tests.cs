using System;
using nsu.timofeev.sharpLab;
using nsu.timofeev.sharpLab.Movers;
using nsu.timofeev.sharpLab.OutputWriter;
using NUnit.Framework;

namespace sharpLab.Test
{
    [TestFixture]
    public class Tests
    {
        private WorldService _worldService;
        private nsu.timofeev.sharpLab.FoodGenerator _foodGenerator;

        [SetUp]
        public void SetUp()
        {
            _foodGenerator = new nsu.timofeev.sharpLab.FoodGenerator();
            _worldService = new WorldService(new CircleMover(), _foodGenerator, new NameGenerator(), new OutputFileWriter("log.txt"));
        }


        [Test]
        public void WormMoverTest()
        {
            Worm worm = _worldService.AddWorm();
            Point oldPoint = worm.Position;
            
            worm.Move();
            
            Assert.True(!oldPoint.Equals(worm.Position));
        }

        [Test]
        public void WormMultiplyWithoutChildTest()
        {
            Worm worm = _worldService.AddWorm();
            int wormCount = _worldService.Worms.Count;

            worm.Health = 2;
            _worldService.WormMultiply(worm);
            
            Assert.True(_worldService.Worms.Count == wormCount);
        }
        
        [Test]
        public void WormMultiplyWithChildTest()
        {
            Worm worm = _worldService.AddWorm();
            int wormCount = _worldService.Worms.Count;

            worm.Health = 12;
            _worldService.WormMultiply(worm);
            
            Assert.True(_worldService.Worms.Count != wormCount);
        }
        
        [Test]
        public void WormUniqNameTest()
        {
            Worm worm1 = _worldService.AddWorm();
            worm1.Move();
            Worm worm2 = _worldService.AddWorm();

            Assert.True(!worm1.Name.Equals(worm2.Name));
        }

        [Test]
        public void UniqNewFoodDots()
        {
            _foodGenerator.CreateFood(_worldService);
            _foodGenerator.CreateFood(_worldService);
            
            Assert.True(!_worldService.Foods[0].Position.Equals(_worldService.Foods[1].Position));
        }
        
        //TODO: 1. Тест если мув на занятую клетку, размножение на клетку с едой

        [Test]
        public void BusyCellTest()
        {
            _worldService.AddWorm();
            _worldService.AddWorm();
            
            Assert.True(_worldService.Worms.Count == 1);
        }

        [Test]
        public void MultiplyOnFoodTest()
        {
            _worldService.Worms.Clear();
            Worm worm1 = _worldService.AddWorm();
            Food food1 = _foodGenerator.CreateFoodTest(_worldService, new Point(0,1));
            worm1.Move();
            _worldService.Round(0);
            Console.WriteLine("FOOD: " + food1.Position.X + ", " + food1.Position.Y);
            Console.WriteLine("WORM: " + _worldService.Worms[0].Position.X + ", " + _worldService.Worms[0].Position.Y);
            Console.WriteLine("HEALTH: " + worm1.Health);
            Assert.True(worm1.Health > 10);
        }
    }
}