using UnityEngine;

namespace Assets.CodeBase.ExplosiveSpore.Interfaces
{
    public interface ISporeBehavior
    {
        public int Generation { get; }
        public Vector3 Position { get; }
        public Vector3 Scale { get; }
        public void Explode();
    }
}
