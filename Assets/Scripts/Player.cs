using System;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5;
    private float _vBoundTop = 0f;
    private float _vBoundBottom = -4f;
    private float _hBoundRight = 9.3f;
    private float _hBoundLeft = -9.3f;
    [SerializeField] private GameObject _laserPrefab, _tripleShotPrefab, _shield;
    [SerializeField] private Vector3 _laserOffset = new Vector3(0,0.5f,0);
    [SerializeField] private Vector3 _tripleShotOffset = new Vector3(0, -0.5f, 0);
    [SerializeField] private float _fireRate = 0.5f;
    private float _canFire = -1.0f;
    [SerializeField] private int _lives = 3, _fireMode;
    [SerializeField] private bool _isShieldActive;
    [SerializeField] [Header("SCORE")] private int _playerScore;
    [SerializeField] private GameObject _rightDamage, _leftDamage, _explosion;
    [SerializeField] private AudioClip _laserClip, _powerUpClip, _explosionClip;
    [Header("Channels"), SerializeField] private IntUpdateChannel _scoreUpdateChannel;
    [SerializeField] private IntUpdateChannel _powerUpChannel;
    [SerializeField] private UpdateChannel _damageChannel;
    private AudioSource _audio;
    private Transform _transform;
    private float _deltaTime;
    private readonly float _powerUpCooldownTime = 5.0f;
    private WaitForSeconds _powerUpDelay;
    //Actions
    public Action OnPlayerDeath;
    public Action<int> OnPlayerDamaged;
   
    private void OnEnable()
    {
        _powerUpChannel.OnIntUpdate += PowerUp;
        _damageChannel.OnUpdate += Damage;
    }

    private void OnDisable()
    {
        _powerUpChannel.OnIntUpdate -= PowerUp;
        _damageChannel.OnUpdate -= Damage;
    }

    private void Start()
    {
        _transform = transform;
        _transform.position = new Vector3(0, -4, 0);
        TryGetComponent(out _audio);
        _powerUpDelay = new WaitForSeconds(_powerUpCooldownTime);
    }

    private void Update()
    {
        _deltaTime = Time.deltaTime;
        Movement();
        if(Input.GetKeyDown(KeyCode.Space)&& Time.time > _canFire)
        {
            Shoot();
        }
    }

    private void Movement()
    {
        float hInput = Input.GetAxis("Horizontal");
        float vInput = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(hInput, vInput, 0);
        _transform.Translate(direction * (_speed * _deltaTime));
        ScreenWrap();
    }

    private void Shoot()
    {
        _canFire = Time.time + _fireRate;
        switch(_fireMode)
        {
            case 0:
                Instantiate(_laserPrefab, _transform.position + _laserOffset, Quaternion.identity);
                break;
            case 1:
                Instantiate(_tripleShotPrefab, _transform.position + _tripleShotOffset, Quaternion.identity);
                break;
        }
        _audio.PlayOneShot(_laserClip, 10);
    }

    private void Damage()
    {
        if (!_isShieldActive)
        {
            _lives--;
            switch(_lives)
            {
                case 0:
                    _audio.PlayOneShot(_explosionClip);
                    Instantiate(_explosion, _transform.position, Quaternion.identity);
                    OnPlayerDeath?.Invoke();
                    Destroy(gameObject);
                    //SpawnManager.Instance.OnPlayerDeath();
                    break;
                case 1:
                    _leftDamage.SetActive(true);
                    break;
                case 2:
                    _rightDamage.SetActive(true);
                    break;

            }
            OnPlayerDamaged?.Invoke(_lives);
        }
        else if(_isShieldActive)
        {
            _isShieldActive = false;
            _shield.SetActive(false);
        }
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
                _isShieldActive = true;
                _shield.SetActive(true);
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
           _transform.position = new Vector3(_hBoundRight,_transform.position.y, 0);
        }
        _transform.position = new Vector3(_transform.position.x, Mathf.Clamp(_transform.position.y, _vBoundBottom, _vBoundTop));

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
