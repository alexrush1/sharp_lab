using System;
using System.IO;
using nsu.timofeev.sharpLab;
using NUnit.Framework;

namespace sharpLab.Test
{
    [TestFixture]
    public class Tests
    {
        private World _world;

        [SetUp]
        public void SetUp()
        {
            StreamWriter streamWriter = new StreamWriter("log.txt");
            _world = new World(streamWriter);
        }

        [Test]
        public void WormMoverTest()
        {
            Worm worm = _world.AddWorm();
            Point oldPoint = worm.Position;
            
            worm.Move();
            
            Assert.True(!oldPoint.Equals(worm.Position));
        }

        [Test]
        public void WormMultiplyWithoutChildTest()
        {
            Worm worm = _world.AddWorm();
            int wormCount = _world.Worms.Count;

            worm.Health = 2;
            _world.WormMultiply(worm);
            
            Assert.True(_world.Worms.Count == wormCount);
        }
        
        [Test]
        public void WormMultiplyWithChildTest()
        {
            Worm worm = _world.AddWorm();
            int wormCount = _world.Worms.Count;

            worm.Health = 12;
            _world.WormMultiply(worm);
            
            Assert.True(_world.Worms.Count != wormCount);
        }
        
        [Test]
        public void WormUniqNameTest()
        {
            Worm worm1 = _world.AddWorm();
            worm1.Move();
            Worm worm2 = _world.AddWorm();

            Assert.True(!worm1.Name.Equals(worm2.Name));
        }

        [Test]
        public void UniqNewFoodDots()
        {
            _world.CreateFood();
            _world.CreateFood();
            
            Assert.True(!_world.Foods[0].Position.Equals(_world.Foods[1].Position));
        }

    }
}