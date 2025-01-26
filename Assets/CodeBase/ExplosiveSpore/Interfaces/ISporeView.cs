using System;
using UnityEngine;

namespace Assets.CodeBase.ExplosiveSpore.Interfaces
{
    public interface ISporeView
    {
        public event Action<ISporeView> Clicked;
        public event Action<GameObject> Exploded;

        public Vector3 Position { get; }
        public Vector3 Scale { get; }
        public void Explode();

    }
}
