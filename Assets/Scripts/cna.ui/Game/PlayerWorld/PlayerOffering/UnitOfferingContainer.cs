using System.Collections.Generic;
using cna.poo;
using UnityEngine;

namespace cna.ui {
    public class UnitOfferingContainer : MonoBehaviour {

        [SerializeField] private NormalCardSlot normalCardSlot_Prefab;
        [SerializeField] private Transform unitContent;
        [SerializeField] private List<NormalCardSlot> cardSlots = new List<NormalCardSlot>();

        public void UpdateUI() {
            D.G.Board.UnitOffering.ForEach(c => {
                NormalCardSlot p = cardSlots.Find(p => p.UniqueCardId == c);
                if (p == null) {
                    NormalCardSlot normalCardSlot = Instantiate(normalCardSlot_Prefab, Vector3.zero, Quaternion.identity);
                    normalCardSlot.transform.SetParent(unitContent);
                    normalCardSlot.transform.localScale = Vector3.one;
                    normalCardSlot.SetupUI(c, CardHolder_Enum.UnitOffering);
                    cardSlots.Add(normalCardSlot);
                } else {
                    p.UpdateUI();
                }
            });
            foreach (NormalCardSlot n in cardSlots.ToArray()) {
                if (!D.G.Board.UnitOffering.Contains(n.UniqueCardId)) {
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
