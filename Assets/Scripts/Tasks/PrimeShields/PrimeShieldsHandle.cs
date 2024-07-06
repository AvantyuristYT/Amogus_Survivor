using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrimeShieldsHandle : MonoBehaviour
{
    [SerializeField] private GameObject taskObject;

    private List<bool> activatedButtons = new List<bool>();

    [SerializeField] private ShieldButton[] shieldsButton;
    [SerializeField] private Image turtleImage;

    private bool canInteract = true;

    private int countActiveButton = 0;

    private byte colorForTurtle = 0;

    // AUDIO
    [SerializeField] private AudioClip selectRedAudio;
    [SerializeField] private AudioClip selectWhiteAudio;
    [SerializeField] private AudioClip taskComplete;

    public void ButtonClicked(int index)
    {
        if (!canInteract)
            return;

        if (activatedButtons[index - 1] == true)
        {
            AudioManager.Instance.PlayOneSound(selectWhiteAudio);
            countActiveButton--;
            colorForTurtle -= 36;
            turtleImage.color = new Color32(255, colorForTurtle, colorForTurtle, 255);

            shieldsButton[index - 1].DeactivateButton();
        }
        else
        {
            AudioManager.Instance.PlayOneSound(selectRedAudio);
            countActiveButton++;
            colorForTurtle += 36;
            turtleImage.color = new Color32(255, colorForTurtle, colorForTurtle, 255);
            shieldsButton[index - 1].ActivateButton();
        }

        activatedButtons[index-1] = !activatedButtons[index-1];



        if (countActiveButton == activatedButtons.Count)
        {
            StartCoroutine(CloseTask());
        }
            
    }

    private void Start()
    {
        for (int i = 0; i < 7; i++)
        {
            int state = Random.Range(0, 2);
            if (state == 0)
                activatedButtons.Add(false);
            else
                activatedButtons.Add(true);
        }

        for (int i = 0; i < activatedButtons.Count; i++)
        {
            if (activatedButtons[i] == true)
            {
                shieldsButton[i].ActivateButton();
                countActiveButton++;
                colorForTurtle += 36;
            }
            else
            {
                shieldsButton[i].DeactivateButton();
            }
        }

        turtleImage.color = new Color32(255, colorForTurtle, colorForTurtle, 255);
    }

    private IEnumerator CloseTask()
    {
        turtleImage.color = Color.white;

        AudioManager.Instance.PlayOneSound(taskComplete);
        canInteract = false;
        yield return new WaitForSeconds(2f);

        taskObject.SetActive(false);
    }
}
