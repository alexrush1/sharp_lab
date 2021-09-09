using nsu.timofeev.sharpLab.Enum;

namespace nsu.timofeev.sharpLab.Movers
{
    public interface IWormMover
    {
        Wish GetWish();
        void Move(Worm worm);
    }
}