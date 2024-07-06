using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuelGasHandler : MonoBehaviour
{
    [SerializeField] private GameObject taskObject;

    [SerializeField] private Slider fuelSlider;

    [SerializeField] private Image redDetector;
    [SerializeField] private Image greenDetector;

    private bool buttonPressed = false;
    private bool isComplete = false;

    // AUDIO
    [SerializeField] private AudioClip taskComplete;

    public void PointerUp()
    {
        buttonPressed = false;
    }

    public void PointerDown()
    {
        buttonPressed = true;
    }


    private void Start()
    {
        fuelSlider.value = 0;
    }

    private void Update()
    {
        if (buttonPressed && !isComplete)
        {
            fuelSlider.value += 30 * Time.deltaTime;

            if (fuelSlider.value >= fuelSlider.maxValue)
            {
                isComplete = true;
                redDetector.color = new Color32(100, 0, 0, 255);
                greenDetector.color = new Color32(0, 255, 0, 255);

                AudioManager.Instance.PlayOneSound(taskComplete);
                StartCoroutine(CloseTask(1.5f));
            }
            else
            {
                redDetector.color = new Color32(255, 0, 0, 255);
                greenDetector.color = new Color32(0, 100, 0, 255);
            }
        }
    }

    private IEnumerator CloseTask(float time)
    {
        yield return new WaitForSeconds(time);
        taskObject.SetActive(false);
    }
}
