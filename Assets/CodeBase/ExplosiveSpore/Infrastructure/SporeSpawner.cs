using Assets.CodeBase.ExplosiveSpore.Interfaces;
using Assets.CodeBase.ExplosiveSpore.Presenter;
using System;
using UnityEngine;
namespace Assets.CodeBase.ExplosiveSpore.Infrastructure
{
    public class SporeSpawner: MonoBehaviour
    {
        [SerializeField, Range(0, 1)] private float _baseDivisionChance;
        [SerializeField] private float _divisionChanceFactor;
        [SerializeField] private float _scaleFactor;
        [SerializeField] private int _minChildCount;
        [SerializeField] private int _maxChildCount;

        [SerializeField] private SporeFactory _sporeFactory;


        private void Awake()
        {
            ISporeRepository sporeRepository = new SporeRepository();

            _sporeFactory.Init(sporeRepository);   

            ISporePresenter sporePresenter = 
                new SporePresenter
                (
                    _sporeFactory, 
                    sporeRepository,
                    _baseDivisionChance,
                    _sporeFactory.BaseScale,
                    _divisionChanceFactor,
                    _scaleFactor,
                    _minChildCount,
                    _maxChildCount
                );
        }
    }
}
