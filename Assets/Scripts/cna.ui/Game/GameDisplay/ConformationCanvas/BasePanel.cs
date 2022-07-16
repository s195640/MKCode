using cna.poo;
using UnityEngine;

namespace cna.ui {
    public abstract class BasePanel : MonoBehaviour {
        [SerializeField] private ConformationCanvas ConformationCanvas;
        public bool Active { get => gameObject.activeSelf; set => gameObject.SetActive(value); }
        public virtual void SetupUI(PlayerData pd) { }

        public virtual void UpdateUI() { }

        public void Clear() {
            ConformationCanvas.Clear();
        }
    }
}
