using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Soup : MonoBehaviour
{
    [System.Serializable]
    public class IngredientEvent : UnityEvent<Ingredient.IngredientType> { }

    [Header("Settings")]
    [SerializeField] private float _maxQuality = 100f;
    [SerializeField] private float _goodValue = 5f;
    [SerializeField] private float _badValue = -10f;
    [SerializeField] private float _deadlyValue = -100f;

    [Header("References")]
    [SerializeField] private Slider _qualitySlider;

    [Header("Events")]
    public IngredientEvent OnIngredientAdded;
    public UnityEvent OnSoupRuined;

    private float _currentQuality;

    private void Start()
    {
        _currentQuality = _maxQuality / 2f; // Стартовое значение
        UpdateQualityUI();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Ingredient ingredient = other.GetComponent<Ingredient>();
        if (ingredient == null) 
            return;

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
    }

    private void AddQuality(float amount)
    {
        _currentQuality = Mathf.Clamp(_currentQuality + amount, 0f, _maxQuality);
        UpdateQualityUI();

        if (_currentQuality <= Mathf.Epsilon)
        {
            OnSoupRuined?.Invoke();
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
}
