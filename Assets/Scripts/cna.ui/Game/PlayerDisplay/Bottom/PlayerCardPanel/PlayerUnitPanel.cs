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

        public void UpdateUI(PlayerData pd, Vector3 scale, bool disableClick = false) {
            unitSizeText.text = "Units (" + pd.Deck.Unit.Count + " of " + pd.Deck.UnitHandLimit + ")";

            pd.Deck.Unit.ForEach(c => {
                NormalCardSlot p = cardSlots.Find(p => p.UniqueCardId == c);
                if (p == null) {
                    NormalCardSlot normalCardSlot = Instantiate(normalCardSlot_Prefab, Vector3.zero, Quaternion.identity);
                    normalCardSlot.transform.SetParent(content);
                    normalCardSlot.transform.localScale = scale;
                    normalCardSlot.SetupUI(pd, c, CardHolder_Enum.PlayerUnitHand);
                    if (disableClick) {
                        normalCardSlot.UpdateUI_DisableClick();
                    }
                    cardSlots.Add(normalCardSlot);
                } else {
                    p.UpdateUI(pd);
                }
            });
            foreach (NormalCardSlot n in cardSlots.ToArray()) {
                if (!pd.Deck.Unit.Contains(n.UniqueCardId)) {
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
