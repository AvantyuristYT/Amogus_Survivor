using UnityEngine;
using YG;

public class MobileInputUI : MonoBehaviour
{
    [SerializeField] private GameObject mobileButtons;

    private void Start()
    {
        if (YandexGame.EnvironmentData.isMobile)
            mobileButtons.SetActive(true);
        else
            mobileButtons.SetActive(false);
    }
}
