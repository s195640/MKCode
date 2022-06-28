using UnityEngine;

namespace cna.ui {
    public class PlayerCardPanel : UIGameBase {

        [SerializeField] private PlayerDeckPanel playerDeckPanel;
        [SerializeField] private PlayerHandPanel playerHandPanel;
        [SerializeField] private PlayerUnitPanel playerUnitPanel;
        [SerializeField] private PlayerSkillPanel playerSkillPanel;


        public void UpdateUI() {
            playerDeckPanel.UpdateUI();
            playerHandPanel.UpdateUI();
            playerUnitPanel.UpdateUI();
            playerSkillPanel.UpdateUI();
        }
    }
}
