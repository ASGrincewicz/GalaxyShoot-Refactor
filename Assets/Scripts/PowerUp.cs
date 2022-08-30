using System;
using UnityEngine;
using UnityEngine.Pool;

public class PowerUp : MonoBehaviour
{
    [SerializeField] private float _speed = 2.5f;
    [SerializeField] private int _powerUpID;
    [SerializeField] private IntUpdateChannel _powerUpChannel;
    private IObjectPool<PowerUp> _powerUpPool;

    public void SetPool(IObjectPool<PowerUp> pool)
    {
        _powerUpPool = pool;
    }
    private void Update()
    {
       Movement();
    }

    private void Movement()
    {
        transform.MoveDown(_speed);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            _powerUpChannel.CallIntUpdate(_powerUpID);
            gameObject.SetActive(false);
        }
    }

    private void OnBecameInvisible()
    {
        _powerUpPool.Release(this);
        _powerUpPool.Clear();
    }
}
