using Unity.VisualScripting;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    private int objectsOnPlate = 0;

    public bool IsActive => objectsOnPlate > 0;

    public Renderer plateRenderer;
    public Color inactiveColor;
    public Color activeColor = Color.black;

    private void Start()
    {
        // Set initial color
        if (plateRenderer != null)
        {
            inactiveColor = plateRenderer.material.color;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Pushable")) return;

        objectsOnPlate++;
        if (IsActive && plateRenderer != null)
        {
            plateRenderer.material.color = activeColor;
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (!other.CompareTag("Pushable")) return;

        objectsOnPlate--;
        if (!IsActive && plateRenderer != null)
        {
            plateRenderer.material.color = inactiveColor;
        }
    }
}
