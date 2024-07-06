using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UploadDataHandler : MonoBehaviour
{
    [SerializeField] private GameObject taskObject;

    [SerializeField] private GameObject downloadButton;
    [SerializeField] private GameObject progressBar;
    [SerializeField] private GameObject paperImage;

    [SerializeField] private TextMeshProUGUI percentageUploadText;
    [SerializeField] private TextMeshProUGUI remainingTimeText;

    private Slider progressBarSlider;
    private PaperParabolaAnimation paperAnimation;

    // AUDIO
    [SerializeField] private AudioClip taskCompleteAudio;

    private void Start()
    {
        progressBarSlider = progressBar.GetComponent<Slider>();
        paperAnimation = paperImage.GetComponent<PaperParabolaAnimation>();

        downloadButton.SetActive(true);
        progressBar.SetActive(false);
        paperImage.SetActive(false);
    }

    public void StartUploadData()
    {
        downloadButton.SetActive(false);
        progressBar.SetActive(true);
        paperImage.SetActive(true);
        paperAnimation.StartAnimation();

        percentageUploadText.text = "0%";
        remainingTimeText.text = "Оставшееся время: 343д 22ч 12м 3с";

        StartCoroutine(UploadData());
    }

    private IEnumerator UploadData()
    {
        progressBarSlider.value = 0;
        yield return new WaitForSeconds(1);
        progressBarSlider.value = 20f;
        remainingTimeText.text = "Оставшееся время: 127д 2ч 56м 20с";
        yield return new WaitForSeconds(1);
        progressBarSlider.value = 35f;
        remainingTimeText.text = "Оставшееся время: 56д 9ч 2м 48с";
        yield return new WaitForSeconds(1);
        progressBarSlider.value = 65f;
        remainingTimeText.text = "Оставшееся время: 2д 37м 3с";
        yield return new WaitForSeconds(1);
        progressBarSlider.value = 70f;
        remainingTimeText.text = "Оставшееся время: 17ч 54м 41с";

        while (progressBarSlider.value < progressBarSlider.maxValue)
        {
            progressBarSlider.value += 1f;
            yield return new WaitForSeconds(0.1f);
            remainingTimeText.text = "Оставшееся время: " + (progressBarSlider.maxValue - progressBarSlider.value) + "c";
        }

        remainingTimeText.text = "Завершено!";

        yield return new WaitForSeconds(0.5f);
        AudioManager.Instance.PlayOneSound(taskCompleteAudio);
        yield return new WaitForSeconds(1);
        taskObject.SetActive(false);
    }

    public void ChangePercentageText()
    {
        percentageUploadText.text = (int)progressBarSlider.value + "%";
    }
}
