using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System;

public class Soup : MonoBehaviour
{
    [System.Serializable]
    public class IngredientEvent : UnityEvent<Ingredient.IngredientType> { }

    [Header("Settings")]
    [SerializeField] private float _maxQuality = 100f;
    [SerializeField] private float _goodValue = 5f;
    [SerializeField] private float _badValue = -10f;
    [SerializeField] private float _deadlyValue = -100f;
    [SerializeField] private float _timeAfterWave = 5f;

    [Header("References")]
    [SerializeField] private Slider _qualitySlider;

    [Header("Events")]
    public IngredientEvent OnIngredientAdded;
    public static Action OnSoupRuined;
    public static Action OnSoupFull;
    public static Action IOnIngredientAdded;

    private float _currentQuality;
    private bool _noDamage = false;

    private void Start()
    {
        _currentQuality = _maxQuality / 2f; // Стартовое значение
        UpdateQualityUI();
    }

    private void OnEnable()
    {
        GameManipulator.BossWave += NoDamage;
    }
    private void OnDisable()
    {
        GameManipulator.BossWave += NoDamage;
    }

    private void NoDamage()
    {
        _noDamage = true;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        Boss boss = other.GetComponent<Boss>();
        Ingredient ingredient = other.GetComponent<Ingredient>();
        if (ingredient == null)
        {
            if (boss == null)
            {
                return;
            }
            else
            {
                AddQuality(_deadlyValue);
                return;
            }
        }

            ProcessIngredient(ingredient);
        other.gameObject.SetActive(false); // Отключаем ингредиент
    }

    private void ProcessIngredient(Ingredient ingredient)
    {
        // Определяем эффект в зависимости от типа
        switch (ingredient.GetIngredientType())
        {
            case Ingredient.IngredientType.Good:
                AddQuality(_goodValue);
                break;

            case Ingredient.IngredientType.Bad:
                AddQuality(_badValue);
                break;

            case Ingredient.IngredientType.Deadly:
                AddQuality(_deadlyValue);
                break;
        }

        // Вызываем событие
        OnIngredientAdded?.Invoke(ingredient.GetIngredientType());
        IOnIngredientAdded?.Invoke();
    }

    private void AddQuality(float amount)
    {
        _currentQuality = Mathf.Clamp(_currentQuality + amount, 0f, _maxQuality);
        UpdateQualityUI();

        if (_currentQuality <= Mathf.Epsilon)
        {
            OnSoupRuined?.Invoke();
        }
        if (_currentQuality >= _maxQuality)
        {
            OnSoupFull?.Invoke();

            StartCoroutine(DoInvulnerability());

        }
    }

    private void UpdateQualityUI()
    {
        if (_qualitySlider != null)
        {
            _qualitySlider.value = _currentQuality / _maxQuality;

            // Меняем цвет в зависимости от качества
            _qualitySlider.fillRect.GetComponent<Image>().color = Color.Lerp(Color.red, Color.green, _currentQuality / _maxQuality);
        }
    }
    IEnumerator DoInvulnerability()
    {
        yield return new WaitForSeconds(_timeAfterWave);
        if (_noDamage == false)
        {
            _currentQuality = 40;
            UpdateQualityUI();
        }
    }
}
