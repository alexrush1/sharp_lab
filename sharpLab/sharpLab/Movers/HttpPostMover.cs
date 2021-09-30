using nsu.timofeev.sharpLab;
using nsu.timofeev.sharpLab.Enum;
using nsu.timofeev.sharpLab.Movers;
using sharpLab.Movers.Wishes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace sharpLab.Movers
{
    class HttpPostMover : IWormMover
    {
        private string _urlBase;
        private Wish _wish;

        public HttpPostMover(string ip, string port)
        {
            _urlBase = $"https://{ip}:{port}/";
        }

        public WishEntity RequestBehaviour(Worm target, WorldService world)
        {
            var model = CreateWorldModel(world);
            var json = JsonSerializer.Serialize(model);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var url = $"{_urlBase}BehaviourGenerator/{target.Name}/getAction";

            using (var client = new HttpClient())
            {
                var task = client.PostAsync(url, data);
                task.Wait(1000);
                var response = task.Result;
                string result = response.Content.ReadAsStringAsync().Result;
                var behaviour = JsonSerializer.Deserialize<Behaviour>(result);
                return CreateBehaviourFromModel(behaviour);
            }
        }

        private WorldModel CreateWorldModel(WorldService world)
        {
            var worms = new WormModel[world.Worms.Count];
            var foods = new FoodModel[world.Foods.Count];

            for (int i = 0; i < worms.Length; i++)
            {
                worms[i] = new WormModel()
                {
                    Name = world.Worms.ElementAt(i).Name,
                    LifeStrength = world.Worms.ElementAt(i).Health,
                    Position = new PositionModel()
                    {
                        X = world.Worms.ElementAt(i).Position.X,
                        Y = world.Worms.ElementAt(i).Position.Y
                    }
                };
            }

            for (int i = 0; i < foods.Length; i++)
            {
                foods[i] = new FoodModel()
                {
                    ExpiresIn = world.Foods.ElementAt(i).lifetime,
                    Position = new PositionModel()
                    {
                        X = world.Foods.ElementAt(i).Position.X,
                        Y = world.Foods.ElementAt(i).Position.Y
                    }
                };
            }

            return new WorldModel() { Worms = worms, Food = foods };
        }

        private WishEntity CreateBehaviourFromModel(Behaviour model)
        {
            Direction? direction;

            switch (model.direciton)
            {
                case "Up":
                    direction = Direction.UP;
                    break;
                case "Down":
                    direction = Direction.DOWN;
                    break;
                case "Right":
                    direction = Direction.RIGHT;
                    break;
                case "Left":
                    direction = Direction.LEFT;
                    break;
                default:
                    direction = null;
                    break;
            }
            _wish = Wish.MOVE;

            if (direction == null)
            {
                _wish = Wish.STAY;
                return new StayWish();
            }

            if (model.split)
            {
                _wish = Wish.MULTIPLY;
                return new MultiplyWish((Direction)direction);
            }

            return new ChangeDirectionWish((Direction)direction);
        }

        public Wish GetWish(Worm worm)
        {
            WishEntity wishEntity = RequestBehaviour(worm, worm.WorldService);
            if (wishEntity.GetWish() == WishType.CHANGEDIRECTION) 
            {
                ChangeDirectionWish cdw = (ChangeDirectionWish)wishEntity;
                worm.Direction = cdw.Direction;
            }
            return _wish;
        }

        public Point Move(Worm worm)
        {
            switch (worm.Direction)
            {
                case Direction.UP:
                    worm.Position.Y--;
                    break;
                case Direction.DOWN:
                    worm.Position.Y++;
                    break;
                case Direction.RIGHT:
                    worm.Position.X--;
                    break;
                case Direction.LEFT:
                    worm.Position.X++;
                    break;
                default:
                    break;
            }
            return worm.Position;
        }
    }
}
