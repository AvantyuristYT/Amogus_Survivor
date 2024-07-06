using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyGarbageHandler : MonoBehaviour
{
    [SerializeField] private GameObject taskObject;

    [SerializeField] private GarbageCollector garbageCollector;
    [SerializeField] private LeverHandle leverHandle;

    [SerializeField] private GameObject bottomCollider;

    // AUDIO
    [SerializeField] private AudioClip taskCompleteAudio;
    [SerializeField] private AudioClip garbageLoopAudio;
    [SerializeField] private AudioClip leverPullAudio;

    private void OnEnable()
    {
        leverHandle.LeverSwitched += StartGarbageDestroying;
        garbageCollector.AllGarbageDestroyed += TaskComplete;
    }

    private void OnDisable()
    {
        leverHandle.LeverSwitched -= StartGarbageDestroying;
        garbageCollector.AllGarbageDestroyed -= TaskComplete;
    }

    private void TaskComplete()
    {
        AudioManager.Instance.StopSound();
        AudioManager.Instance.PlayOneSound(taskCompleteAudio);
        StartCoroutine(CloseTask(1.5f));
    }


    private IEnumerator CloseTask(float time)
    {
        yield return new WaitForSeconds(time);
        taskObject.SetActive(false);
    }

    private void StartGarbageDestroying()
    {
        StartCoroutine(Tremble());

        AudioManager.Instance.PlayOneSound(leverPullAudio);
        AudioManager.Instance.PlaySoundLoop(garbageLoopAudio);

        bottomCollider.transform.DOMove(new Vector2(bottomCollider.transform.position.x - 700, bottomCollider.transform.position.y), 4f);
    }

    private IEnumerator Tremble()
    {
        for (int i = 0; i < 100; i++)
        {
            transform.localPosition += new Vector3(6, 6, 0);
            yield return new WaitForSeconds(0.05f);
            transform.localPosition -= new Vector3(6, 6, 0);
            yield return new WaitForSeconds(0.05f);
        }
    }
}
