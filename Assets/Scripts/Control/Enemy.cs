using GalaxyShooter.Channels;
using UnityEngine;
using UnityEngine.Pool;

namespace GalaxyShooter.Control
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private IntUpdateChannel _scoreUpdateChannel;
        [SerializeField] private UpdateChannel _damageChannel;
        [SerializeField] private int _pointValue = 10;
        [SerializeField] private float _speed = 4f;
        [SerializeField] private float _defaultSpeed = 4.0f;
        private Animator _anim;
        private AudioSource _audio;
        [SerializeField] private AudioClip _explosionClip;

        private readonly int _animatorTrigger = Animator.StringToHash("OnEnemyDeath");
        private IObjectPool<Enemy> _enemyPool;

        private void Start()
        {
            if (!TryGetComponent(out _anim))
            {
                Debug.LogError("Animator is NULL!");
            }

            if (!TryGetComponent(out _audio))
            {
                Debug.LogError("AudioSource is NULL!");
            }
        }

        private void Update()
        {
            Movement();
        }

        public void SetPool(IObjectPool<Enemy> pool)// Using Unity's Pooling API
        {
            _enemyPool = pool;
        }

        private void Movement()
        {
            transform.MoveDown(_speed); //Here is an extension method I create for the Transform Component
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player") || other.CompareTag("Laser")) //Updated way to check tags.
            {
                _anim.SetTrigger(_animatorTrigger);
                _audio.PlayOneShot(_explosionClip);
                _speed = 0;
                if (TryGetComponent(out Collider2D col)) // Updated way to check a component.
                {
                    col.enabled = false;
                }
                _scoreUpdateChannel.CallIntUpdate(_pointValue);
                gameObject.SetActive(false);

                if (other.CompareTag("Player"))
                {
                    _damageChannel.CallUpdate();
                }
            }
        }

        private void OnBecameVisible()
        {
            if (TryGetComponent(out Collider2D col))
            {
                col.enabled = true;
            }

            _speed = _defaultSpeed;
        }

        private void OnBecameInvisible()
        {
            _enemyPool.Release(this);
        }
    }
}