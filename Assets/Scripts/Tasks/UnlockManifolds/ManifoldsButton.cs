using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ManifoldsButton : MonoBehaviour
{
    [SerializeField] private int numberValue; public int NumberValue { get { return numberValue; } }
    [SerializeField] private Image numberImage;

    private void Start()
    {
        ResetButton();
    }

    public void ButtonSuccess()
    {
        numberImage.color = Color.green;
    }

    public void ButtonError()
    {
        numberImage.color = Color.red;
    }

    public void ResetButton()
    {
        numberImage.color = Color.white;
    }
}
