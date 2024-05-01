using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private WaveData[] _waves;
    [SerializeField] private AchievementData[] _achievement;
    [SerializeField] private List<Transform> _SpawnPoints;
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _waveNumberText;
    [SerializeField] private GameObject _EndGamePanel;
    private int _currentWaveIndex = 0;
    private GameObject _powerUpPrefab;
    private GameObject _asteroidPrefab;
    private int _score = 0;

    private void Start()
    {
        _scoreText.text = "Score: " + _score;
        AudioManager.instance.Play("Background");
        StartCoroutine(SpawnWaves());
    }

    private void Update()
    {
        foreach (AchievementData achievement in _achievement)
        {
            if (_score == achievement.AchievementScore)
            {
                _waveNumberText.text = achievement.AchievementName.ToString();
                AudioManager.instance.Play("Achievement");
            }
        }
    }

    private IEnumerator SpawnWaves()
    {
        while (_currentWaveIndex < _waves.Length)
        {
            int _wavesNumber = _currentWaveIndex + 1;
            _waveNumberText.text = "Wave " + _wavesNumber.ToString();
            WaveData currentWave = _waves[_currentWaveIndex];
            int i = 0;
            float elapsedTime = 0f;
            while (elapsedTime < currentWave.WaveDuration)
            {
                SpawnAsteroid();
                yield return new WaitForSeconds(2f / currentWave.SpawnRate);
                _waveNumberText.text = "";
                if (i < currentWave.PowerUpCount)
                {
                    SpawnPowerUp();
                    i++;
                    yield return new WaitForSeconds(40f / currentWave.SpawnRate);
                }
                elapsedTime += 2f / currentWave.SpawnRate;
            }

            yield return new WaitForSeconds(currentWave.WaveDuration - elapsedTime);
            yield return new WaitForSeconds(5f);
            _currentWaveIndex++;
        }
        EndGame();
    }
    
    private void SpawnAsteroid()
    {
        WaveData currentWave = _waves[_currentWaveIndex];
        int _randomTypeIndex = Random.Range(0, currentWave.Asteroids.Length);
        
        _asteroidPrefab = currentWave.Asteroids[_randomTypeIndex];
        Transform spawnPoint = GetRandomSpawnPoint(_SpawnPoints);
        Instantiate(_asteroidPrefab, spawnPoint.position, Quaternion.identity);
    }

    private void SpawnPowerUp()
    {
        WaveData currentWave = _waves[_currentWaveIndex];
        int _randomTypeIndex = Random.Range(0, currentWave.PowerUps.Length);

        _powerUpPrefab = currentWave.PowerUps[_randomTypeIndex];
        Transform spawnPoint = GetRandomSpawnPoint(_SpawnPoints);
        Instantiate(_powerUpPrefab, spawnPoint.position, Quaternion.identity);
    }

    private Transform GetRandomSpawnPoint(List<Transform> spawnPoints)
    {
        if (spawnPoints.Count > 0)
        {
            int randomIndex = Random.Range(0, spawnPoints.Count);
            return spawnPoints[randomIndex];
        }
        return null;
    }

    public void IncreaseScore()
    {
        _score += 1;
        _scoreText.text = "Score: " + _score;
    }

    public void EndGame()
    {
        _EndGamePanel.SetActive(true);
        AudioManager.instance.Stop("Background");
        AudioManager.instance.Play("GameOver");
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
