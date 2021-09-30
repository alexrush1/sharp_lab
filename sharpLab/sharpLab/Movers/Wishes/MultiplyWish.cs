using nsu.timofeev.sharpLab.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sharpLab.Movers.Wishes
{
    class MultiplyWish : WishEntity
    {

        private Direction _direction;
        public Direction Direction => _direction;

        public MultiplyWish(Direction direction)
        {
            _direction = direction;
        }

        public override WishType GetWish()
        {
            return WishType.MULTIPLY;
        }
    }
}
