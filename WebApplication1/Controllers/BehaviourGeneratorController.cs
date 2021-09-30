using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using WebApplication1.MathUtils;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BehaviourGeneratorController : ControllerBase
    {
        private static int _postCounter = 0;

        private readonly ILogger<BehaviourGeneratorController> _logger;

        public BehaviourGeneratorController(ILogger<BehaviourGeneratorController> logger)
        {
            _logger = logger;
        }

        //Test Data
        //{"Worms":[{"Name":"John","LifeStrength":20,"Position":{"X":1,"Y":1}}],"Food":[{"ExpiresIn":10,"Position":{"X":2,"Y":-1}}]}

        [HttpPost("{name}/getAction")]
        public BehaviourModel Post(string name, WorldModel world)
        {
            Console.WriteLine(_postCounter++);

            var wormsPositions = new Position[world.Worms.Length];
            var foodsPositions = new Position[world.Food.Length];

            Worm currentWorm = new Worm();
            int currentWormIndex = 0;

            for (int i = 0; i < world.Worms.Length; i++)
            {
                wormsPositions[i] = world.Worms[i].Position;
                if (world.Worms[i].name.Equals(name))
                {
                    currentWormIndex = i;
                    currentWorm = world.Worms[i];
                }
            }

            for (int i = 0; i < world.Food.Length; i++)
            {
                foodsPositions[i] = world.Food[i].Position;
            }

            var closestFoods = new (Worm, Food)[world.Worms.Length];
            Food closestFood;

            for (int i = 0; i < closestFoods.Length; i++)
            {
                var temp = world.Food[DiscreteMath.FindAllClosest(world.Worms[i].Position, foodsPositions)[0]];
                closestFoods[i] = (world.Worms[i], temp);
            }

            closestFood = closestFoods[currentWormIndex].Item2;

            var closestWorm = world.Worms[DiscreteMath.FindAllClosest(closestFood.Position, wormsPositions)[0]];

            var split = false;
            var doNothing = false;

            if (currentWorm.Position.X != closestWorm.Position.X ||
                currentWorm.Position.Y != closestWorm.Position.Y)
            {
                var foodsLeft = new List<Food>(world.Food);

                Console.WriteLine($"Before {foodsLeft.Count}");

                for (int i = 0; i < closestFoods.Length; i++)
                {
                    foodsLeft.Remove(closestFoods[i].Item2);
                }

                Console.WriteLine($"After {foodsLeft.Count}");

                var foodsLeftPositions = new Position[foodsLeft.Count];

                for (int i = 0; i < foodsLeft.Count; i++)
                {
                    foodsLeftPositions[i] = foodsLeft[i].Position;
                }

                if (foodsLeft.Count == 0)
                {
                    doNothing = true;
                }
                else
                {
                    closestFood = foodsLeft[DiscreteMath.FindAllClosest(currentWorm.Position, foodsLeftPositions)[0]];
                }
            }

            var direciton = DiscreteMath.GetDirectionToMove(currentWorm.Position, closestFood.Position);

            if (currentWorm.lifeStrength > 25 || _postCounter > 580 && currentWorm.lifeStrength > 13)
            {
                direciton = DiscreteMath.FindFreeDirection(currentWorm.Position, foodsPositions);
                split = true;
                doNothing = false;
            }

            if (doNothing)
            {
                return new BehaviourModel() { Direciton = "None", Split = false };
            }

            var directionStr = DiscreteMath.DirectionToString(direciton);
            return new BehaviourModel() { Direciton = directionStr, Split = split };
        }
    }
}
