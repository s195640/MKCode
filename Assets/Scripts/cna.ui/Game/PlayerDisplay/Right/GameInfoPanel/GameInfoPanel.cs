using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace cna.ui {
    public class GameInfoPanel : UIGameBase {
        [SerializeField] private PlayerInfoPrefab playerInfoPrefab_Prefab;
        [SerializeField] private Transform panel;
        private List<PlayerInfoPrefab> playerPanelList = new List<PlayerInfoPrefab>();
        private List<int> currentOrder = new List<int>();

        public override void SetupUI() {
            playerPanelList.ForEach(p => Destroy(p.gameObject));
            playerPanelList.Clear();
            D.G.Players.ForEach(p => {
                PlayerInfoPrefab i = Instantiate(playerInfoPrefab_Prefab, Vector3.zero, Quaternion.identity);
                i.transform.SetParent(panel);
                i.transform.SetAsFirstSibling();
                i.transform.localScale = Vector3.one;
                i.SetupUI(p.Key);
                playerPanelList.Add(i);
            });
        }

        public void UpdateUI() {
            CheckSetupUI();
            playerPanelList.ForEach(p => p.UpdateUI());
            reOrder();
        }



        private void reOrder() {
            if (!Enumerable.SequenceEqual(D.G.Board.PlayerTurnOrder, currentOrder)) {
                currentOrder.Clear();
                currentOrder.AddRange(D.G.Board.PlayerTurnOrder);
                for (int i = currentOrder.Count - 1; i >= 0; i--) {
                    playerPanelList.Find(pi => pi.playerKey == currentOrder[i]).transform.SetAsFirstSibling();
                }
            }
        }
    }
}
