using Assets.CodeBase.ExplosiveSpore.Interfaces;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.CodeBase.ExplosiveSpore.Infrastructure
{
    public class SporeRepository : ISporeRepository
    {
        private Dictionary<ISporeView, IExploder> _spores;
        private Dictionary<ISporeView, Spore> _instances;

        public SporeRepository() 
        {
            _spores = new();
            _instances = new();
        }

        public void Add(Spore instance, ISporeView view, IExploder model) 
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

        public Spore GetInstance(ISporeView view) 
        {
            if (_instances.ContainsKey(view) == false)
            {
                return default;
            }

            return _instances[view]; 
        }

        public IExploder GetBehavior(ISporeView view) 
        {
            if (_spores.ContainsKey(view) == false) 
            {
                return default;
            }

            return _spores[view];
        }
    }
}
