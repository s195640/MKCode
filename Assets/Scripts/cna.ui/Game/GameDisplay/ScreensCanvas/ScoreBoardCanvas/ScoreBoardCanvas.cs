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
        private List<int> playerKeys = new List<int>();
        [SerializeField] private GameObject[] blockFame;

        public override void SetupUI() {
            playerFameVal = new int[] { -1, -1, -1, -1 };
            playerRepVal = new int[] { -1, -1, -1, -1 };
            playerKeys.Clear();
            for (int i = 0; i < 4; i++) {
                playerFames[i].gameObject.SetActive(false);
                playerReps[i].gameObject.SetActive(false);
            }
            D.G.Players.ForEach(p => {
                if (!p.DummyPlayer) {
                    Image_Enum shieldId = D.AvatarMetaDataMap[p.Avatar].AvatarShieldId;
                    playerFames[playerKeys.Count].ImageEnum = shieldId;
                    playerReps[playerKeys.Count].ImageEnum = shieldId;
                    playerFames[playerKeys.Count].gameObject.SetActive(true);
                    playerReps[playerKeys.Count].gameObject.SetActive(true);
                    playerKeys.Add(p.Key);
                }
            });
            blockFame[0].SetActive(D.G.GameData.FamePerLevel > 0);
            blockFame[1].SetActive(D.G.GameData.FamePerLevel > 1);
        }

        public void UpdateUI() {
            CheckSetupUI();
            for (int i = 0; i < playerKeys.Count; i++) {
                PlayerData p = D.GetPlayerByKey(playerKeys[i]);
                int fame = BasicUtil.GetPlayerTotalFame(p.Fame, D.G.GameData.FamePerLevel);
                if (fame > 119) {
                    fame = 119;
                }
                int rep = p.RepLevel + 7;
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
