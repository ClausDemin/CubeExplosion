using Assets.CodeBase.ExplosiveCubes.Model;
using Assets.CodeBase.ExplosiveCubes.View.Interfaces;

namespace Assets.CodeBase.ExplosiveCubes.Presenter.Interfaces
{
    public interface IExplosiveObjectPresenterFactory
    {
        public IExplosiveObjectPresenter Create(IExplosiveObjectsFactory objectsFactory, ISeparableEntity separableEntity);
    }
}
