using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BulletTypes
{
    Bolt, Crossed, Wave, Charged
}

[CreateAssetMenu(menuName = "GameData/BulletData")]
public class BulletData : ScriptableObject
{
    [SerializeField] private BulletTypes _bulletType;
    [SerializeField] private int _damage;
    [SerializeField] private float _bulletSpeed;

    public BulletTypes BulletType => _bulletType;
    public int Damage => _damage;
    public float BulletSpeed => _bulletSpeed;
}
