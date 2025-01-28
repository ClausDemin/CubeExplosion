using Assets.CodeBase.ExplosiveSpore.Interfaces;
using Assets.CodeBase.ExplosiveSpore.Presenter;
using Assets.Scripts.Utils;
using System;
using System.Collections.Generic;
using UnityEngine;
namespace Assets.CodeBase.ExplosiveSpore.Infrastructure
{
    public class SporeSpawner: MonoBehaviour
    {
        [SerializeField, Range(0, 1)] private float _baseDivisionChance;
        [SerializeField, Range(0, 1)] private float _divisionChanceFactor;
        [SerializeField, Range(0, 1)] private float _scaleFactor;
        [SerializeField, Min(0)] private int _minChildCount;
        [SerializeField] private int _maxChildCount;
        [SerializeField] private SporeFactory _sporeFactory;

        private ISporeRepository _repository;
        
        private void Awake()
        {
            _repository = new SporeRepository();

            _sporeFactory.Init(_repository);   

            ISporePresenter sporePresenter = 
                new SporePresenter(this, _sporeFactory, _repository,_baseDivisionChance,_divisionChanceFactor);
        }

        private void OnValidate()
        {
            if (_maxChildCount < _minChildCount) 
            { 
                _maxChildCount = _minChildCount;
            }
        }

        public List<GameObject> CreateChildren(ISporeView sporeView, float spreadInnerRadius = 3, float spreadOuterRadius = 5)
        {
            Spore sporeInstance = _repository.GetInstance(sporeView);
            IExploder exploder = _repository.GetBehavior(sporeView);
            List<GameObject> children = new();

            int count = UserUtils.GetRandomInt(_minChildCount, _maxChildCount);
            int generation = exploder.Generation + 1;
            Vector3 scale = _sporeFactory.BaseScale * (float)Math.Pow(_scaleFactor, generation);

            for (int i = 0; i < count; i++)
            {
                Vector3 position = UserUtils.GetRandomVector(sporeInstance.transform.position, spreadInnerRadius, spreadOuterRadius);

                children.Add(_sporeFactory.Create(position, scale, Quaternion.identity, generation));
            }

            return children;
        }
    }
}
