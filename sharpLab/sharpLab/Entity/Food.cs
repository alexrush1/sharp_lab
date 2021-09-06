namespace nsu.timofeev.sharpLab
{
    public class Food
    {
        public Point Position;
        public int lifetime = 10;

        public Food(Point position)
        {
            Position = position;
        }
    }
}