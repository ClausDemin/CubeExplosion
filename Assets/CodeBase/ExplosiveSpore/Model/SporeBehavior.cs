using Assets.CodeBase.ExplosiveSpore.Interfaces;
using Assets.Scripts.Utils;
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
        foreach (Rigidbody body in GetExplodableObjects())
        {
            body.AddExplosionForce(_explosionForce, Position, _explosionRadius);
        }
    }

    public void Explode(List<GameObject> gameObjects)
    {
        foreach (Rigidbody body in GetExplodableObjects(gameObjects))
        {
            body.AddExplosionForce(_explosionForce, Position, _explosionRadius);
        }
    }

    private List<Rigidbody> GetExplodableObjects()
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

    private List<Rigidbody> GetExplodableObjects(List<GameObject> gameObjects)
    {
        List<Rigidbody> objects = new List<Rigidbody>();

        foreach (var gameObject in gameObjects)
        {
            if (gameObject.TryGetComponent<Rigidbody>(out var rigidbody))
            {
                if (UserUtils.GetDistanceBetween(rigidbody.transform.position, Position) <= _explosionRadius) 
                {
                    objects.Add(rigidbody);
                }
            }
        }

        return objects;
    }
}
