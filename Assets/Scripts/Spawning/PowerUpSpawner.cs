using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace Spawning
{
    public class PowerUpSpawner: MonoBehaviour
    {
        [SerializeField] private List<PowerUp> _availablePowerUps = new List<PowerUp>();
            [SerializeField] private PowerUp _powerUpToSpawn;
            [SerializeField] private Transform _powerUpContainer;
            [SerializeField] private float _spawnRate;
            [SerializeField] private int _maxPoolSize = 5;
            public IObjectPool<PowerUp> powerUpPool;
            public float SpawnRate => _spawnRate;

            private void Awake()
            {
                powerUpPool = new ObjectPool<PowerUp>(CreatePowerUp,OnGet,OnRelease,OnPowerUpDestroy,maxSize: _maxPoolSize);
            }

            private PowerUp CreatePowerUp()
            {
                _powerUpToSpawn = _availablePowerUps[Random.Range(0, _availablePowerUps.Count)];
                PowerUp powerUp = Instantiate(_powerUpToSpawn, _powerUpContainer);
                powerUp.SetPool(powerUpPool);
                return powerUp;
            }

            private void OnGet(PowerUp powerUp)
            {
                powerUp.transform.position =  new Vector3(Random.Range(-8, 8), 6, 0);
                powerUp.gameObject.SetActive(true);
            }

            private void OnRelease(PowerUp powerUp)
            {
                if (powerUp.isActiveAndEnabled)
                {
                    powerUp.gameObject.SetActive(false);
                }
            }

            private void OnPowerUpDestroy(PowerUp powerUp)
            {
                Destroy(powerUp.gameObject);
            }
        
        }
    }