using UnityEngine;
using UnityEngine.Pool;

public class Laser : MonoBehaviour
{
    [SerializeField] protected float _speed = 8f;
    
    [SerializeField] protected float _destroyBoundary;

    protected Transform _transform;

    protected IObjectPool<Laser> _laserPool;

    protected bool _isVisible = false;

    private void Start()
    {
        _transform = transform;
    }

    protected virtual void Update()
    {
        _transform.Translate(Vector3.up * (_speed * Time.deltaTime));
        if(_transform.position.y > _destroyBoundary)
        {
            gameObject.SetActive(false);
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Enemy")|| other.CompareTag("Asteroid"))
        {
            gameObject.SetActive(false);
        }
    }

    public void SetPool(IObjectPool<Laser> pool)
    {
        _laserPool = pool;S
    }

    protected void OnBecameVisible()
    {
        _isVisible = true;
    }

    protected void OnBecameInvisible()
    {
        _isVisible = false;
       _laserPool.Release(this);
    }
}