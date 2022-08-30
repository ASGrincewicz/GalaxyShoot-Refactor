using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] protected float _speed = 8f;
    
    [SerializeField] protected float _destroyBoundary;

    protected Transform _transform;

    private void Start()
    {
        _transform = transform;
    }

    protected virtual void Update()
    {
        _transform.Translate(Vector3.up * (_speed * Time.deltaTime));
        if(_transform.position.y > _destroyBoundary)
        {
            Destroy(gameObject);
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Enemy")|| other.CompareTag("Asteroid"))
        {
            Destroy(gameObject);
        }
    }
}