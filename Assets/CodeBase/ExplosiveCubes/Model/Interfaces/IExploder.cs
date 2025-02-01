using System.Collections.Generic;
using UnityEngine;

namespace Assets.CodeBase.ExplosiveCubes.Model.Interfaces
{
    public interface IExploder
    {
        public float ExplosionForce { get; }
        public float ExplosionRadius { get; }

        public void Explode(Vector3 position, List<Rigidbody> involvedObjects);
    }
}
