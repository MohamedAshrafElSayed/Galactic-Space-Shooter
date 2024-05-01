using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GameData/WaveData")]
public class WaveData : ScriptableObject
{
    [SerializeField] private GameObject[] _powerUps;
    [SerializeField] private GameObject[] _asteroids;
    [SerializeField] private float _waveDuration;
    [SerializeField] private int _powerUpCount;
    [SerializeField] private float _spawnRate;

    public GameObject[] PowerUps => _powerUps;
    public GameObject[] Asteroids => _asteroids;
    public float WaveDuration => _waveDuration;
    public float SpawnRate => _spawnRate;
    public int PowerUpCount => _powerUpCount;
}