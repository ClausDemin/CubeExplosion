using Assets.CodeBase.ExplosiveSpore.Interfaces;
using Assets.CodeBase.ExplosiveSpore.View;
using System;
using UnityEngine;

public class Spore : MonoBehaviour, ISporeView
{
    [SerializeField] private ExplosionVFX _explosionVFX;

    public event Action<ISporeView> Clicked;

    [field: SerializeField] public float ExplosionRadius { get; private set; }
    [field: SerializeField] public float ExplosionForce { get; private set; }

    private void OnMouseUpAsButton()
    {
        Clicked?.Invoke(this);
    }

    public void PlayEffects() 
    {
        Instantiate(_explosionVFX, transform.position, Quaternion.identity);
    }
}
