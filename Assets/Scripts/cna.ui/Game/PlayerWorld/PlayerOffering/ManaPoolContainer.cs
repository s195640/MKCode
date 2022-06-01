using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace cna.ui {

    public class ManaPoolContainer : MonoBehaviour {
        [SerializeField] private List<ManaDiePrefab> slots = new List<ManaDiePrefab>();
        [SerializeField] private TextMeshProUGUI ManaPoolAvailable;

        public void UpdateUI() {
            for (int i = 0; i < 10; i++) {
                slots[i].gameObject.SetActive(false);
            }
            for (int i = 0; i < D.G.Board.ManaPool.Count; i++) {
                slots[i].gameObject.SetActive(true);
                slots[i].SetupUI(D.G.Board.ManaPool[i], i);
            }
            int manaAvailable = D.LocalPlayer.ManaPoolAvailable;
            ManaPoolAvailable.text = "(Available " + manaAvailable + ")";
        }
    }
}
