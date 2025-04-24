using UnityEngine;
using System;

public class Boss : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float _baseSpeed = 3f;
    [SerializeField] private float _speedIncreasePerHit = 2f;
    [SerializeField] private float _rotationSpeed = 100f;
    [SerializeField] private float _bounceAngleVariation = 30f;
    [SerializeField] private int _hitsToDefeat = 8;

    private Rigidbody2D _rb;
    private int _currentHits;
    private Vector2 _currentDirection;
    private Camera _mainCamera;
    [SerializeField] private float _currentSpeed;

    public static Action Win;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _mainCamera = Camera.main;
        _currentDirection = Vector2.down;
        _currentSpeed = _baseSpeed;
    }

    private void Start()
    {
        // Начальное движение
        _rb.velocity = _currentDirection * _currentSpeed;
        _rb.angularVelocity = _rotationSpeed;
    }

    private void FixedUpdate()
    {
        CheckBoundaries();
    }

    private void CheckBoundaries()
    {
        Vector2 screenPos = _mainCamera.WorldToViewportPoint(transform.position);
        bool bounced = false;

        // Левая/правая граница
        if (screenPos.x < 0.05f || screenPos.x > 0.95f)
        {
            _currentDirection = Vector2.Reflect(_currentDirection, Vector2.right);
            bounced = true;
        }

        // Верхняя граница
        if (screenPos.y > 0.95f)
        {
            _currentDirection = Vector2.Reflect(_currentDirection, Vector2.up);
            bounced = true;
        }

        if (bounced)
        {
            ApplyMovementWithVariation();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            HandlePlayerCollision(collision);
        }
    }

    private void HandlePlayerCollision(Collider2D collision)
    {
        // Увеличиваем скорость после удара
        _currentSpeed += _speedIncreasePerHit;

        // Новое направление с учетом удара
        _currentDirection = (transform.position - collision.transform.position).normalized;
        ApplyMovementWithVariation();

        _currentHits++;
        if (_currentHits >= _hitsToDefeat)
        {
            DefeatBoss();
        }
    }

    private void ApplyMovementWithVariation()
    {
        // Добавляем случайный угол
        float randomAngle = UnityEngine.Random.Range(-_bounceAngleVariation, _bounceAngleVariation);
        _currentDirection = Quaternion.Euler(0, 0, randomAngle) * _currentDirection;

        // Применяем движение с текущей скоростью
        _rb.velocity = _currentDirection * _currentSpeed;
    }

    private void DefeatBoss()
    {
        Win?.Invoke();
        Destroy(gameObject);
    }
}