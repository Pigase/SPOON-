using System.Collections;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class Ingredient : MonoBehaviour
{
    public enum IngredientType { Good, Bad, Deadly }

    [Header("Parametars")]
    [SerializeField] private float _speed = 3f;
    [SerializeField] private int _goodIngredientChance = 45;
    [SerializeField] private int _badIngredientChance = 45;
    [SerializeField] private float _speedMinRotate = 1f;
    [SerializeField] private float _speedMaxRotate = 2f;

    [Header("References")]
    [SerializeField] private Sprite[] _spritesGoodIngredient;
    [SerializeField] private Sprite[] _spritesBadIngredient;
    [SerializeField] private Sprite[] _spritesDeadlyIngredient;

    [SerializeField] private float _speedRotate;

    private Rigidbody2D _rb;
    private SpriteRenderer _sp;
    private IngredientType _type;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _sp = GetComponent<SpriteRenderer>();
    }
    private void OnEnable()
    {
        SetRandomRotate();
        SetRandomTypeAndSprite();

        GameManipulator.NumberWave += UpStats;
    }

    private void OnDisable()
    {
        GameManipulator.NumberWave += UpStats;
    }
    private void SetRandomTypeAndSprite()
    {
        int ChooseIngredient = UnityEngine.Random.Range(1, 101);
        int spriteIndex;

        if (ChooseIngredient <= _goodIngredientChance)
        {
            _type = IngredientType.Good;
            spriteIndex = UnityEngine.Random.Range(0, _spritesGoodIngredient.Length);
            _sp.sprite = _spritesGoodIngredient[spriteIndex];
        }
        else if (ChooseIngredient <= _goodIngredientChance + _badIngredientChance)
        {
            _type = IngredientType.Bad;
            spriteIndex = UnityEngine.Random.Range(0, _spritesBadIngredient.Length);
            _sp.sprite = _spritesBadIngredient[spriteIndex];
        }
        else
        {
            _type = IngredientType.Deadly;
            spriteIndex = UnityEngine.Random.Range(0, _spritesDeadlyIngredient.Length);
            _sp.sprite = _spritesDeadlyIngredient[spriteIndex];
        }
    }

    private void UpStats(int num)
    {
        if (num == 2)
        {
            _goodIngredientChance = 35;
            _badIngredientChance = 40;
            _speed = 5f;
        }
        if (num == 3)
        {
            _goodIngredientChance = 30;
            _badIngredientChance = 35;
            _speed = 7f;
        }
    }
    private void SetRandomRotate()
    {
        _speedRotate = UnityEngine.Random.Range(_speedMinRotate, _speedMaxRotate);
    }

    private void FixedUpdate()
    {
        transform.Rotate(Vector3.forward * _speedRotate * Time.deltaTime);
        _rb.velocity = Vector2.down * _speed;
    }

    public IngredientType GetIngredientType() => _type;
}
