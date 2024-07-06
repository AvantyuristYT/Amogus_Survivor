using System.Collections;
using UnityEngine;

public class DivertPowerHandler : MonoBehaviour
{
    [SerializeField] private GameObject taskObject;

    [SerializeField] private SlidersHolder slidersHolder;
    [SerializeField] private SwitchesHolder switchesHolder;

    private int switchesCount;
    private int activeSwitchID;

    // AUDIO
    [SerializeField] private AudioClip taskComplete;

    private void OnEnable()
    {
        slidersHolder.TaskCompleted += CloseTask;
    }

    private void OnDisable()
    {
        slidersHolder.TaskCompleted -= CloseTask;
    }

    private void Start()
    {
        switchesCount = switchesHolder.GetSwitchesCount();
        activeSwitchID = Random.Range(0, switchesCount);

        InitializeSlidersAndSwitches();
    }
    
    private void InitializeSlidersAndSwitches()
    {
        switchesHolder.SelectActiveSwitch(activeSwitchID);
        slidersHolder.SelectActiveSlider(activeSwitchID);
    }

    public void ChangeSliderValue(int switchID)
    {
        if (activeSwitchID == switchID)
        {
            float newValue = switchesHolder.GetSwitchValue(switchID);
            slidersHolder.ChangeSliderValue(switchID, newValue);
        }
    }

    private void CloseTask()
    {
        AudioManager.Instance.PlayOneSound(taskComplete);
        switchesHolder.DeactivateSwitch(activeSwitchID);
        StartCoroutine(TimerBeforeClose(2));
    }

    private IEnumerator TimerBeforeClose(float time)
    {
        yield return new WaitForSeconds(time);
        taskObject.SetActive(false);
    }
}
