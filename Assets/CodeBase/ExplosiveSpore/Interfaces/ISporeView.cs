using System;
using UnityEngine;

namespace Assets.CodeBase.ExplosiveSpore.Interfaces
{
    public interface ISporeView
    {
        public event Action<ISporeView> Clicked;
        public void PlayEffects();

        public float ExplosionRadius { get; }
        public float ExplosionForce { get; }
    }
}
