
using System.Collections.Generic;
using cna.poo;
using UnityEngine;

namespace cna.ui {
    public class DiscardPilePanel : BasePanel {
        [SerializeField] private NormalCardSlot normalCardSlot_Prefab;
        [SerializeField] private List<NormalCardSlot> cardSlots = new List<NormalCardSlot>();
        [SerializeField] private Transform content;

        public override void SetupUI(PlayerData pd) {
            foreach (NormalCardSlot n in cardSlots.ToArray()) {
                Destroy(n.gameObject);
                cardSlots.Remove(n);
            }
            D.LocalPlayer.Deck.Discard.ForEach(c => {
                NormalCardSlot normalCardSlot = Instantiate(normalCardSlot_Prefab, Vector3.zero, Quaternion.identity);
                normalCardSlot.transform.SetParent(content);
                normalCardSlot.transform.localScale = Vector3.one;
                normalCardSlot.SetupUI(pd, c, CardHolder_Enum.DiscardDeck);
                cardSlots.Add(normalCardSlot);
            });
        }

        public void OnClick_Close() {
            gameObject.SetActive(false);
        }

        public void OnClick_DiscardDeck() {
            if (gameObject.activeSelf) {
                gameObject.SetActive(false);
            } else {
                gameObject.SetActive(true);
                SetupUI(D.LocalPlayer);
            }
        }
    }
}
