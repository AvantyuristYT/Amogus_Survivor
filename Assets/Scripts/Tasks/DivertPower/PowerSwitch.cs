using UnityEngine;
using UnityEngine.UI;

public class PowerSwitch : MonoBehaviour
{
    [SerializeField] private Image handleSprite;
    private Slider switchSlider;

    private void Awake()
    {
        switchSlider = GetComponent<Slider>();
    }

    public void Activate()
    {
        handleSprite.color = Color.white;
        switchSlider.interactable = true;
    }

    public void Deactivate()
    {
        handleSprite.color = new Color(0.2f, 0.2f, 0.2f);
        switchSlider.interactable = false;
    }

    public float GetSwitchValue()
    {
        return switchSlider.value;
    }
}
