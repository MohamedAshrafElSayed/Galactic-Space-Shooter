using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private GameObject[] _bullets;
    private BulletData _bulletPrefab;
    private int _currentIndex = 0;
    private float _bulletSpeed;

    private void Start()
    {
        _bulletPrefab = _bullets[_currentIndex].GetComponent<BulletController>().bulletData;
        _bulletSpeed = _bulletPrefab.BulletSpeed;
    }
    public void IncreaseBulletIndex()
    {
        _currentIndex = Mathf.Clamp(_currentIndex + 1, 0, _bullets.Length - 1);
    }

    public void OnBulletFired()
    {
        GameObject bullet = (GameObject)Instantiate(_bullets[_currentIndex], transform.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = _bulletSpeed * (-transform.up);
        Destroy(bullet.gameObject, 1f);
    }
}
