using System.Collections.Generic;
using cna.poo;
using UnityEngine;

namespace cna.ui {
    public class PlayerSkillPanel : MonoBehaviour {
        [SerializeField] private SkillCardSlot prefab;
        [SerializeField] private List<SkillCardSlot> cardSlots = new List<SkillCardSlot>();
        [SerializeField] private Transform content;

        public void UpdateUI() {
            D.LocalPlayer.Deck.Skill.ForEach(c => {
                SkillCardSlot p = cardSlots.Find(p => p.UniqueCardId == c);
                if (p == null) {
                    SkillCardSlot normalCardSlot = Instantiate(prefab, Vector3.zero, Quaternion.identity);
                    normalCardSlot.transform.SetParent(content);
                    normalCardSlot.transform.localScale = Vector3.one;
                    normalCardSlot.SetupUI(c, CardHolder_Enum.PlayerSkillHand);
                    cardSlots.Add(normalCardSlot);
                } else {
                    p.UpdateUI();
                }
            });
            foreach (SkillCardSlot n in cardSlots.ToArray()) {
                if (!D.LocalPlayer.Deck.Skill.Contains(n.UniqueCardId)) {
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
