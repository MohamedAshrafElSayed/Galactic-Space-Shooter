using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public BulletData bulletData;

    private BulletTypes _bulletType;
    private int _damage;
    private float _bulletSpeed;

    private void Start()
    {
        _bulletType = bulletData.BulletType;
        _damage = bulletData.Damage;
        _bulletSpeed = bulletData.BulletSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Asteroid"))
        {
            Destroy(gameObject);
        }
    }
}