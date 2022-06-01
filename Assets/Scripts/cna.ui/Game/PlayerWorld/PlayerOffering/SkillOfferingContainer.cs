using System.Collections.Generic;
using cna.poo;
using UnityEngine;

namespace cna.ui {
    public class SkillOfferingContainer : MonoBehaviour {
        [SerializeField] private SkillCardSlot prefab;
        [SerializeField] private Transform content;
        [SerializeField] private List<SkillCardSlot> slots = new List<SkillCardSlot>();

        public void UpdateUI() {
            D.G.Board.SkillOffering.ForEach(c => {
                SkillCardSlot p = slots.Find(p => p.UniqueCardId == c);
                if (p == null) {
                    SkillCardSlot cardSlot = Instantiate(prefab, Vector3.zero, Quaternion.identity);
                    cardSlot.transform.SetParent(content);
                    cardSlot.transform.localScale = Vector3.one;
                    cardSlot.SetupUI(c, CardHolder_Enum.SkillOffering);
                    slots.Add(cardSlot);
                } else {
                    p.UpdateUI();
                }
            });
            foreach (SkillCardSlot n in slots.ToArray()) {
                if (!D.G.Board.SkillOffering.Contains(n.UniqueCardId)) {
                    if (n.ActionCard.UniqueCardId == n.UniqueCardId) {
                        n.ActionCard.SelectedCardSlot = null;
                    }
                    Destroy(n.gameObject);
                    slots.Remove(n);
                }
            }
        }
    }
}
