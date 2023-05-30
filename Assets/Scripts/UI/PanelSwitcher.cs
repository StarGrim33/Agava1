using UnityEngine;

public class PanelSwitcher : MonoBehaviour
{
    [SerializeField] private GameObject _object;

    public void OpenPanel(GameObject panel)
    {
        _object.SetActive(false);
        _object.SetActive(true);
        panel.SetActive(true);
    }

    public void ClosePanel(GameObject panel)
    {
        panel.SetActive(false);
    }
}
