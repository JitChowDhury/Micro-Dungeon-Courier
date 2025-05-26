using UnityEngine;

public class ControlsMenu : MonoBehaviour
{
    public GameObject controlsPanel;
    public GameObject mainMenuPanel; // New reference

    void Update()
    {
        if (controlsPanel.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            CloseControls();
        }
    }

    public void OpenControls()
    {
        controlsPanel.SetActive(true);
        mainMenuPanel.SetActive(false); 
    }

    public void CloseControls()
    {
        controlsPanel.SetActive(false);
        mainMenuPanel.SetActive(true); 
    }
}
