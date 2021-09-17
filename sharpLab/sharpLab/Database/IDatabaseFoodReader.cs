using nsu.timofeev.sharpLab;

namespace sharpLab.Database
{
    public interface IDatabaseFoodReader
    {
        Point GetFoodPoint(DatabaseContext database, int id);
    }
}