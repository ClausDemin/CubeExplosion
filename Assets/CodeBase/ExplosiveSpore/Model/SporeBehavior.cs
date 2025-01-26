using Assets.CodeBase.ExplosiveSpore.Interfaces;
using System.Collections.Generic;
using UnityEngine;

public class SporeBehavior : ISporeBehavior
{
    private Transform _bearer;

    private float _explosionRadius;
    private float _explosionForce;

    public SporeBehavior(Transform bearer, float explosionRadius, float explosionForce, int generation) 
    {
        _bearer = bearer;

        _explosionRadius = explosionRadius;
        _explosionForce = explosionForce;

        Generation = generation;
    }

    public int Generation { get; }
    public Vector3 Position => _bearer.transform.position;
    public Vector3 Scale => _bearer.transform.localScale;

    public void Explode()
    {
        foreach (Rigidbody body in GetObjectsInRadius())
        {
            body.AddExplosionForce(_explosionForce, Position, _explosionRadius);
        }
    }

    private List<Rigidbody> GetObjectsInRadius()
    {
        Collider[] hits = Physics.OverlapSphere(Position, _explosionRadius);

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
