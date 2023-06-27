using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Spettro.UI
{
    public class TabSystem : MonoBehaviour
    {
        [Header("Info")]
        public int activeIndex;
        public bool openTabOnStart = true;
        public bool disablePanelsOnStart = false;
        public bool changeButtonColors = true;
        public TabButtonUI[] tabs;
        public UnityEvent<string> tabName = new UnityEvent<string>();
        [Header("Button Configuration")]
        public Color32 colorActive;
        public Color32 colorInactive;

        public void Start()
        {
            if (openTabOnStart)
                ActivateTab(0);
            if (disablePanelsOnStart)            
                DisablePanels();
            
        }
        public void ActivateTab(int index)
        {
            DisablePanels();
            //Make the specific index tab and button active
            int indexCapped = Mathf.Clamp(index, 0, tabs.Length);
            activeIndex = indexCapped;
            if (changeButtonColors)
                tabs[indexCapped].button.GetComponent<Image>().color = colorActive;
            tabs[indexCapped].tabPage.SetActive(true);
            tabs[indexCapped].enabled = true;
            tabName.Invoke(tabs[indexCapped].name);
        }
        public void NextTab()
        {
            activeIndex++;
            activeIndex = (int)Mathf.Repeat(activeIndex, tabs.Length);
            ActivateTab(activeIndex);
        }
        public void PreviousTab()
        {
            activeIndex--;
            activeIndex = (int)Mathf.Repeat(activeIndex, tabs.Length);
            ActivateTab(activeIndex);
        }
        public void DisablePanels()
        {
            //Make every button inactive and every tab inactive
            for (int i = 0; i < tabs.Length; i++)
            {
                if(changeButtonColors)
                tabs[i].button.GetComponent<Image>().color = colorInactive;
                if (tabs[i].tabPage)
                tabs[i].tabPage.SetActive(false);
                tabs[i].enabled = false;
            }
        }
    }
    [System.Serializable]
    public struct TabButtonUI
    {
        public Button button;
        public GameObject tabPage;
        public string name;
        public bool enabled;
    }

}
