using Assets.CodeBase.ExplosiveSpore.Interfaces;
using Assets.Scripts.Utils;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.CodeBase
{
    public class SporeFactory: MonoBehaviour
    {
        [SerializeField] private GameObject _sporePrefab;
        [SerializeField] private List<Vector3> _prewarmedPositions = new();

        private ISporeRepository _repository;

        public event Action<ISporeView> InstanceCreated;

        private void Start()
        {
            CreatePrewarmedInstances();
        }

        public GameObject Create(Vector3 position, Vector3 scale, Quaternion rotation, int generation) 
        {
            GameObject sporeInstance = Instantiate(_sporePrefab, position, rotation);

            sporeInstance.transform.localScale = scale;

            if (sporeInstance.TryGetComponent(out ISporeView spore)) 
            {
                ISporeBehavior sporeBehavior = new SporeBehavior(generation);
                _repository.Add(sporeInstance, spore, sporeBehavior);

                SetRandomColor(sporeInstance);

                InstanceCreated?.Invoke(spore);
            } 

            return sporeInstance;
        }

        public void Destroy(ISporeView view) 
        { 
            GameObject instance = _repository.GetInstance(view);
            _repository.Remove(view);

            Destroy(instance);
        }

        public void Init(ISporeRepository repository) 
        { 
            _repository = repository;
        }

        private void CreatePrewarmedInstances()
        {
            if (_prewarmedPositions.Count > 0)
            {
                foreach (var position in _prewarmedPositions)
                {
                    Create(position, _sporePrefab.transform.localScale, _sporePrefab.transform.rotation, 0);
                }
            }
        }

        private void SetRandomColor(GameObject instance) 
        {
            if (instance.TryGetComponent<MeshRenderer>(out var renderer)) 
            { 
                float randomRed = UserUtils.GetRandomFloat();
                float randomBlue = UserUtils.GetRandomFloat();
                float randomGreen = UserUtils.GetRandomFloat();
                float alpha = 1;

                Color cubeColor = new Color(randomRed, randomGreen, randomBlue, alpha);

                renderer.material.SetColor("_Color", cubeColor);
            }
        }
    }
}
