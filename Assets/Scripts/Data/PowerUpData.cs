using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PowerUpType
{
    Health, Damage, Slowdown
}

[CreateAssetMenu(menuName = "GameData/PowerUpData")]
public class PowerUpData : ScriptableObject
{
    [SerializeField] private PowerUpType _powerType;

    public PowerUpType PowerType => _powerType;
}
