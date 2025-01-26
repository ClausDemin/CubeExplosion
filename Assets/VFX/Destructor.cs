using System.Collections;
using UnityEngine;

public class Destructor : MonoBehaviour
{
    [SerializeField] private float _lifetime;

    private void Start()
    {
        StartCoroutine(DestroyOverTime());
    }

    private IEnumerator DestroyOverTime() 
    {
        yield return new WaitForSeconds(_lifetime);

        Destroy(gameObject);
    }
}
