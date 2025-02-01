using System;

namespace Assets.CodeBase.ExplosiveCubes.Model
{
    public interface ISeparableEntity
    {
        public int Generation { get;}

        public bool TrySeparate(out int childCount);
    }
}
