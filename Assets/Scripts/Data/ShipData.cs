using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GameData/PlayerData")]
public class ShipData : ScriptableObject
{
    [SerializeField] private int _lives = 4;
    [SerializeField] private float _shipSpeed = 5f;

    public int Lives => _lives;
    public float ShipSpeed => _shipSpeed;
}
