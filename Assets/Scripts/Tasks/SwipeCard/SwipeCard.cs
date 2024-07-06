using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class SwipeCard : MonoBehaviour, IPointerClickHandler, IPointerUpHandler, IPointerDownHandler
{
    private bool canInteract = false;
    private bool isDragging = false;

    [SerializeField] private GameObject swipeCardTask;

    [SerializeField] private RectTransform card;
    [SerializeField] private RectTransform scannerStartPos;
    [SerializeField] private RectTransform scannerEndPos;

    [SerializeField] private float minSwipeTime = 1.0f; // Мин. время для прохождения
    [SerializeField] private float maxSwipeTime = 2.0f; // Макс. время для прохождения
    [SerializeField] private float resetTime = 0.5f; // Время до возврата карточки на место
                     private float swipeStartTime;

    [SerializeField] private TextMeshProUGUI screenText;

    // AUDIO
    [SerializeField] private AudioClip[] cardMovesAudio;
    [SerializeField] private AudioClip cardAcceptAudio;
    [SerializeField] private AudioClip cardDenyAudio;
    [SerializeField] private AudioClip walletOutAudio;


    public void OnPointerClick(PointerEventData eventData)
    {
        if (!canInteract)
        {
            card.DOMove(new Vector2(scannerStartPos.position.x, scannerStartPos.position.y), 0.5f);
            canInteract = true;

            screenText.text = "Проведите картой по сканеру...";
            AudioManager.Instance.PlayOneSound(walletOutAudio);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!canInteract)
            return;

        isDragging = true;
        swipeStartTime = Time.time;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!canInteract)
            return;

        isDragging = false;

        float swipeTime = Time.time - swipeStartTime;

        Debug.Log(swipeTime);

        if (swipeTime >= minSwipeTime && swipeTime <= maxSwipeTime
            && card.position.x >= scannerEndPos.position.x)
        {
            // Мини-игра пройдена
            Debug.Log("Success");
            screenText.text = "Успех! Благодарим вас.";
            AudioManager.Instance.PlayOneSound(cardAcceptAudio);
            StartCoroutine(CloseTask(2));
        }
        else
        {
            // Возвращаем карту на начальную позицию
            card.DOMove(new Vector2(scannerStartPos.position.x, scannerStartPos.position.y), 0.5f);
            AudioManager.Instance.PlayOneSound(cardDenyAudio);
        }

        if (swipeTime < minSwipeTime)
            screenText.text = "Слишком быстро! Попробуйте ещё раз...";
        else if (swipeTime > maxSwipeTime)
            screenText.text = "Слишком медленно! Попробуйте ещё раз...";
    }

    private void Start()
    {
        screenText.text = "Вставьте карту...";
    }

    private void Update()
    {
        if (!canInteract)
            return;

        if (isDragging)
        {
            Vector2 mousePos = Input.mousePosition;

            card.position = new Vector2(Mathf.Clamp(mousePos.x, scannerStartPos.position.x, scannerEndPos.position.x), card.position.y);
        }
    }

    private IEnumerator CloseTask(float time)
    {
        yield return new WaitForSeconds(time);

        swipeCardTask.SetActive(false);
    }
}
