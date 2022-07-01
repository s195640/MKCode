using System.Collections.Generic;
using cna.poo;
using UnityEngine;

namespace cna.ui {
    public class MonsterHandPanel : MonoBehaviour {
        [SerializeField] private List<MonsterCardSlot> cardSlots = new List<MonsterCardSlot>();
        [SerializeField] private MonsterCardSlot prefab;
        [SerializeField] private Transform content;

        public void UpdateUI(PlayerData pd, Dictionary<int, MonsterDetailsVO> monsterDetails, Vector3 scale, bool disableClick = false) {
            foreach (MonsterDetailsVO m in monsterDetails.Values) {
                MonsterCardSlot p = cardSlots.Find(p => p.MonsterDetails.UniqueId == m.UniqueId);
                if (p == null) {
                    MonsterCardSlot c = Instantiate(prefab, Vector3.zero, Quaternion.identity);
                    c.transform.SetParent(content);
                    c.transform.localScale = scale;
                    c.SetupUI(pd, m);
                    if (disableClick) {
                        c.UpdateUI_DisableClick();
                    }
                    cardSlots.Add(c);
                } else {
                    p.UpdateUI(pd, m);
                }
            }
            foreach (MonsterCardSlot n in cardSlots.ToArray()) {
                if (!monsterDetails.ContainsKey(n.MonsterDetails.UniqueId)) {
                    Destroy(n.gameObject);
                    cardSlots.Remove(n);
                }
            }
        }
    }
}
