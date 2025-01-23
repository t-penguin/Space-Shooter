using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController1 : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _waveText;
    [SerializeField] private GameObject _gameOverPanel;
    [SerializeField] private bool _isGameOver;
    [SerializeField] private int _score;

    [Header("Hazard Info")]
    [SerializeField] private List<GameObject> _asteroids;
    [SerializeField] private List<GameObject> _enemies;
    [SerializeField] private List<GameObject> _modifiers;
    [SerializeField] private Vector2 _hazardHorizontalRange;
    [SerializeField] private float _hazardZ;

    [Header("Wave Info")]
    [SerializeField] private float _startDelay;
    [SerializeField] private float _waveInterval;
    [SerializeField] private float _spawnInterval;
    [SerializeField] private int _waveAmount = 10;
    [SerializeField] private int _wave;

    private void Awake()
    {
        if (_asteroids == null || _asteroids.Count == 0)
            Debug.LogWarning("No asteroid prefabs in list, asteroids will not be able to spawn.");

        if (_enemies == null || _enemies.Count == 0)
            Debug.LogWarning("No enemy prefabs in list, enemies will not be able to spawn.");

        if (_modifiers == null || _modifiers.Count == 0)
            Debug.LogWarning("No modifier prefabs in list, modifiers will not be able to spawn.");
    }

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
        // Wait a bit before starting
        yield return new WaitForSeconds(_startDelay);

        // Main spawning loop
        while (true)
        {
            if (_isGameOver)
                yield break;

            IncrementWave();

            // Wave spawning loop
            for (int i = 0; i < _waveAmount; i++)
            {
                float spawnX = Random.Range(_hazardHorizontalRange.x, _hazardHorizontalRange.y);
                SpawnAsteroid(spawnX, 4, 8);

                // Wait a bit in between spawns
                yield return new WaitForSeconds(_spawnInterval);
            }

            // Wait a bit in between waves
            yield return new WaitForSeconds(_waveInterval);
        }
    }

    private void SpawnAsteroid(float posX, float minSpeed, float maxSpeed)
    {
        Quaternion rotation = new Quaternion(0, 180, 0, 1);
        GameObject asteroid = _asteroids[Random.Range(0, _asteroids.Count)];
        asteroid = Spawn(asteroid, posX, rotation);
        asteroid.GetComponent<Mover1>().SetSpeed(minSpeed, maxSpeed);
    }

    private void SpawnEnemy(float posX)
    {
        GameObject enemy = _enemies[Random.Range(0, _enemies.Count)];
        Spawn(enemy, posX, Quaternion.identity);
    }

    private void SpawnModifier(float posX)
    {
        GameObject modifier = _modifiers[Random.Range(0, _modifiers.Count)];
        Spawn(modifier, posX, Quaternion.identity);
    }

    private GameObject Spawn(GameObject prefab, float posX, Quaternion rotation)
    {
        Vector3 position = new Vector3(posX, 0, _hazardZ);
        return Instantiate(prefab, position, rotation);
    }

    private void OnAsteroidDestroyed() => AddScore(10);

    private void OnPlayerDestroyed() => GameOver();

    private void AddScore(int amount)
    {
        _score += amount;
        _scoreText.text = $"Score: {_score}";
    }

    private void IncrementWave()
    {
        _wave++;
        _waveText.text = $"Wave: {_wave}";
    }

    private void GameOver()
    {
        _gameOverPanel.SetActive(true);
        _isGameOver = true;
    }
}
