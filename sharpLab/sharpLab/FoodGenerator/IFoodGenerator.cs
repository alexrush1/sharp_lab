namespace nsu.timofeev.sharpLab
{
    public interface IFoodGenerator
    {
        void CreateFood(WorldService worldService);
        Food CreateFoodTest(WorldService worldService, Point point);
    }
}