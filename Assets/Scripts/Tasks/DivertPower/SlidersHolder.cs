using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class SlidersHolder : MonoBehaviour
{
    public Action TaskCompleted;

    [SerializeField] private Slider[] powerSliders;

    private int activeSliderID;

    private bool isInteractable = true;

    private void Start()
    {
        foreach(Slider slider in powerSliders)
        {
            slider.value = Random.Range(40f, 70f);
        }

        StartCoroutine(RandomChangeSliderValue());
    }

    private void Update()
    {
        if (powerSliders[activeSliderID].value >= 100 && isInteractable)
        {
            isInteractable = false;
            TaskCompleted?.Invoke();
        }
    }

    private IEnumerator RandomChangeSliderValue()
    {
        while (true)
        {
            foreach (Slider slider in powerSliders)
            {
                if (slider != powerSliders[activeSliderID])
                {
                    slider.value += Random.Range(-3, 3);
                    slider.value = Mathf.Clamp(slider.value, 40f, 70f);
                }
            }

            yield return new WaitForSeconds(0.1f);
        }
    }

    public void SelectActiveSlider(int sliderID)
    {
        activeSliderID = sliderID;
        powerSliders[activeSliderID].value = 50f;
    }

    public void ChangeSliderValue(int sliderID, float newValue)
    {
        powerSliders[sliderID].value = newValue;
    }
}
