using System;
using GalaxyShooter.Channels;
using UnityEngine;

namespace GalaxyShooter.Combat
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private GameObject _shield;
        [SerializeField] private int _lives = 3;
        [SerializeField] private bool _isShieldActive;
        [SerializeField] private GameObject _rightDamage, _leftDamage, _explosion;
        [SerializeField] private UpdateChannel _damageChannel;
        
        //Actions
        public Action OnPlayerDeath;
        public Action<int> OnPlayerDamaged;
        
        private void OnEnable()
        {
            _damageChannel.OnUpdate += Damage;
        }

        private void OnDisable()
        {
            _damageChannel.OnUpdate -= Damage;
        }

        public void SetShields(bool isOn)
        {
            _isShieldActive = isOn;
            _shield.SetActive(true);
        }
        public void Damage()
        {
            if (!_isShieldActive)
            {
                _lives--;
                switch (_lives)
                {
                    case 0:
                       // _audio.PlayOneShot(_explosionClip);
                        Instantiate(_explosion, transform.position, Quaternion.identity);
                        OnPlayerDeath?.Invoke();
                        Destroy(gameObject);
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
            else if (_isShieldActive)
            {
                _isShieldActive = false;
                _shield.SetActive(false);
            }
        }
    }
}