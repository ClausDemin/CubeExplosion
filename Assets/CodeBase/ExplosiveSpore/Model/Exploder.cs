using Assets.CodeBase.ExplosiveSpore.Interfaces;
using Assets.Scripts.Utils;
using System.Collections.Generic;
using UnityEngine;

public class Exploder : IExploder
{
    private float _explosionRadius;
    private float _explosionForce;

    public Exploder(float explosionRadius, float explosionForce, int generation) 
    {
        _explosionRadius = explosionRadius;
        _explosionForce = explosionForce;

        Generation = generation;
    }

    public int Generation { get; }

    public void Explode(Vector3 position)
    {
        foreach (Rigidbody body in GetExplodableObjects(position))
        {
            body.AddExplosionForce(_explosionForce, position, _explosionRadius);
        }
    }

    public void Explode(List<GameObject> gameObjects, Vector3 position)
    {
        foreach (Rigidbody body in GetExplodableObjects(gameObjects, position))
        {
            body.AddExplosionForce(_explosionForce, position, _explosionRadius);
        }
    }

    private List<Rigidbody> GetExplodableObjects(Vector3 position)
    {
        Collider[] hits = Physics.OverlapSphere(position, _explosionRadius);

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

    private List<Rigidbody> GetExplodableObjects(List<GameObject> gameObjects, Vector3 position)
    {
        List<Rigidbody> objects = new List<Rigidbody>();

        foreach (var gameObject in gameObjects)
        {
            if (gameObject.TryGetComponent<Rigidbody>(out var rigidbody))
            {
                if (UserUtils.GetDistanceBetween(rigidbody.transform.position, position) <= _explosionRadius) 
                {
                    objects.Add(rigidbody);
                }
            }
        }

        return objects;
    }
}
