using UnityEngine;

public class MultiPlatePuzzleManager : MonoBehaviour
{
    public PressurePlate[] plates;
    public TriggerableObject target;

    void Update()
    {
        if (AllPlatesActive())
        {
            target.Activate();
        }
        else
        {
            target.Deactivate();
        }
    }

    bool AllPlatesActive()
    {
        foreach (PressurePlate plate in plates)
        {
            if (!plate.IsActive) return false;
        }
        return true;
    }
}
