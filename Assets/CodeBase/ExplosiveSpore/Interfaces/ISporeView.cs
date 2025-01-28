using System;
using UnityEngine;

namespace Assets.CodeBase.ExplosiveSpore.Interfaces
{
    public interface ISporeView
    {
        public event Action<ISporeView> Clicked;

        public float ExplosionRadius { get; }
        public float ExplosionForce { get; }

        public void PlayEffects();

    }
}
