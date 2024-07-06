using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.XR;

public class ManifoldsButtonsHolder : MonoBehaviour
{
    [SerializeField] private GameObject taskObject;

    [SerializeField] private ManifoldsButton[] manifoldsButtons;

    private int previousNumber;

    private bool canInteract = true;

    // AUDIO
    [SerializeField] private AudioClip selectButtonAudio;
    [SerializeField] private AudioClip failTaskAudio;
    [SerializeField] private AudioClip taskComplete;

    private void Start()
    {
        previousNumber = 0;

        RandomMoveButtons();
    }

    private void RandomMoveButtons()
    {
        for (int i = 0; i <  manifoldsButtons.Length; i++)
        {
            manifoldsButtons[i].transform.SetSiblingIndex(Random.Range(0, 9));
        }
    }

    public void CheckButton(int numberValue)
    {
        if (!canInteract)
            return;

        if (numberValue - previousNumber == 1)
        {
            AudioManager.Instance.PlayOneSound(selectButtonAudio);

            previousNumber++;

            foreach(ManifoldsButton button in manifoldsButtons)
            {
                if (button.NumberValue == numberValue)
                {
                    button.ButtonSuccess();

                    if (previousNumber == 10)
                    {
                        StartCoroutine(CloseTask(2f));
                    }

                    return;
                }
            }
        }
        else
        {
            canInteract = false;

            foreach (ManifoldsButton button in manifoldsButtons)
            {
                button.ButtonError();
            }

            StartCoroutine(RestartTask(2f));
        }
    }

    private IEnumerator RestartTask(float time)
    {
        AudioManager.Instance.PlayOneSound(failTaskAudio);
        yield return new WaitForSeconds(time);

        foreach (ManifoldsButton button in manifoldsButtons)
        {
            button.ResetButton();
        }

        previousNumber = 0;

        canInteract = true;
    }

    private IEnumerator CloseTask(float time)
    {
        canInteract = false;
        AudioManager.Instance.PlayOneSound(taskComplete);
        yield return new WaitForSeconds(time);

        taskObject.SetActive(false);
    }
}
