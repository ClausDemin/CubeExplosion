
using UnityEngine;

namespace Assets.CodeBase.ExplosiveSpore.Interfaces
{
    public interface ISporeRepository
    {
        public void Add(Spore instance, ISporeView view, IExploder model);

        public void Remove(ISporeView view);

        public IExploder GetBehavior(ISporeView view);

        public Spore GetInstance(ISporeView view);
    }
}
