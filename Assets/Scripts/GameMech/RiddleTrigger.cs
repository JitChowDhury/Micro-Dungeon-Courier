using UnityEngine;

public class RiddleTrigger : MonoBehaviour
{
    [TextArea(2, 4)]
    public string riddleMessage;
    private bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!triggered && other.CompareTag("Player"))
        {
            triggered = true;
            RiddleManager rm = GameObject.Find("RiddleManager").GetComponent<RiddleManager>();
            if (rm != null)
            {
                rm.ShowRiddle(riddleMessage);
            }
        }
    }
}
