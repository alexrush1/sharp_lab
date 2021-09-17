using nsu.timofeev.sharpLab;

namespace sharpLab.Database
{
    public sealed class DatabaseFoodLoader : IDatabaseFoodLoader
    {
        private IDatabaseFoodReader _reader;
        private DatabaseContext _database;

        public DatabaseFoodLoader(IDatabaseFoodReader reader, DatabaseContext database)
        {
            _reader = reader;
            _database = database;
        }

        public Point Load(int id)
        {
            return _reader.GetFoodPoint(_database, id);
        }
    }
}