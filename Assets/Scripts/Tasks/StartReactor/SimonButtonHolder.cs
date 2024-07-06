using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimonButtonHolder : MonoBehaviour
{
    [SerializeField] private Image[] simonButtons;

    private void Awake()
    {
        foreach(var button in simonButtons)
        {
            button.gameObject.SetActive(false);
        }
    }

    public void ButtonShow(int index)
    {
        simonButtons[index-1].gameObject.SetActive(true);
    }

    public void ButtonHide(int index)
    {
        simonButtons[index - 1].gameObject.SetActive(false);
    }
}
