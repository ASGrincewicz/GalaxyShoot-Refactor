using System.Collections;
using Spawning;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private PowerUpSpawner _powerUpSpawner;
    [SerializeField] private bool _stopSpawning = true;
    [SerializeField] private GameObject[] _powerUps = new GameObject[3];
    private Asteroid _asteroid;
    private Player _player;

    private void OnEnable()
    {
        _player = FindObjectOfType<Player>();
        _player.OnPlayerDeath += OnPlayerDeath;
        _asteroid = FindObjectOfType<Asteroid>();
        _asteroid.OnStartSpawning += StartSpawning;
    }

    private void OnDisable()
    {
        StopAllCoroutines();
        _player.OnPlayerDeath -= OnPlayerDeath;
        _asteroid.OnStartSpawning -= StartSpawning;

    }
    private void OnPlayerDeath()
    {
        _stopSpawning = true;
        StopAllCoroutines();
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
