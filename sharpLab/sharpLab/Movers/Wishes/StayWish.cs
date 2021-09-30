using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sharpLab.Movers.Wishes
{
    class StayWish : WishEntity
    {
        public override WishType GetWish()
        {
            return WishType.STAY;
        }
    }
}
