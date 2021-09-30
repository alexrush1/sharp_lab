namespace WebApplication1.Models
{
    public class WorldModel
    {
        public Worm[] Worms { get; set; }
        public Food[] Food { get; set; }
    }

    public class Worm
    {
        public string name { get; set; }
        public int lifeStrength { get; set; }
        public Position Position { get; set; }
    }

    public class Food
    {
        public int expiresIn { get; set; }
        public Position Position { get; set; }
    }

    public class Position
    {
        public int X { get; set; }
        public int Y { get; set; }
    }
}
