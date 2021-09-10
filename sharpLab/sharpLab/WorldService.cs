using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using nsu.timofeev.first_lab.Movers;
using nsu.timofeev.sharpLab.Enum;
using nsu.timofeev.sharpLab.Movers;
using nsu.timofeev.sharpLab.OutputWriter;

namespace nsu.timofeev.sharpLab
{
    public sealed class WorldService : IHostedService
    {
        private IWormMover _wormMover;
        private static IOutputWriter _output;
        private IFoodGenerator _foodGenerator;
        private INameGenerator _nameGenerator;
        
        public readonly List<Worm> Worms = new List<Worm>();
        public readonly List<Food> Foods = new List<Food>();

        public WorldService(IWormMover wormMover, IFoodGenerator foodGenerator, INameGenerator nameGenerator, IOutputWriter outputWriter)
        {
            _wormMover = wormMover;
            _foodGenerator = foodGenerator;
            _nameGenerator = nameGenerator;
            _output = outputWriter;
            AddWorm();
        }

        public Worm AddWorm()
        {
            Worm worm = new Worm(_nameGenerator.Generate(this), _wormMover, this);
            Worms.Add(worm);
            return worm;
        }
        
        public void Live()
        {
            for (int i = 0; i < 100; i++)
            {
                Round(i);
            }
        }

        public void Round(int i)
        {
            _foodGenerator.CreateFood(this);
            CheckEatenFood();
            CheckWormWishes();
            CheckDeathWorms();
            FoodLifetime();
            CheckRottenFood();
            _output.Log(this, i);
        }

        private void CheckDeathWorms()
        {
            Worms.RemoveAll(i => i.Health == 0);
        }

        private void FoodLifetime()
        {
            foreach (var food in Foods.ToList())
            {
                food.lifetime--;
            }
        }

        private void CheckRottenFood()
        {
            Foods.RemoveAll(i => i.lifetime < 0);
        }

        private void CheckEatenFood()
        {
            foreach (var worm in Worms.ToList())
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
                worm.Health--;
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

        public Task StartAsync(CancellationToken cancellationToken)
        {
            return Task.Run(Live, cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}