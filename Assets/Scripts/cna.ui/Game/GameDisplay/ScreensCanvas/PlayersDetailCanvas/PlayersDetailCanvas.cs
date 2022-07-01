using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace cna.ui {
    public class PlayersDetailCanvas : UIGameBase {

        [SerializeField] private Transform content;
        [SerializeField] private List<PlayerDetailsContainerPrefab> PlayerDetails;

        public override void SetupUI() {
            int totalPlayers = D.G.Players.Count - (D.GLD.DummyPlayer ? 1 : 0);

            PlayerDetails.ForEach(p => p.gameObject.SetActive(false));

            int index = 0;
            D.G.Players.ForEach(p => {
                if (!p.DummyPlayer && p.Key != D.LocalPlayerKey) {
                    PlayerDetails[index].gameObject.SetActive(true);
                    PlayerDetails[index].SetupUI(p.Key);
                    index++;
                }
                if (totalPlayers < 3 && p.Key == D.LocalPlayerKey) {
                    PlayerDetails[index].gameObject.SetActive(true);
                    PlayerDetails[index].SetupUI(p.Key);
                    index++;
                }
            });
        }

        public void UpdateUI() {
            CheckSetupUI();
            PlayerDetails.ForEach(p => {
                if (p.gameObject.activeSelf) {
                    p.UpdateUI();
                }
            });
        }
    }
}
