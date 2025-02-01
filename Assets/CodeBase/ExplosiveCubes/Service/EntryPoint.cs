using Assets.CodeBase.ExplosiveCubes.Model.Infrastructure;
using Assets.CodeBase.ExplosiveCubes.Presenter.Infrastructure;
using Assets.CodeBase.ExplosiveCubes.View;
using UnityEngine;

namespace Assets.CodeBase.ExplosiveCubes.Service
{
    public class EntryPoint: MonoBehaviour
    {
        [SerializeField, Range(0, 1)] private float _baseSeparateChance;
        [SerializeField, Range(0, 1)] private float _separateOverGenerationFactor;
        [SerializeField, Range(0, 1)] private float _scaleOverGenerationFactor;
        [SerializeField, Min(1)] private int _minChildCount;
        [SerializeField] private int _maxChildCount;
        [SerializeField, Min(0)] private int _initialGeneration;

        [SerializeField] private ExplosiveObjectsFactory _explosiveObjectsFactory;

        private SeparableEntityFactory _entityFactory;
        private ExplosiveObjectPresenterFactory _explosiveObjectPresenterFactory;

        private void Awake()
        {
            _entityFactory = new SeparableEntityFactory(_baseSeparateChance, _separateOverGenerationFactor, _minChildCount, _maxChildCount);
            _explosiveObjectsFactory.Init(_entityFactory, _scaleOverGenerationFactor, _minChildCount, _maxChildCount, _initialGeneration);
        }

        private void OnValidate()
        {
            ValidateChildCount();
        }

        private void ValidateChildCount()
        {
            if (_minChildCount > _maxChildCount)
            {
                _maxChildCount = _minChildCount;
            }
        }
    }
}
