using System.Collections.Generic;
using UnityEngine;

namespace Assets.CodeBase.ExplosiveSpore.Interfaces
{
    public interface IExploder
    {
        public int Generation { get; }

        public void Explode(Vector3 position);

        public void Explode(List<GameObject> gameObjects, Vector3 position);
    }
}
