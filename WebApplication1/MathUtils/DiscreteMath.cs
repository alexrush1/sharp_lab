using System;
using System.Collections.Generic;
using WebApplication1.Models;

namespace WebApplication1.MathUtils
{
    public static class DiscreteMath
    {
        public static List<int> FindAllClosest(Position from, Position[] to)
        {
            var minDistance = int.MaxValue;

            for (int i = 0; i < to.Length; i++)
            {
                var distance = GetDiscreteDistance(from, to[i]);
                minDistance = Math.Min(minDistance, distance);
            }

            var result = new List<int>();

            for (int i = 0; i < to.Length; i++)
            {
                var distance = GetDiscreteDistance(from, to[i]);

                if (distance == minDistance)
                {
                    result.Add(i);
                }
            }

            return result;
        }

        public static int GetDiscreteDistance(Position a, Position b)
        {
            return Math.Abs(b.X - a.X) + Math.Abs(b.Y - a.Y);
        }

        public static Position GetDirectionToMove(Position from, Position to)
        {
            var direction = new Position() { X = to.X - from.X, Y = to.Y - from.Y };

            if (direction.X == 0 && direction.Y == 0)
            {
                return new Position() { X = 0, Y = 0 };
            }

            if (Math.Abs(direction.X) < Math.Abs(direction.Y))
            {
                direction.X = 0;
                direction.Y /= Math.Abs(direction.Y);
            }
            else
            {
                direction.X /= Math.Abs(direction.X);
                direction.Y = 0;
            }

            return direction;
        }

        public static string DirectionToString(Position direciton)
        {
            if (direciton.X == 0 && direciton.Y == 1)
            {
                return "Up";
            }
            if (direciton.X == 0 && direciton.Y == -1)
            {
                return "Down";
            }
            if (direciton.X == 1 && direciton.Y == 0)
            {
                return "Right";
            }
            if (direciton.X == -1 && direciton.Y == 0)
            {
                return "Left";
            }

            return "None";
        }

        public static Position FindFreeDirection(Position origin, Position[] obstacles)
        {
            var temp = new Position() { X = origin.X, Y = origin.Y + 1 };

            if (IsCellFree(temp, obstacles))
            {
                return new Position() { X = 0, Y = 1 };
            }

            temp = new Position() { X = origin.X, Y = origin.Y - 1 };

            if (IsCellFree(temp, obstacles))
            {
                return new Position() { X = 0, Y = -1 };
            }

            temp = new Position() { X = origin.X + 1, Y = origin.Y };

            if (IsCellFree(temp, obstacles))
            {
                return new Position() { X = 1, Y = 0 };
            }

            temp = new Position() { X = origin.X - 1, Y = origin.Y };

            if (IsCellFree(temp, obstacles))
            {
                return new Position() { X = -1, Y = 0 };
            }

            return new Position() { X = 0, Y = 0 };
        }

        public static bool IsCellFree(Position target, Position[] obstacles)
        {
            for (int i = 0; i < obstacles.Length; i++)
            {
                if (obstacles[i].X == target.X && obstacles[i].Y == target.Y)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
