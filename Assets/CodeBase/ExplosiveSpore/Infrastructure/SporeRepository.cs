using Assets.CodeBase.ExplosiveSpore.Interfaces;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.CodeBase.ExplosiveSpore.Infrastructure
{
    public class SporeRepository : ISporeRepository
    {
        private Dictionary<ISporeView, ISporeBehavior> _spores;
        private Dictionary<ISporeView, GameObject> _instances;

        public SporeRepository() 
        {
            _spores = new();
            _instances = new();
        }

        public void Add(GameObject instance, ISporeView view, ISporeBehavior model) 
        { 
            _spores.Add(view, model);
            _instances.Add(view, instance);
        }

        public void Remove(ISporeView view) 
        {
            if (_spores.ContainsKey(view) == false || _instances.ContainsKey(view) == false) 
            {
                return;
            }
            
            _spores.Remove(view);
            _instances.Remove(view);
        }

        public GameObject GetInstance(ISporeView view) 
        {
            if (_instances.ContainsKey(view) == false)
            {
                return default;
            }

            return _instances[view]; 
        }

        public ISporeBehavior GetBehavior(ISporeView view) 
        {
            if (_spores.ContainsKey(view) == false) 
            {
                return default;
            }

            return _spores[view];
        }
    }
}
