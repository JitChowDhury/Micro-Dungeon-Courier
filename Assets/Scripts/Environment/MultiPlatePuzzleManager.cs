using UnityEngine;

public class MultiPlatePuzzleManager : MonoBehaviour
{
    public PressurePlate[] plates;
    public TriggerableObject target;
    private bool wasActive = false;
    void Update()
    {
        bool currentlyActive = AllPlatesActive();

        if (currentlyActive && !wasActive)
        {
            target.Activate(); // 🔊 play door sound here
        }
        else if (!currentlyActive && wasActive)
        {
            target.Deactivate();
        }

        wasActive = currentlyActive;

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
