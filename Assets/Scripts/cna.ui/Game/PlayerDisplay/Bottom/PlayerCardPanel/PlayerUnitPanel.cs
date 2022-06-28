using System.Collections.Generic;
using cna.poo;
using TMPro;
using UnityEngine;

namespace cna.ui {
    public class PlayerUnitPanel : MonoBehaviour {
        [SerializeField] private TextMeshProUGUI unitSizeText;
        [SerializeField] private NormalCardSlot normalCardSlot_Prefab;
        [SerializeField] private List<NormalCardSlot> cardSlots = new List<NormalCardSlot>();
        [SerializeField] private Transform content;

        public void UpdateUI() {
            unitSizeText.text = "Units (" + D.LocalPlayer.Deck.Unit.Count + " of " + D.LocalPlayer.Deck.UnitHandLimit + ")";

            D.LocalPlayer.Deck.Unit.ForEach(c => {
                NormalCardSlot p = cardSlots.Find(p => p.UniqueCardId == c);
                if (p == null) {
                    NormalCardSlot normalCardSlot = Instantiate(normalCardSlot_Prefab, Vector3.zero, Quaternion.identity);
                    normalCardSlot.transform.SetParent(content);
                    normalCardSlot.transform.localScale = Vector3.one;
                    normalCardSlot.SetupUI(c, CardHolder_Enum.PlayerUnitHand);
                    cardSlots.Add(normalCardSlot);
                } else {
                    p.UpdateUI();
                }
            });
            foreach (NormalCardSlot n in cardSlots.ToArray()) {
                if (!D.LocalPlayer.Deck.Unit.Contains(n.UniqueCardId)) {
                    if (n.ActionCard.UniqueCardId == n.UniqueCardId) {
                        n.ActionCard.SelectedCardSlot = null;
                    }
                    Destroy(n.gameObject);
                    cardSlots.Remove(n);
                }
            }
        }
    }
}
