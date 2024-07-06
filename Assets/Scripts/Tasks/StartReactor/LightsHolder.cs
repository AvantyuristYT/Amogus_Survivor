using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightsHolder : MonoBehaviour
{
    [SerializeField] private LightDetector[] lightsList;

    public void ActivateLight(int lightIndex)
    {
        lightsList[lightIndex - 1].LightOn();
    }

    public void ResetAllLights()
    {
        foreach (LightDetector light in lightsList)
        {
            light.LightOff();
        }
    }
}
