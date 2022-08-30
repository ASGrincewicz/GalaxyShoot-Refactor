using UnityEngine;

public class EnemyLaser : Laser
{
    [SerializeField] private UpdateChannel _damageChannel;

    protected override void Update()
    {
        _transform.MoveDown(_speed);
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
