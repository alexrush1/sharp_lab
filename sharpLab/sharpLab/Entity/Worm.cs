using System;
using nsu.timofeev.sharpLab.Enum;
using nsu.timofeev.sharpLab.Movers;

namespace nsu.timofeev.sharpLab
{

    public class Worm
    {
        public WorldService WorldService { get; }

        public readonly String Name;
        public Point Position;
        public int Steps;
        public int Health;

        public Wish Wish;

        public IWormMover WormMover;

        public Worm(String name, IWormMover wormMover, WorldService worldService)
        {
            Name = name;
            Position.X = 0;
            Position.Y = 0;
            Health = 10;
            WormMover = wormMover;
            WorldService = worldService;
        }

        public Wish GetWish()
        {
            return WormMover.GetWish();
            // Array values = System.Enum.GetValues(typeof(Wish));
            // Random random = new Random(DateTime.Now.Millisecond);
            // return (Wish)values.GetValue(random.Next(values.Length));
        }

        public Direction GetMultiplyDirection()
        {
            Array values = System.Enum.GetValues(typeof(Direction));
            Random random = new Random();
            return (Direction)values.GetValue(random.Next(values.Length));
        }

        public void Move()
        {
            WormMover.Move(this);
        }
    }
}