using UnityEngine;

public class SwitchesHolder : MonoBehaviour
{
    [SerializeField] private PowerSwitch[] powerSwitches;

    public int GetSwitchesCount()
    {
        return powerSwitches.Length;
    }

    public float GetSwitchValue(int switchID)
    {
        return powerSwitches[switchID].GetSwitchValue();
    }

    public void SelectActiveSwitch(int switchID)
    {
        for (int i = 0; i < powerSwitches.Length; i++)
        {
            if (i == switchID)
            {
                powerSwitches[i].Activate();
            }
            else
            {
                powerSwitches[i].Deactivate();
            }
        }
    }

    public void DeactivateSwitch(int switchID)
    {
        powerSwitches[switchID].Deactivate();
    }
}
