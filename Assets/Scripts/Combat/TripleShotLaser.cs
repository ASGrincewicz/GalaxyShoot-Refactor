using UnityEngine;

namespace GalaxyShooter.Combat
{
    public class TripleShotLaser : Laser
    {
        protected override void Update()
        {
            _transform.Translate(Vector3.up * (_speed * Time.deltaTime));
            if (_transform.position.y > _destroyBoundary)
            {
                Destroy(gameObject);
                if (_transform.parent != null)
                {
                    Destroy(_transform.parent.gameObject);
                }
            }
        }

        protected override void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Enemy") || other.CompareTag("Asteroid"))
            {
                Destroy(gameObject);
                if (_transform.parent != null)
                {
                    Destroy(_transform.parent.gameObject);
                }
            }
        }
    }
}