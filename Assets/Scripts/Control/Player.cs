using System.Collections;
using GalaxyShooter.Channels;
using GalaxyShooter.Combat;
using UnityEngine;

namespace GalaxyShooter.Control
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private float _speed = 5;
        [SerializeField] private AudioClip _laserClip;
        [SerializeField] private AudioClip _powerUpClip;
        [SerializeField] private AudioClip _explosionClip;
        [SerializeField] private IntUpdateChannel _powerUpChannel;
        private int _fireMode;
        public int FireMode => _fireMode;
        private Transform _transform;
        private AudioSource _audio;
        private Animator _animator;
        private readonly int _animatorHInputParameter = Animator.StringToHash("horizontalInput");
        private readonly float _powerUpCooldownTime = 5.0f;
        private const float _vBoundTop = 0f;
        private const float _vBoundBottom = -4f;
        private const float _hBoundRight = 9.3f;
        private const float _hBoundLeft = -9.3f;
        private float _deltaTime;
        private WaitForSeconds _powerUpDelay;

        private void OnEnable()
        {
            _powerUpChannel.OnIntUpdate += PowerUp;
        }

        private void OnDisable()
        {
            _powerUpChannel.OnIntUpdate -= PowerUp;
        }

        private void Start()
        {
            _transform = transform;
            _transform.position = new Vector3(0, -4, 0);
            _powerUpDelay = new WaitForSeconds(_powerUpCooldownTime);
            if(!TryGetComponent(out _audio))
            {
               Debug.LogError("Audio source not found."); 
            }

            if (!TryGetComponent(out _animator))
            {
                Debug.LogError("Animator not found.");
            }
        }

        private void Update()
        {
            _deltaTime = Time.deltaTime;
            Movement();
        }

        private void Movement()
        {
            float hInput = Input.GetAxis("Horizontal");
            float vInput = Input.GetAxis("Vertical");
            Vector3 direction = new Vector3(hInput, vInput, 0);
            _transform.Translate(direction * (_speed * _deltaTime));
            _animator.SetFloat(_animatorHInputParameter, hInput);
            ScreenWrap();
        }

        public void SetFireMode(int fireMode)
        {
            _fireMode = fireMode;
        }

        private void PowerUp(int powerUpID)
        {
            _audio.PlayOneShot(_powerUpClip);
            switch (powerUpID)
            {
                case 0:
                    _fireMode = 1;
                    StartCoroutine(PowerUpCooldown(0));
                    break;
                case 1:
                    _speed += 2;
                    StartCoroutine(PowerUpCooldown(1));
                    break;
                case 2:
                    TryGetComponent(out Health health);
                    health.SetShields(true);
                    break;
                default:
                    Debug.Log("Default Power Up ID");
                    break;
            }
        }

        private void ScreenWrap()
        {
            if (_transform.position.x > _hBoundRight)
            {
                _transform.position = new Vector3(_hBoundLeft, _transform.position.y, 0);
            }
            else if (_transform.position.x < _hBoundLeft)
            {
                _transform.position = new Vector3(_hBoundRight, _transform.position.y, 0);
            }

            _transform.position = new Vector3(_transform.position.x,
                Mathf.Clamp(_transform.position.y, _vBoundBottom, _vBoundTop));

        }

        private IEnumerator PowerUpCooldown(int powerUpID)
        {
            switch (powerUpID)
            {
                case 0:
                    yield return _powerUpDelay;
                    _fireMode = 0;
                    break;
                case 1:
                    yield return _powerUpDelay;
                    _speed -= 2;
                    break;
                default:
                    Debug.Log("Default Power Up ID");
                    break;
            }
        }
    }
}
