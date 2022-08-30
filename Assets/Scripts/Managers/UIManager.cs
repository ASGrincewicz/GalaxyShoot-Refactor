using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text scoreText;
    [SerializeField] private Image _livesImg;
    [SerializeField] private Sprite[] _livesSprite = new Sprite[4];
    [SerializeField] private GameObject _gameOverText, _restartText;
    [SerializeField] private IntUpdateChannel _scoreUpdateChannel;
    private int _currentScore;
    private Player _player;
    
    private void OnEnable()
    {
        _player = FindObjectOfType<Player>();
        _player.OnPlayerDamaged += UpdateLives;
        _scoreUpdateChannel.OnIntUpdate += UpdateScore;
    }

    private void OnDisable()
    {
        _player.OnPlayerDamaged -= UpdateLives;
        _scoreUpdateChannel.OnIntUpdate -= UpdateScore;
    }


    private void Start()
    {
        scoreText.text = $"SCORE: {0}";
    }
    private void UpdateScore(int points)
    {
        _currentScore += points;
        scoreText.text = $"SCORE: {_currentScore}";
    }
    private void UpdateLives(int currentLives)
    {
        _livesImg.sprite = _livesSprite[currentLives];
        if (currentLives == 0)
        {
            _restartText.SetActive(true);
            StartCoroutine(GameOverFlicker());
        }
    }

    private IEnumerator GameOverFlicker()
    {
        while (true)
        {
            _gameOverText.SetActive(true);
            yield return new WaitForSeconds(0.75f);
            _gameOverText.SetActive(false);
            yield return new WaitForSeconds(0.75f);
        }
    }
}
