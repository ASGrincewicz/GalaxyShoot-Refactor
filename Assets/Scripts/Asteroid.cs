using System;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed = 3f;
    [SerializeField] private GameObject _asteroidExplosion;
    public Action OnStartSpawning;

    private void Update()
    {
       transform.Rotate(Vector3.forward * (_rotationSpeed * Time.deltaTime));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Laser"))
        {
            Instantiate(_asteroidExplosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
            OnStartSpawning?.Invoke();
        }
    }
}
