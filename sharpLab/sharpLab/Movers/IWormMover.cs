using nsu.timofeev.sharpLab.Enum;

namespace nsu.timofeev.sharpLab.Movers
{
    public interface IWormMover
    {
        Wish GetWish(Worm worm);
        Point Move(Worm worm);
    }
}