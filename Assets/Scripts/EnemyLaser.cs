using UnityEngine;

public class EnemyLaser : Laser
{
    [SerializeField] private UpdateChannel _damageChannel;

    protected override void Update()
    {
        //if (!_isVisible) return;
        _transform.Translate(Vector3.down * (_speed * Time.deltaTime));
        if (_transform.position.y < _destroyBoundary)
        {
            gameObject.SetActive(false);
        }
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        _laserPool.Release(this);
        _damageChannel.CallUpdate();
    }
}
