using System.Collections.Generic;
using cna.poo;
using UnityEngine;

namespace cna.ui {
    public class ScoreBoardCanvas : UIGameBase {

        [SerializeField] private ScoreCellPrefab prefab;
        [SerializeField] private AddressableImage[] playerFames;
        [SerializeField] private AddressableImage[] playerReps;
        [SerializeField] private ScoreCellPrefab[] fameCellList;
        [SerializeField] private RepCell[] repCellList;
        private int[] playerFameVal;
        private int[] playerRepVal;

        public override void SetupUI() {
            playerFameVal = new int[] { -1, -1, -1, -1 };
            playerRepVal = new int[] { -1, -1, -1, -1 };
            for (int i = 0; i < 4; i++) {
                playerFames[i].gameObject.SetActive(false);
                playerReps[i].gameObject.SetActive(false);
            }
            for (int i = 0; i < D.G.Players.Count; i++) {
                Image_Enum shieldId = D.AvatarMetaDataMap[D.G.Players[i].Avatar].AvatarShieldId;
                playerFames[i].ImageEnum = shieldId;
                playerReps[i].ImageEnum = shieldId;
                playerFames[i].gameObject.SetActive(true);
                playerReps[i].gameObject.SetActive(true);
            }
        }

        public void UpdateUI() {
            CheckSetupUI();
            List<PlayerData> p = D.G.Players;
            for (int i = 0; i < p.Count; i++) {
                int fame = p[i].TotalFame;
                int rep = p[i].RepLevel + 7;
                if (playerFameVal[i] != fame) {
                    playerFameVal[i] = fame;
                    UpdateUI_Fame(i);
                }
                if (playerRepVal[i] != rep) {
                    playerRepVal[i] = rep;
                    UpdateUI_Rep(i);
                }
            }
        }

        private void UpdateUI_Fame(int index) {
            playerFames[index].transform.SetParent(fameCellList[playerFameVal[index]].tokens.transform);
            playerFames[index].transform.localScale = Vector3.one;
        }
        private void UpdateUI_Rep(int index) {
            playerReps[index].transform.SetParent(repCellList[playerRepVal[index]].tokens.transform);
            playerReps[index].transform.localScale = Vector3.one;
        }


    }
}
