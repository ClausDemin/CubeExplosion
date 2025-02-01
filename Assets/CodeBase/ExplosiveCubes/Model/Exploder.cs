using Assets.CodeBase.ExplosiveCubes.Model.Interfaces;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.CodeBase.ExplosiveCubes.Model
{
    public class Exploder : IExploder
    {
        public Exploder(float explosionForce, float explosionRadius)
        {
            ExplosionForce = explosionForce;
            ExplosionRadius = explosionRadius;
        }

        public float ExplosionForce { get; private set; }
        public float ExplosionRadius { get; private set; }

        public void Explode(Vector3 position, List<Rigidbody> involvedObjects)
        {
            foreach (var rigidbody in involvedObjects)
            {
                rigidbody.AddExplosionForce(ExplosionForce, position, ExplosionRadius);
            }
        }
    }
}

