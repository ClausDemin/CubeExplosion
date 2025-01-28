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
        private SporeFactory _factory;
        private ColorChanger _colorChanger;
        private ISporeRepository _repository;

        private float _baseDivisionChance;
        private Vector3 _baseScale;
        private float _divisionChanceFactor;
        private float _scaleFactor;
        private int _minChildCount;
        private int _maxChildCount;

        public SporePresenter
            (
                SporeFactory factory, 
                ISporeRepository repository, 
                float baseDivisionChance,
                Vector3 baseScale, 
                float divisionChanceFactor,
                float scaleFactor,
                int minChildCount,
                int maxChildCount
            )
        {
            _factory = factory;
            _repository = repository;

            _baseDivisionChance = baseDivisionChance;
            _baseScale = baseScale;
            _divisionChanceFactor = divisionChanceFactor;
            _scaleFactor = scaleFactor;
            _minChildCount = minChildCount;
            _maxChildCount = maxChildCount;

            _colorChanger = new ColorChanger();

            _factory.InstanceCreated += OnInstanceCreated;
        }

        public void Explode(ISporeView sporeView)
        {
            ISporeBehavior sporeBehavior = _repository.GetBehavior(sporeView);

            if (sporeBehavior != null)
            {
                float divideChance = _baseDivisionChance * (float) Math.Pow(_divisionChanceFactor, sporeBehavior.Generation);

                if (IsDivided(divideChance))
                {
                    sporeBehavior.Explode(CreateChildren(sporeView));
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
            GameObject instance = _repository.GetInstance(sporeView);

            if (instance != null)
            {
                if (instance.TryGetComponent<MeshRenderer>(out var renderer))
                {
                    _colorChanger.SetRandomColor(renderer);
                }
            }
        }

        private List<GameObject> CreateChildren(ISporeView sporeView, float spreadInnerRadius = 3, float spreadOuterRadius = 5)
        {
            ISporeBehavior sporeBehavior = _repository.GetBehavior(sporeView);
            List<GameObject> children = new();

            int count = UserUtils.GetRandomInt(_minChildCount, _maxChildCount);
            int generation = sporeBehavior.Generation + 1;
            Vector3 scale =  _baseScale * (float) Math.Pow(_scaleFactor, generation);

            for (int i = 0; i < count; i++)
            {
                Vector3 position = UserUtils.GetRandomVector(sporeBehavior.Position, spreadInnerRadius, spreadOuterRadius);

                children.Add(_factory.Create(position, scale, Quaternion.identity, generation));
            }

            return children;
        }
    }
}
