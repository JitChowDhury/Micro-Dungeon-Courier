using UnityEngine;

public class SceneStartFade : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(FadeManager.Instance.FadeIn());
    }
}
