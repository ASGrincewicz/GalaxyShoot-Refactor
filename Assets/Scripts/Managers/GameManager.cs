using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private bool _gameOver;
    private Player _player;

    private void OnEnable()
    {
        _player = FindObjectOfType<Player>();
        _player.OnPlayerDeath += OnPlayerDeath;
    }

    private void OnDisable()
    {
        _player.OnPlayerDeath -= OnPlayerDeath;
    }

    private void OnPlayerDeath()
    {
        _gameOver = true;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R)&& _gameOver)
        {
            SceneManager.LoadSceneAsync(1);
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
