using System;
using nsu.timofeev.sharpLab.Enum;

namespace nsu.timofeev.sharpLab.Movers
{
    public class CircleMover : IWormMover
    {
        private Point[] _points = new[]
        {
            new Point(0, 1),
            new Point(1, 1),
            new Point(1, 0),
            new Point(1, -1),
            new Point(0, -1),
            new Point(-1, -1),
            new Point(-1, 0),
            new Point(-1, 1)
        };

        public Wish GetWish()
        {
            return Wish.MOVE;
        }

        public void Move(Worm worm)
        {
            worm.Position = _points[worm.Steps % _points.Length];
            worm.Steps++;
            worm.Health--;
        }
    }
}