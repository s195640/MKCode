using cna.poo;
using UnityEngine;

namespace cna.ui {
    public class PlayerCardPanel : UIGameBase {

        [SerializeField] private PlayerDeckPanel playerDeckPanel;
        [SerializeField] private PlayerHandPanel playerHandPanel;
        [SerializeField] private PlayerUnitPanel playerUnitPanel;
        [SerializeField] private PlayerSkillPanel playerSkillPanel;


        public void UpdateUI() {
            PlayerData pd = D.LocalPlayer;
            playerDeckPanel.UpdateUI(pd);
            playerHandPanel.UpdateUI(pd, Vector3.one);
            playerUnitPanel.UpdateUI(pd, Vector3.one);
            playerSkillPanel.UpdateUI(pd);
        }
    }
}
