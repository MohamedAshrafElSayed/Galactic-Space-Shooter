using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AsteroidController : MonoBehaviour
{
    private AsteroidTypes _asteroidType;
    private int _health;
    private float _asteroidSpeed ;
    private float _currentAsteroidSpeed;
    private Rigidbody2D _rb;
    private Animator _animator;

    [SerializeField] private Transform[] targetPos;

    public AsteroidData asteroidData;
    public UnityEvent ScoreEvent;

    private void Start()
    {
        InitializeAsteroid();
        Destroy(gameObject,3f);
    }

    private void InitializeAsteroid()
    {
        _asteroidType = asteroidData.AsteroidType;
        _health = asteroidData.Health;
        _asteroidSpeed = asteroidData.AsteroidSpeed;
        _currentAsteroidSpeed = _asteroidSpeed;

        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
        _rb.angularVelocity = Random.Range(-180f, 180f);

        Transform randomTarget = GetRandomTarget();

        if (randomTarget != null)
        {
            Vector2 direction = (randomTarget.position - transform.position).normalized;
            Debug.DrawRay(transform.position, direction * 5f, Color.red, 2f);
            _rb.velocity = direction * _currentAsteroidSpeed;
        }
        else
        {
            _rb.velocity = new Vector2(-_currentAsteroidSpeed, 0f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            BulletData bullet = collision.gameObject.GetComponent<BulletController>().bulletData;
            TakeDamage(bullet.Damage);
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            DestroyAsteroid();
        }
    }

    private Transform GetRandomTarget()
    {
        if (targetPos.Length > 0)
        {
            int randomIndex = Random.Range(0, targetPos.Length);
            return targetPos[randomIndex];
        }
        return null;
    }

    private void TakeDamage(int damage)
    {
        _health -= damage;

        if (_health <= 0)
        {
            ScoreEvent.Invoke();
            DestroyAsteroid();
        }
    }

    public void ApplySlowDown()
    {
        if(gameObject.activeSelf)
        {
            StartCoroutine(SlowDownCoroutine());
        }
    }

    private IEnumerator SlowDownCoroutine()
    {
        _currentAsteroidSpeed = 3f;
        yield return new WaitForSeconds(3f);
        _currentAsteroidSpeed = _asteroidSpeed;
    }

    private void DestroyAsteroid()
    {
        _animator.SetBool("IsDead", true);
        AudioManager.instance.Play("Asteroid");
        Destroy(gameObject,0.15f);
    }
}
