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
        remainingTimeText.text = "���������� �����: 343� 22� 12� 3�";

        StartCoroutine(UploadData());
    }

    private IEnumerator UploadData()
    {
        progressBarSlider.value = 0;
        yield return new WaitForSeconds(1);
        progressBarSlider.value = 20f;
        remainingTimeText.text = "���������� �����: 127� 2� 56� 20�";
        yield return new WaitForSeconds(1);
        progressBarSlider.value = 35f;
        remainingTimeText.text = "���������� �����: 56� 9� 2� 48�";
        yield return new WaitForSeconds(1);
        progressBarSlider.value = 65f;
        remainingTimeText.text = "���������� �����: 2� 37� 3�";
        yield return new WaitForSeconds(1);
        progressBarSlider.value = 70f;
        remainingTimeText.text = "���������� �����: 17� 54� 41�";

        while (progressBarSlider.value < progressBarSlider.maxValue)
        {
            progressBarSlider.value += 1f;
            yield return new WaitForSeconds(0.1f);
            remainingTimeText.text = "���������� �����: " + (progressBarSlider.maxValue - progressBarSlider.value) + "c";
        }

        remainingTimeText.text = "���������!";

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
