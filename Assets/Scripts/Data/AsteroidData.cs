using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AsteroidTypes
{
    Huge, Large, Big, Medium, Small
}

[CreateAssetMenu(menuName = "GameData/AsteroidData")]
public class AsteroidData : ScriptableObject
{
    [SerializeField] private AsteroidTypes _asteroidType;
    [SerializeField] private int _health;
    [SerializeField] private float _asteroidSpeed;

    public AsteroidTypes AsteroidType => _asteroidType;
    public int Health => _health;
    public float AsteroidSpeed => _asteroidSpeed;
}
