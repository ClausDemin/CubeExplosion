using Assets.CodeBase.ExplosiveSpore.Interfaces;
using System;
using System.Collections.Generic;
using UnityEngine;

public class SporeExplosion : MonoBehaviour, ISporeView
{
    [SerializeField] private GameObject _explosionVFX;

    [SerializeField] private float _explosionRadius;
    [SerializeField] private float _explosionForce;

    public event Action<ISporeView> Clicked;
    public event Action<GameObject> Exploded;

    public Vector3 Position => transform.position;
    public Vector3 Scale => transform.localScale;

    private void OnMouseUpAsButton()
    {
        Clicked?.Invoke(this);
    }
    
    public void Explode() 
    {
        Instantiate(_explosionVFX, transform.position, Quaternion.identity);

        foreach (Rigidbody body in GetObjectsInRadius()) 
        {
            body.AddExplosionForce(_explosionForce, transform.position, _explosionRadius);
        }

        Exploded?.Invoke(gameObject);
    }

    private List<Rigidbody> GetObjectsInRadius()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, _explosionRadius);

        List<Rigidbody> objects = new List<Rigidbody>();

        foreach (Collider hit in hits) 
        {
            if (hit.attachedRigidbody != null) 
            { 
                objects.Add(hit.attachedRigidbody);
            }
        }

        return objects;
    }
}
