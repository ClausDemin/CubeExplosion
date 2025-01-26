
using UnityEngine;

namespace Assets.CodeBase.ExplosiveSpore.Interfaces
{
    public interface ISporeRepository
    {
        public void Add(GameObject instance, ISporeView view, ISporeBehavior model);

        public void Remove(ISporeView view);

        public ISporeBehavior GetBehavior(ISporeView view);

        public GameObject GetInstance(ISporeView view);
    }
}
