using Assets.CodeBase.ExplosiveCubes.Model;
using Assets.CodeBase.ExplosiveCubes.Model.Interfaces;
using Assets.CodeBase.ExplosiveCubes.View.Interfaces;
using Assets.Scripts.Utils;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.CodeBase.ExplosiveCubes.Presenter
{
    public class ExplosiveObjectPresenter: IExplosiveObjectPresenter
    {
        private IExplosiveObjectsFactory _objectsFactory;
        private IExploder _exploder;
        private ISeparableEntity _separableEntity;

        public ExplosiveObjectPresenter(IExplosiveObjectsFactory objectsFactory, IExploder exploder, ISeparableEntity separableEntity)
        {
            _objectsFactory = objectsFactory;
            _exploder = exploder;
            _separableEntity = separableEntity;
        }

        public bool TryExplode(IExplosiveObject explosiveObject) 
        {
            bool separated = _separableEntity.TrySeparate(out int childCount);

            if (separated) 
            {
                List<Rigidbody> involvedObject = new List<Rigidbody>();

                Vector3 position = explosiveObject.gameObject.transform.position;

                for (int i = 0; i < childCount; i++)
                {
                    IExplosiveObject child = _objectsFactory.Create(explosiveObject, _separableEntity.Generation + 1);

                    if (child.gameObject.TryGetComponent(out Rigidbody rigidbody)) 
                    {
                        if (IsInvolved(rigidbody, position)) 
                        {
                            involvedObject.Add(rigidbody);
                        }
                    }
                }

                _exploder.Explode(position, involvedObject);
            }

            GameObject.Destroy(explosiveObject.gameObject);

            return separated;
        }

        private bool IsInvolved(Rigidbody rigidbody, Vector3 explosionPosition) 
        {
            float distance = UserUtils.GetDistanceBetween(explosionPosition, rigidbody.transform.position);

            return (distance <= _exploder.ExplosionRadius);
        }
    }
}
