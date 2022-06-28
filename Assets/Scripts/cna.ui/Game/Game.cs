using UnityEngine;

namespace cna.ui {
    public class Game : UIGameBase {

        [SerializeField] PlayerDisplay PlayerDisplay;
        [SerializeField] GameDisplay GameDisplay;
        [SerializeField] PlayerWorld PlayerWorld;

        public void UpdateUI() {
            CheckSetupUI();
            PlayerDisplay.UpdateUI();
            GameDisplay.UpdateUI();
            PlayerWorld.UpdateUI();
        }

        public override void Clear() {
            PlayerDisplay.Clear();
            GameDisplay.Clear();
            PlayerWorld.Clear();
        }
    }
}
