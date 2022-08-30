using GalaxyShooter.Control;
using UnityEngine;
using UnityEngine.Pool;

namespace GalaxyShooter.Spawning
{
    public class EnemySpawner: MonoBehaviour
    {
        [SerializeField] private Enemy _enemyToSpawn;
        [SerializeField] private Transform _enemyContainer;
        [SerializeField] private float _spawnRate;
        [SerializeField] private int _maxPoolSize = 5;
        public IObjectPool<Enemy> enemyPool;
        public float SpawnRate => _spawnRate;

        private void Awake()
        {
           enemyPool = new ObjectPool<Enemy>(CreateEnemy,OnGet,OnRelease,OnEnemyDestroy,maxSize: _maxPoolSize);
        }

        private Enemy CreateEnemy()
        {
            Enemy enemy = Instantiate(_enemyToSpawn, _enemyContainer);
            enemy.SetPool(enemyPool);
            return enemy;
        }

        private void OnGet(Enemy enemy)
        {
            enemy.transform.position =  new Vector3(Random.Range(-8, 8), 6, 0);
            enemy.gameObject.SetActive(true);
        }

        private void OnRelease(Enemy enemy)
        {
            if (enemy.isActiveAndEnabled)
            {
                enemy.gameObject.SetActive(false);
            }
        }

        private void OnEnemyDestroy(Enemy enemy)
        {
            Destroy(enemy.gameObject);
        }
        
    }
}