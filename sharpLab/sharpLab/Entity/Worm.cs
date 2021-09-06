using System;
using nsu.timofeev.sharpLab.Enum;
using nsu.timofeev.sharpLab.Movers;

namespace nsu.timofeev.sharpLab
{

    public class Worm
    {
        public World World { get; }

        public readonly String Name;
        public Point Position;
        public int Steps;
        public int Health;

        public WormMover WormMover;
        public Direction Direction;

        public Worm(String name, WormMover wormMover, World world)
        {
            Name = name;
            Position.X = 0;
            Position.Y = 0;
            Health = 10;
            WormMover = wormMover;
            World = world;
        }

        public Wish GetWish()
        {
            Array values = System.Enum.GetValues(typeof(Wish));
            Random random = new Random();
            return (Wish)values.GetValue(random.Next(values.Length));
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