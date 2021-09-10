using System;
using nsu.timofeev.sharpLab;
using nsu.timofeev.sharpLab.Enum;
using nsu.timofeev.sharpLab.Movers;

namespace nsu.timofeev.first_lab.Movers
{
    public class CloseFoodMover : IWormMover
    {
        private Point _closestFood;
        
        public Point FindClosestFood(Worm worm)
        {
            int minDistance = Int32.MaxValue;
            int currentDistance = Int32.MaxValue;
            int foodX, foodY;
            Point ToFood = new Point(0, 0);
            foreach (var food in worm.WorldService.Foods)
            {
                foodX = food.Position.X - worm.Position.X;
                foodY = food.Position.Y - worm.Position.Y;
                currentDistance = (Math.Abs(foodX) + Math.Abs(foodY));
                if (currentDistance < minDistance)
                {
                    ToFood.X = foodX;
                    ToFood.Y = foodY;
                    minDistance = currentDistance;
                }
            }
            return ToFood;
        }

        public Wish GetWish()
        {
            return Wish.MOVE;
        }

        public Point Move(Worm worm)
        {
            _closestFood = FindClosestFood(worm);
            if (_closestFood.X > 0)
            {
                worm.Position.X++;
            }
            else if (_closestFood.X < 0)
            {
                worm.Position.X--;
            }
            else if (_closestFood.Y > 0)
            {
                worm.Position.Y++;
            }
            else if (_closestFood.Y < 0)
            {
                worm.Position.Y--;
            }
            else
            {
                worm.Position.X++;
            }

            return _closestFood;
        }
    }
}