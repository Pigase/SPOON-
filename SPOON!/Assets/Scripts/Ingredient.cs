using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Ingredient : MonoBehaviour
{
    [SerializeField] private float _speed = 1f;
    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        // «адаем скорость через физику (более стабильно)
        _rb.velocity = Vector2.down * _speed;
    }
}
