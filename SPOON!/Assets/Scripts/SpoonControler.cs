using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SpoonControler : MonoBehaviour
{
    [SerializeField] private BoxCollider2D _boundaryCollider;
    private Vector2 _minBounds;
    private Vector2 _maxBounds;
    private Camera _mainCamera;

    private void Awake()
    {
        _mainCamera = Camera.main;
        CalculateBounds();
    }

    private void CalculateBounds()
    {
        // Получаем мировые координаты границ
        Bounds bounds = _boundaryCollider.bounds;
        _minBounds = bounds.min;
        _maxBounds = bounds.max;

        // Для дебага (можно удалить)
        Debug.Log($"Bounds: {_minBounds} to {_maxBounds}");
    }

    private void Update()
    {
        //if (EventSystem.current.IsPointerOverGameObject()) return;

        Vector2 mousePos = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        MoveSpoon(mousePos);
    }

    private void MoveSpoon(Vector2 targetPosition)
    {
        float clampedX = Mathf.Clamp(targetPosition.x, _minBounds.x, _maxBounds.x);
        float clampedY = Mathf.Clamp(targetPosition.y, _minBounds.y, _maxBounds.y);

        transform.position = new Vector2(clampedX, clampedY);
    }
}
