using Assets.CodeBase.ExplosiveSpore.Interfaces;
using Assets.Scripts.Utils;
using System;
using UnityEngine;

namespace Assets.CodeBase.ExplosiveSpore.Presenter
{
    public class SporePresenter : ISporePresenter
    {
        private SporeFactory _factory;
        private ISporeRepository _repository;

        private float _baseDivisionChance;
        private float _baseScale;
        private float _divisionChanceFactor;
        private float _scaleFactor;
        private int _minChildCount;
        private int _maxChildCount;

        public SporePresenter
            (
                SporeFactory factory, 
                ISporeRepository repository, 
                float baseDivisionChance, 
                float baseScale, 
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
                    CreateChildren(sporeView);

                    sporeView.Explode();
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
            sporeView.Clicked += Explode;
        }

        private void CreateChildren(ISporeView sporeView, float spreadInnerRadius = 3, float spreadOuterRadius = 5)
        {
            int count = UserUtils.GetRandomInt(_minChildCount, _maxChildCount);

            ISporeBehavior sporeBehavior = _repository.GetBehavior(sporeView);

            int generation = sporeBehavior.Generation + 1;

            Vector3 scale = sporeView.Scale * (float)(_baseScale * Math.Pow(_scaleFactor, generation));

            for (int i = 0; i < count; i++)
            {
                var position = UserUtils.GetRandomVector(sporeView.Position, spreadInnerRadius, spreadOuterRadius);

                _factory.Create(position, scale, Quaternion.identity, generation);
            }
        }
    }
}
