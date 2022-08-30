using System.Collections;
using GalaxyShooter.Combat;
using GalaxyShooter.Control;
using UnityEngine;

namespace GalaxyShooter.Spawning
{
    public class SpawnManager : MonoBehaviour
    {
        [SerializeField] private EnemySpawner _enemySpawner;
        [SerializeField] private PowerUpSpawner _powerUpSpawner;
        [SerializeField] private bool _stopSpawning = true;
        [SerializeField] private GameObject[] _powerUps = new GameObject[3];
        private Asteroid _asteroid;
        private Health _playerHealth;

        private void OnEnable()
        {
            Player player = FindObjectOfType<Player>();
            player.TryGetComponent(out _playerHealth);
            _playerHealth.OnPlayerDeath += OnPlayerDeath;
            _asteroid = FindObjectOfType<Asteroid>();
            _asteroid.OnStartSpawning += StartSpawning;
        }

        private void OnDisable()
        {
            StopAllCoroutines();
            _asteroid.OnStartSpawning -= StartSpawning;

        }

        private void OnPlayerDeath()
        {
            _stopSpawning = true;
            StopAllCoroutines();
            _playerHealth.OnPlayerDeath -= OnPlayerDeath;
        }

        private void StartSpawning()
        {
            if (_stopSpawning)
            {
                _stopSpawning = false;
                StartCoroutine(SpawnRoutine());
                StartCoroutine(PowerUpRoutine());
            }
        }

        private IEnumerator SpawnRoutine()
        {
            while (!_stopSpawning)
            {
                _enemySpawner.enemyPool.Get();
                yield return new WaitForSeconds(_enemySpawner.SpawnRate);
            }
        }

        private IEnumerator PowerUpRoutine()
        {
            while (!_stopSpawning)
            {
                _powerUpSpawner.powerUpPool.Get();
                yield return new WaitForSeconds(_powerUpSpawner.SpawnRate);
            }
        }
    }
}
