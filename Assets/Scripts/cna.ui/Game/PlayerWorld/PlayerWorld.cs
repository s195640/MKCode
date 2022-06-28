using cna.poo;
using UnityEngine;

namespace cna.ui {
    public class PlayerWorld : UIGameBase {
        [SerializeField] private PlayerGrid playerGrid;

        public override void SetupUI() {
            gameObject.SetActive(true);
        }

        public void UpdateUI() {
            CheckSetupUI();
            playerGrid.UpdateUI();
        }
    }
}
