using UnityEngine;

public class PanelsHandler : MonoBehaviour
{
    public static PanelsHandler singleton;

    private void Awake() => singleton = this;

    public void CloseOrOpenPanel(GameObject panel)
    {
        if (panel.activeSelf) ClosePanel(panel);
        else OpenPanel(panel);
    }

    public void ClosePanel(GameObject panel) => panel.SetActive(false);

    public void OpenPanel(GameObject panel) => panel.SetActive(true);
}
