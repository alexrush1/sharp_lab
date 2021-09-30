using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sharpLab.Movers
{
    public enum WishType
    {
        STAY,
        CHANGEDIRECTION,
        MULTIPLY
    }

    public abstract class WishEntity
    {
        public abstract WishType GetWish();
    }
}
