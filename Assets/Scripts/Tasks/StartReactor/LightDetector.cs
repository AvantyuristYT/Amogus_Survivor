using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightDetector : MonoBehaviour
{
    [SerializeField] private Image lightImage;

    public void LightOn()
    {
        lightImage.color = Color.green;
    }

    public void LightOff()
    {
        lightImage.color = Color.white;
    }
}
