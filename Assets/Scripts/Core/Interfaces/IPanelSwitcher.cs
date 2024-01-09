using UnityEngine;

namespace Interfaces
{
    public interface IPanelSwitcher
    {
        void OpenPanel(GameObject panel);

        void ClosePanel(GameObject panel);
    }
}
