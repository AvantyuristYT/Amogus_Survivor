using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageCollector : MonoBehaviour
{
    public Action AllGarbageDestroyed;

    [SerializeField] private List<GameObject> garbageList;
    [SerializeField] private DestroyValidator destroyValidator;

    private void OnEnable()
    {
        destroyValidator.ObjectDestroyed += DeleteGarbageFromList;
    }

    private void OnDisable()
    {
        destroyValidator.ObjectDestroyed -= DeleteGarbageFromList;
    }

    private void DeleteGarbageFromList(GameObject gameObjectForDelete)
    {
        garbageList.Remove(gameObjectForDelete);

        if (garbageList.Count == 0)
        {
            Debug.Log("осярни!!");
            AllGarbageDestroyed?.Invoke();
        }
    }
}
