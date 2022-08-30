using GalaxyShooter.Combat;
using GalaxyShooter.Control;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GalaxyShooter.Core
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private bool _gameOver;
        private Health _playerHealth;

        private void OnEnable()
        {
            Player player = FindObjectOfType<Player>();
            player.TryGetComponent(out _playerHealth);
            _playerHealth.OnPlayerDeath += OnPlayerDeath;
        }

        private void OnPlayerDeath()
        {
            _gameOver = true;
            _playerHealth.OnPlayerDeath -= OnPlayerDeath;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.R) && _gameOver)
            {
                SceneManager.LoadSceneAsync(1);
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }
        }
    }
}