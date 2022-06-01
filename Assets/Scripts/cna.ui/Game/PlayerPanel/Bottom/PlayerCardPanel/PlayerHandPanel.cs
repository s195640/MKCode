using System.Collections.Generic;
using cna.poo;
using TMPro;
using UnityEngine;

namespace cna.ui {
    public class PlayerHandPanel : MonoBehaviour {
        [SerializeField] private TextMeshProUGUI PlayerHandText;
        [SerializeField] private NormalCardSlot normalCardSlot_Prefab;
        [SerializeField] private List<NormalCardSlot> cardSlots = new List<NormalCardSlot>();
        [SerializeField] private Transform content;

        public void UpdateUI() {
            UpdateUI_PlayerHandLimit(D.LocalPlayer.Deck.TotalHandSize);
            D.LocalPlayer.Deck.Hand.ForEach(c => {
                NormalCardSlot p = cardSlots.Find(p => p.UniqueCardId == c);
                if (p == null) {
                    NormalCardSlot normalCardSlot = Instantiate(normalCardSlot_Prefab, Vector3.zero, Quaternion.identity);
                    normalCardSlot.transform.SetParent(content);
                    normalCardSlot.transform.localScale = Vector3.one;
                    normalCardSlot.SetupUI(c, CardHolder_Enum.PlayerHand);
                    cardSlots.Add(normalCardSlot);
                } else {
                    p.UpdateUI();
                }
            });
            foreach (NormalCardSlot n in cardSlots.ToArray()) {
                if (!D.LocalPlayer.Deck.Hand.Contains(n.UniqueCardId)) {
                    if (n.ActionCard.UniqueCardId == n.UniqueCardId) {
                        n.ActionCard.SelectedCardSlot = null;
                    }
                    Destroy(n.gameObject);
                    cardSlots.Remove(n);
                }
            }
        }

        private void UpdateUI_PlayerHandLimit(int limit) {
            PlayerHandText.text = "Player Hand (Limit " + limit + ")";
        }
    }
}
