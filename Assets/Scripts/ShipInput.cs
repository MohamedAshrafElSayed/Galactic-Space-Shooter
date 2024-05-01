using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class ShipInput : MonoBehaviour
{
    private ShipController _controller;
    private Animator _animator;
    private Vector2 _movementVector = Vector2.zero;
    private Rigidbody2D rb;
    private bool _isRespawning = false;
    private int _lives;
    private float _shipSpeed;

    [SerializeField] private TextMeshProUGUI _LivesText;
    [SerializeField] private float _yBoundary, _xBoundary;
    [SerializeField] private Transform spawnPoint;

    public ShipData _shipData;
    public UnityEvent FireEvent;
    public UnityEvent playerDiedEvent;
    public UnityEvent damagePowerUpEvent;
    public UnityEvent slowdownPowerUpEvent;

    private void Awake()
    {
        _controller = new ShipController();
        rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _lives = _shipData.Lives;
        _LivesText.text = "Lives: " + _lives;
        _shipSpeed = _shipData.ShipSpeed;
    }

    private void OnEnable()
    {
        _controller.Enable();
        _controller.Ship.Move.performed += OnMovementPerformed;
        _controller.Ship.Move.canceled += OnMovementCancelled;
        _controller.Ship.Fire.started += OnFireStarted;
    }

    private void OnDisable()
    {
        _controller.Disable();
        _controller.Ship.Move.performed -= OnMovementPerformed;
        _controller.Ship.Move.canceled -= OnMovementCancelled;
        _controller.Ship.Fire.started -= OnFireStarted;
    }
    private void Update()
    {
        PreventPlayerGoingOffScreen();
    }

    private void FixedUpdate()
    {
        if (_controller != null)
        {
            rb.velocity = _movementVector * _shipSpeed;
        }
    }

    private void OnMovementPerformed(InputAction.CallbackContext value)
    {
        _movementVector = value.ReadValue<Vector2>();
    }

    private void OnMovementCancelled(InputAction.CallbackContext value)
    {
        _movementVector = Vector2.zero;
    }

    public void OnFireStarted(InputAction.CallbackContext value)
    {
        if (!_isRespawning)
        {
            AudioManager.instance.Play("Shooting");
            FireEvent.Invoke();
        }
    }

    public void OnFireCancelled(InputAction.CallbackContext value)
    {
        
    }

    private void PreventPlayerGoingOffScreen()
    {
        Vector2 tempPos = transform.position;

        tempPos.x = Mathf.Clamp(tempPos.x, -_xBoundary, _xBoundary);
        tempPos.y = Mathf.Clamp(tempPos.y, -_yBoundary, _yBoundary);

        transform.position = tempPos;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Asteroid") )
        {
            _animator.SetBool("IsDead", true);
            AudioManager.instance.Play("Die");
            TakeDamage();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PowerUp"))
        {
            AudioManager.instance.Play("PowerUp");
            PowerUpType _powerUp = other.GetComponent<PowerUpController>()._powerType;
            CollectPowerUp(_powerUp);
        }
    }

    private void TakeDamage()
    {
        _lives--;
        _LivesText.text = "Lives: " + _lives;

        if (_lives <= 0)
        {
            gameObject.SetActive(false);
            playerDiedEvent.Invoke();
        }
        else
        {
            StartCoroutine(RespawnPlayer());
        }
    }

    private IEnumerator RespawnPlayer()
    {
        _isRespawning = true;
        GetComponent<BoxCollider2D>().isTrigger = true;
        transform.GetChild(0).gameObject.SetActive(false);
        yield return new WaitForSeconds(0.15f);

        transform.position = spawnPoint.position;
        _animator.SetBool("IsDead", false);
        transform.GetChild(0).gameObject.SetActive(true);
        AudioManager.instance.Play("Respawning");
        _animator.SetBool("IsRespawning", true);
        _isRespawning = false;

        yield return new WaitForSeconds(3f);
        _animator.SetBool("IsRespawning", false);
        GetComponent<BoxCollider2D>().isTrigger = false;
    }
    
    private void CollectPowerUp(PowerUpType _powerUp)
    {
        if (_powerUp == PowerUpType.Health && _lives < 4)
        {
            _lives ++;
            _LivesText.text = "Lives: " + _lives;
        }
        if (_powerUp == PowerUpType.Damage)
        {
            damagePowerUpEvent.Invoke();
        }
        if (_powerUp == PowerUpType.Slowdown)
        {
            slowdownPowerUpEvent.Invoke();
        }
    }
}
