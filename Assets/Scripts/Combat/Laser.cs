using UnityEngine;
using UnityEngine.Pool;

namespace GalaxyShooter.Combat
{
    public class Laser : MonoBehaviour
    {
        [SerializeField] protected float _speed = 8f;

        [SerializeField] protected float _destroyBoundary;

        protected Transform _transform;

        protected IObjectPool<Laser> _laserPool;

        private void Start()
        {
            _transform = transform;
        }

        protected virtual void Update()
        {
            _transform.MoveUp(_speed);
            if (_transform.position.y > _destroyBoundary)
            {
                gameObject.SetActive(false);
            }
        }

        protected virtual void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Player"))
            {
                gameObject.SetActive(false);
            }
        }

        public void SetPool(IObjectPool<Laser> pool)
        {
            _laserPool = pool;
        }

        protected void OnBecameInvisible()
        {
            _laserPool.Release(this);
        }
    }
}