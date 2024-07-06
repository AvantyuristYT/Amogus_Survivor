using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

public class SimonSaysHandle : MonoBehaviour
{
    [SerializeField] private GameObject taskObject;

    private int currentStage = 1;

    private int[] randomNumbers = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
    private List<int> newStageNumbersList = new List<int>();
    private Random rand = new Random();

    [SerializeField] private SimonButtonHolder simonButtonHolder;
    [SerializeField] private ButtonsHolder buttonsHolder;
    [SerializeField] private LightsHolder leftLightsHolder;

    // AUDIO
    [SerializeField] private AudioClip buttonShowAudio;
    [SerializeField] private AudioClip taskComplete;

    private void OnEnable()
    {
        buttonsHolder.StageCompleted += StartNextStage;
        buttonsHolder.StageFailed += ResetStage;
    }

    private void OnDisable()
    {
        buttonsHolder.StageCompleted -= StartNextStage;
        buttonsHolder.StageFailed -= ResetStage;
    }

    private void Start()
    {
        foreach (int i in randomNumbers)
        {
            Debug.Log(i);
        }

        StartCoroutine(NewStage());
    }

    private void BlendNumbers()
    {
        randomNumbers = randomNumbers.OrderBy(x => rand.Next()).ToArray();
    }

    private IEnumerator NewStage()
    {
        buttonsHolder.DeactivateButtons();

        BlendNumbers();

        yield return new WaitForSeconds(1f);

        leftLightsHolder.ActivateLight(currentStage);

        for (int i = 0; i < currentStage; i++)
        {
            AudioManager.Instance.PlayOneSound(buttonShowAudio);
            simonButtonHolder.ButtonShow(randomNumbers[i]);
            yield return new WaitForSeconds(0.5f);
            simonButtonHolder.ButtonHide(randomNumbers[i]);
            yield return new WaitForSeconds(0.5f);

            newStageNumbersList.Add(randomNumbers[i]);
        }

        buttonsHolder.SetCurrentStageNumbers(newStageNumbersList);

        buttonsHolder.ActivateButtons();
    }

    private void StartNextStage()
    {
        if (currentStage == 5)
        {
            StartCoroutine(CloseTask(2f));
        }
        else
        {
            currentStage++;
            StartCoroutine(NewStage());
        }
    }
    
    private void ResetStage()
    {
        currentStage = 1;
        leftLightsHolder.ResetAllLights();
        StartCoroutine(NewStage());
    }

    private IEnumerator CloseTask(float time)
    {
        AudioManager.Instance.PlayOneSound(taskComplete);
        buttonsHolder.DeactivateButtons();

        yield return new WaitForSeconds(time);
        taskObject.SetActive(false);
    }
}
