using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpController : MonoBehaviour
{
    public PowerUpData powerUpData;
    [HideInInspector]
    public PowerUpType _powerType;
    [SerializeField] private Transform[] targetPos;

    private Rigidbody2D _rb;

    // Start is called before the first frame update
    void Start()
    {
        InitializePowerUp();
        Destroy(gameObject, 10f);
    }

    private void InitializePowerUp()
    {
        _powerType = powerUpData.PowerType;
        _rb = GetComponent<Rigidbody2D>();
        _rb.angularVelocity = Random.Range(-180f, 180f);

        Transform randomTarget = GetRandomTarget();

        if (randomTarget != null)
        {
            Vector2 direction = (randomTarget.position - transform.position).normalized;
            _rb.velocity = direction * 4f;
        }
        else
        {
            _rb.velocity = new Vector2(-4f, 0f);
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
