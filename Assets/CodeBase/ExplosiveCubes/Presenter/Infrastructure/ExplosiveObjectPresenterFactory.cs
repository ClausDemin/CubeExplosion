using Assets.CodeBase.ExplosiveCubes.Model.Interfaces;
using Assets.CodeBase.ExplosiveCubes.Model;
using Assets.CodeBase.ExplosiveCubes.View.Interfaces;
using Assets.CodeBase.ExplosiveCubes.Presenter.Interfaces;

namespace Assets.CodeBase.ExplosiveCubes.Presenter.Infrastructure
{
    public class ExplosiveObjectPresenterFactory: IExplosiveObjectPresenterFactory
    {
        private IExploder _exploder;

        public ExplosiveObjectPresenterFactory(IExploder exploder)
        {
            _exploder = exploder;
        }   

        public IExplosiveObjectPresenter Create(IExplosiveObjectsFactory objectsFactory, ISeparableEntity separableEntity) 
        {
            return new ExplosiveObjectPresenter(objectsFactory, _exploder, separableEntity);
        }
    }
}
