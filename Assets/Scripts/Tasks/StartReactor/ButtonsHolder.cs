using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonsHolder : MonoBehaviour
{
    public Action StageCompleted;
    public Action StageFailed;

    [SerializeField] private Image[] buttons;
    [SerializeField] private LightsHolder rightLightsHolder;

    private bool canInteract = true;
    private int activeLights = 0;

    private List<int> currentStageNumbers;

    // AUDIO
    [SerializeField] private AudioClip buttonPressAudio;
    [SerializeField] private AudioClip failAudio;

    private void Start()
    {
        DeactivateButtons();
    }

    public void ButtonClick(int numberValue)
    {
        if (canInteract)
        {
            StartCoroutine(ButtonClicked(numberValue));
        }
    }

    private IEnumerator ButtonClicked(int numberValue)
    {
        AudioManager.Instance.PlayOneSound(buttonPressAudio);

        if (numberValue == currentStageNumbers[0])
        {
            activeLights++;
            rightLightsHolder.ActivateLight(activeLights);
            currentStageNumbers.RemoveAt(0);
            Debug.Log(currentStageNumbers.Count);

            buttons[numberValue - 1].color = Color.blue;
            canInteract = false;
            yield return new WaitForSeconds(0.2f);
            buttons[numberValue - 1].color = Color.white;
            canInteract = true;

            if (currentStageNumbers.Count < 1 || currentStageNumbers == null)
            {
                StageCompleted?.Invoke();
                rightLightsHolder.ResetAllLights();
                activeLights = 0;
            }
        }
        else
        {
            AudioManager.Instance.PlayOneSound(failAudio);
            currentStageNumbers.Clear();
            rightLightsHolder.ResetAllLights();
            StartCoroutine(FailedStage());
        }
    }

    public void ActivateButtons()
    {
        canInteract = true;

        foreach(Image button in buttons)
        {
            button.color = Color.white;
        }
    }

    public void DeactivateButtons()
    {
        canInteract = false;

        foreach (Image button in buttons)
        {
            button.color = new Color32(100, 100, 100, 255);
        }
    }

    public void SetCurrentStageNumbers(List<int> newStageArray)
    {
        currentStageNumbers = newStageArray;
    }

    private IEnumerator FailedStage()
    {
        canInteract = false;

        foreach (Image button in buttons)
        {
            button.color = new Color32(100, 0, 0, 255);
        }

        yield return new WaitForSeconds(1.5f);

        StageFailed?.Invoke();
    }
}
