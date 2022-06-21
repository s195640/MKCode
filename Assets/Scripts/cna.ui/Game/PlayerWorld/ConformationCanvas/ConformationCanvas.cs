using UnityEngine;

namespace cna.ui {
    public class ConformationCanvas : MonoBehaviour {

        [SerializeField] private BasePanel[] panels;
        [SerializeField] private GameObject maxPanel;
        [SerializeField] private GameObject minPanel;
        [SerializeField] private CNA_Button dialogBoxButton;


        public bool Active {
            get {
                foreach (BasePanel panel in panels) {
                    if (panel.Active) {
                        return true;
                    }
                }
                return false;
            }
        }

        public bool Minimized {
            get { return minPanel.activeSelf; }
        }

        public void Clean() {
            minPanel.SetActive(false);
            maxPanel.SetActive(true);
            foreach (BasePanel panel in panels) {
                panel.Active = false;
            }
        }

        public void UpdateUI() {
            foreach (BasePanel panel in panels) {
                panel.UpdateUI();
            }
        }

        public void OnClick_Max() {
            maxPanel.SetActive(true);
            minPanel.SetActive(false);
        }

        public void OnClick_Min() {
            maxPanel.SetActive(false);
            minPanel.SetActive(true);
        }
    }
}
