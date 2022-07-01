using System.Collections;
using System.Collections.Generic;
using cna.poo;
using TMPro;
using UnityEngine;

namespace cna.ui {
    public class PlayerDetailsContainerPrefab : MonoBehaviour {

        private Vector3 scale = new Vector3(.7f, .7f, 1f);

        [SerializeField] private AddressableImage avatarShield;
        [SerializeField] private TextMeshProUGUI avatarName;

        [SerializeField] private PlayerDeckPanel playerDeckPanel;
        [SerializeField] private PlayerHandPanel playerHandPanel;
        [SerializeField] private PlayerUnitPanel playerUnitPanel;
        [SerializeField] private PlayerSkillPanel playerSkillPanel;
        [SerializeField] private PlayerDetailsBattleContainer PlayerDetailsBattleContainer;

        private int playerKey = 0;

        public void SetupUI(int playerKey) {
            this.playerKey = playerKey;
            PlayerData pd = D.GetPlayerByKey(playerKey);
            avatarName.text = pd.Name;
            avatarShield.ImageEnum = D.AvatarMetaDataMap[pd.Avatar].AvatarShieldId;
        }

        public void UpdateUI() {
            PlayerData pd = D.GetPlayerByKey(playerKey);
            playerDeckPanel.UpdateUI(pd, true);
            playerHandPanel.UpdateUI(pd, scale, true);
            playerUnitPanel.UpdateUI(pd, scale, true);
            playerSkillPanel.UpdateUI(pd, true);
            PlayerDetailsBattleContainer.UpdateUI(pd);
        }

    }
}
