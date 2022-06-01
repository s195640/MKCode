using UnityEngine;

namespace cna.ui {
    public class PlayerWorld : MonoBehaviour {
        [SerializeField] private PlayerGrid playerGrid;
        [SerializeField] private PlayerOfferingCanvas playerOfferingCanvas;
        [SerializeField] private BattleCanvas battleCanvas;
        [SerializeField] private GameObject notificationCanvas;
        [SerializeField] private ConformationCanvas conformationCanvas;
        [SerializeField] private OptionsCanvas optionsCanvas;
        [SerializeField] private OverlayCanvas OverlayCanvas;

        public void UpdateUI() {
            playerGrid.UpdateUI();
            playerOfferingCanvas.UpdateUI();
            battleCanvas.UpdateUI();
            optionsCanvas.UpdateUI();
            conformationCanvas.UpdateUI();
            OverlayCanvas.UpdateUI();
        }

        public void Clear() {
            notificationCanvas.SetActive(true);
            conformationCanvas.gameObject.SetActive(true);
            conformationCanvas.Clear();
        }
    }
}
