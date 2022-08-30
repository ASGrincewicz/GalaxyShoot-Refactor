using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab, _enemyContainer;

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
        _player.OnPlayerDeath -= OnPlayerDeath;
        _asteroid.OnStartSpawning -= StartSpawning;

    }
    private void OnPlayerDeath()
    {
        _stopSpawning = true;
    }

    private void StartSpawning()
    {
        _stopSpawning = false;
        StartCoroutine(SpawnRoutine());
        StartCoroutine(PowerUpRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        while (!_stopSpawning)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-8, 8), 6, 0);
            GameObject newEnemy = Instantiate(_enemyPrefab, posToSpawn, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(5f);
        }
    }

    private IEnumerator PowerUpRoutine()
    {
        while (!_stopSpawning)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-8, 8), 6, 0);
            Instantiate(_powerUps[Random.Range(0,3)], posToSpawn, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(5, 7));
        }
    }
}
