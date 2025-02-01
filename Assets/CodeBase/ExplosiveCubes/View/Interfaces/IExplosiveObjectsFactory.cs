using UnityEngine;

namespace Assets.CodeBase.ExplosiveCubes.View.Interfaces
{
    public interface IExplosiveObjectsFactory
    {
        public IExplosiveObject Create(Vector3 position, Quaternion rotation, int generation);

        public IExplosiveObject Create(IExplosiveObject parent, int generation);
    }
}
