using nsu.timofeev.sharpLab;

namespace sharpLab.Database
{
    public class DatabaseFoodReader : IDatabaseFoodReader
    {

        public Point GetFoodPoint(DatabaseContext database, int id)
        {
            foreach (var position in database.Positions) {
                if (position.id == id)
                {
                    return new Point(position.pointX, position.pointY);
                }
            }

            return new Point(0, 0);
        }
        
    }
}