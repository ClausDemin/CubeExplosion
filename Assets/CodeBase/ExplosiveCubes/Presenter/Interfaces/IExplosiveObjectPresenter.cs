using Assets.CodeBase.ExplosiveCubes.View.Interfaces;

namespace Assets.CodeBase.ExplosiveCubes.Presenter
{
    public interface IExplosiveObjectPresenter
    {
        public bool TryExplode(IExplosiveObject explosiveObject);
    }
}
