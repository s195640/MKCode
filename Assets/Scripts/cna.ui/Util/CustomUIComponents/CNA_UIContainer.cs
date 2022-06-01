using System.Collections.Generic;
using UnityEngine;

namespace cna.ui {
    public class CNA_UIContainer<I, J> where I : MonoBehaviour, ICNA_UIPrefab<J> {
        private Transform content;
        private I prefab;
        private List<I> slots;

        public CNA_UIContainer(I prefab, Transform content) {
            this.prefab = prefab;
            this.content = content;
            slots = new List<I>();
        }
        public void UpdateUI(List<J> dataList) {
            dataList.ForEach(data => {
                I instance = slots.Find(obj => obj.Equals(data));
                if (instance == null) {
                    I slot = Object.Instantiate(prefab, Vector3.zero, Quaternion.identity);
                    slot.transform.SetParent(content);
                    slot.transform.localScale = Vector3.one;
                    slot.SetupUI(data);
                    slots.Add(slot);
                } else {
                    instance.UpdateUI();
                }
            });
            foreach (I instance in slots.ToArray()) {
                if (!dataList.Contains(instance.Data)) {
                    instance.Destroy();
                    slots.Remove(instance);
                }
            }
        }
    }
}
