using nsu.timofeev.sharpLab.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sharpLab.Movers.Wishes
{
    class ChangeDirectionWish : WishEntity
    {
        private Direction _positionDelta;
        public Direction Direction => _positionDelta;

        public ChangeDirectionWish(Direction positionDelta)
        {
            _positionDelta = positionDelta;
        }

        public override WishType GetWish()
        {
            return WishType.CHANGEDIRECTION;
        }
    }
}
