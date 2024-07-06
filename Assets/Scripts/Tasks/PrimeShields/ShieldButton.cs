using UnityEngine;
using UnityEngine.UI;

public class ShieldButton : MonoBehaviour
{
    [SerializeField] private Image buttonImage;

    public void ActivateButton()
    {
        buttonImage.color = Color.white;
    }

    public void DeactivateButton()
    {
        buttonImage.color = Color.red;
    }
}
