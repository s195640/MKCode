using UnityEngine;

namespace cna.ui {
    public abstract class BasePanel : MonoBehaviour {
        [SerializeField] private ConformationCanvas ConformationCanvas;
        public bool Active { get => gameObject.activeSelf; set => gameObject.SetActive(value); }
        public virtual void SetupUI() { }

        public virtual void UpdateUI() { }
    }
}
