using System;

namespace nsu.timofeev.sharpLab
{
    public struct Point : IEquatable<Point>
    {
        public int X;
        public int Y;

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public void Add(Point term)
        {
            X += term.X;
            Y += term.Y;
        }

        public bool Equals(Point obj)
        {
            return X ==  obj.X && Y == obj.Y;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}