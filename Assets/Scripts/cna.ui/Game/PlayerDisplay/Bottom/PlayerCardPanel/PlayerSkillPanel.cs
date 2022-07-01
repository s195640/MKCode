using System.Collections.Generic;
using cna.poo;
using UnityEngine;

namespace cna.ui {
    public class PlayerSkillPanel : MonoBehaviour {
        [SerializeField] private SkillCardSlot prefab;
        [SerializeField] private List<SkillCardSlot> cardSlots = new List<SkillCardSlot>();
        [SerializeField] private Transform content;

        public void UpdateUI(PlayerData pd, bool disableClick = false) {
            pd.Deck.Skill.ForEach(c => {
                SkillCardSlot p = cardSlots.Find(p => p.UniqueCardId == c);
                if (p == null) {
                    SkillCardSlot normalCardSlot = Instantiate(prefab, Vector3.zero, Quaternion.identity);
                    normalCardSlot.transform.SetParent(content);
                    normalCardSlot.transform.localScale = Vector3.one;
                    normalCardSlot.SetupUI(pd, c, CardHolder_Enum.PlayerSkillHand);
                    if (disableClick) {
                        normalCardSlot.UpdateUI_DisableClick();
                    }
                    cardSlots.Add(normalCardSlot);
                } else {
                    p.UpdateUI(pd);
                }
            });
            foreach (SkillCardSlot n in cardSlots.ToArray()) {
                if (!pd.Deck.Skill.Contains(n.UniqueCardId)) {
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
