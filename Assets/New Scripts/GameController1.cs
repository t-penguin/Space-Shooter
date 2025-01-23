using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController1 : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private GameObject _gameOverPanel;
    [SerializeField] private bool _isGameOver;
    [SerializeField] private int _score;

    [Header("Hazard Info")]
    [SerializeField] private GameObject _hazard;
    [SerializeField] private Vector2 _hazardHorizontalRange;
    [SerializeField] private float _hazardZ;
    [SerializeField] private float _spawnInterval;

    private int waveAmount = 10;
    private void Start()
    {
        StartCoroutine(SpawnHazard());
        _gameOverPanel.SetActive(false);
    }

    private void OnEnable()
    {
        GameEvents.AsteroidDestroyed += OnAsteroidDestroyed;
        GameEvents.PlayerDestroyed += OnPlayerDestroyed;
    }

    private void OnDisable()
    {
        GameEvents.AsteroidDestroyed -= OnAsteroidDestroyed;
        GameEvents.PlayerDestroyed -= OnPlayerDestroyed;
    }

    public void OnRestart()
    {
        if (!_isGameOver)
            return;

        SceneManager.LoadScene(0);
    }

    private IEnumerator SpawnHazard()
    {
        Quaternion spawnRotation = new Quaternion(0, 180, 0, 1);

        while (true)
        {
            float spawnX = Random.Range(_hazardHorizontalRange.x, _hazardHorizontalRange.y);
            Vector3 spawnPosition = new Vector3(spawnX, 0, _hazardZ);
            Instantiate(_hazard, spawnPosition, spawnRotation);
            yield return new WaitForSeconds(_spawnInterval);
        }
    }

    private void OnAsteroidDestroyed() => AddScore(10);

    private void OnPlayerDestroyed() => GameOver();

    private void AddScore(int amount)
    {
        _score += amount;
        _scoreText.text = $"Score: {_score}";
    }

    private void GameOver()
    {
        _gameOverPanel.SetActive(true);
        _isGameOver = true;
    }
}
