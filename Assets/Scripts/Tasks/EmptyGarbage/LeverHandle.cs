using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class LeverHandle : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    public Action LeverSwitched;

    private bool isDragging = false;
    private bool canInteract = true;

    [SerializeField] private RectTransform leverHandleRect;
    [SerializeField] private RectTransform startPosition;
    [SerializeField] private RectTransform endPosition;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (canInteract)
            isDragging = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (canInteract)
        {
            isDragging = false;

            if (leverHandleRect.position.y <= endPosition.position.y)
            {
                // Мини-игра пройдена
                Debug.Log("Success");
                canInteract = false;
                LeverSwitched?.Invoke();
            }
            else
            {
                // Возвращаем рычаг на начальную позицию
                leverHandleRect.DOMove(new Vector2(startPosition.position.x, startPosition.position.y), 0.5f);
            }
        }
    }

    private void Update()
    {
        if (isDragging && canInteract)
        {
            Vector2 mousePos = Input.mousePosition;

            leverHandleRect.position = new Vector2(leverHandleRect.position.x, Mathf.Clamp(mousePos.y, endPosition.position.y, startPosition.position.y));
        }
    }
}
