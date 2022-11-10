using System;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Получить вектор свайпа
/// </summary>
public class SwipeDetection : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public event Action<Vector2> OnSwipe = delegate { };

    [SerializeField] private Camera _mainCamera;

    private Vector3 _startPosition;
    private Vector2 _endPosition;

    private Vector3 GetPointToWorldPoint(Vector2 pontVector)
    {
        return _mainCamera.ScreenToWorldPoint(new Vector3(pontVector.x, pontVector.y, _mainCamera.nearClipPlane));
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _startPosition = GetPointToWorldPoint(eventData.position);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _endPosition = GetPointToWorldPoint(eventData.position) - _startPosition;
        OnSwipe?.Invoke(_endPosition);
    }
}
