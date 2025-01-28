using Assets.CodeBase.ExplosiveSpore.Infrastructure;
using Assets.CodeBase.ExplosiveSpore.Interfaces;
using Assets.CodeBase.ExplosiveSpore.View;
using Assets.Scripts.Utils;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.CodeBase.ExplosiveSpore.Presenter
{
    public class SporePresenter : ISporePresenter
    {
        private SporeSpawner _sporeSpawner;
        private SporeFactory _factory;
        private ColorChanger _colorChanger;
        private ISporeRepository _repository;

        private float _baseDivisionChance;
        private Vector3 _baseScale;
        private float _divisionChanceFactor;
        private float _scaleFactor;
        private int _minChildCount;
        private int _maxChildCount;

        public SporePresenter(SporeSpawner spawner, SporeFactory factory, ISporeRepository repository, float baseDivisionChance,float divisionChanceFactor)
        {
            _sporeSpawner = spawner;
            _factory = factory;
            _repository = repository;

            _baseDivisionChance = baseDivisionChance;
            _divisionChanceFactor = divisionChanceFactor;

            _colorChanger = new ColorChanger();

            _factory.InstanceCreated += OnInstanceCreated;
        }

        public void Explode(ISporeView sporeView)
        {
            IExploder exploder = _repository.GetBehavior(sporeView);
            Spore sporeInstance = _repository.GetInstance(sporeView);

            if (exploder != null)
            {
                float divideChance = _baseDivisionChance * (float) Math.Pow(_divisionChanceFactor, exploder.Generation);

                if (IsDivided(divideChance))
                {
                    exploder.Explode(_sporeSpawner.CreateChildren(sporeView), sporeInstance.transform.position);
                    sporeView.PlayEffects();
                }

                _factory.Destroy(sporeView);
            }
        }

        private bool IsDivided(float divideChance)
        {
            return UserUtils.GetRandomBool(divideChance);
        }

        private void OnInstanceCreated(ISporeView sporeView)
        {
            SetRandomColor(sporeView);

            sporeView.Clicked += Explode;
        }

        private void SetRandomColor(ISporeView sporeView)
        {
            Spore instance = _repository.GetInstance(sporeView);

            if (instance != null)
            {
                if (instance.TryGetComponent<MeshRenderer>(out var renderer))
                {
                    _colorChanger.SetRandomColor(renderer);
                }
            }
        }
    }
}
