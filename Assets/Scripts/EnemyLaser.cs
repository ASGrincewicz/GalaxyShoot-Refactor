using UnityEngine;

public class EnemyLaser : Laser
{
    [SerializeField] private UpdateChannel _damageChannel;

    protected override void Update()
    {
        _transform.Translate(Vector3.down * (_speed * Time.deltaTime));
        if (_transform.position.y < _destroyBoundary)
        {
            Destroy(gameObject);
        }
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
           
            _damageChannel.CallUpdate();
        }
    }
}
