using Assets.CodeBase.ExplosiveCubes.Presenter;
using System;
using UnityEngine;

namespace Assets.CodeBase.ExplosiveCubes.View.Interfaces
{
    public interface IExplosiveObject
    {
        public GameObject gameObject { get; }

        public float ExplosionForce { get; }
        public float ExplosionRadius { get; }

        public void Init(IExplosiveObjectPresenter presenter);
    }
}
