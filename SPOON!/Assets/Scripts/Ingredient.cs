using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Ingredient : MonoBehaviour
{
    [SerializeField] private float _speed = 1;

    private Vector2 _target;

    
    private void OnEnable()
    {
        _target = new Vector2(transform.position.x, transform.position.y-30);
    }
    private void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, _target, _speed * Time.deltaTime);
    }
}
