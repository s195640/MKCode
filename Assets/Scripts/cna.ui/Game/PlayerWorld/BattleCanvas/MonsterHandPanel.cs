using System.Collections.Generic;
using UnityEngine;

namespace cna.ui {
    public class MonsterHandPanel : MonoBehaviour {
        [SerializeField] private List<MonsterCardSlot> cardSlots = new List<MonsterCardSlot>();
        [SerializeField] private MonsterCardSlot prefab;
        [SerializeField] private Transform content;

        public void UpdateUI() {
            foreach (MonsterDetailsVO m in D.B.MonsterDetails.Values) {
                MonsterCardSlot p = cardSlots.Find(p => p.MonsterDetails.UniqueId == m.UniqueId);
                if (p == null) {
                    MonsterCardSlot c = Instantiate(prefab, Vector3.zero, Quaternion.identity);
                    c.transform.SetParent(content);
                    c.transform.localScale = Vector3.one;
                    c.SetupUI(m);
                    cardSlots.Add(c);
                } else {
                    p.UpdateUI(m);
                }
            }
            foreach (MonsterCardSlot n in cardSlots.ToArray()) {
                if (!D.B.MonsterDetails.ContainsKey(n.MonsterDetails.UniqueId)) {
                    Destroy(n.gameObject);
                    cardSlots.Remove(n);
                }
            }
        }
    }
}
