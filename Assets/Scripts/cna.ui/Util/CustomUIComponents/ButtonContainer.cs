using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace cna.ui {
    public class ButtonContainer : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
        [SerializeField] private bool active = true;
        private bool setPointer = false;
        [SerializeField] private GameObject disableFilm;
        [SerializeField] private Image image;

        [Header("Active")]
        [SerializeField] private Texture2D cursorActive;
        [SerializeField] private Vector2 hotspotActive = new Vector2(10, 0);

        [Header("Inactive")]
        [SerializeField] private Texture2D cursorInactive;
        [SerializeField] private Vector2 hotspotInactive = new Vector2(16, 16);

        private Vector3 originalButtonPos;

        public bool Active { get => active; set { active = value; disableFilm.SetActive(!active); } }

        public Color ButtonBolor { get => image.color; set => image.color = value; }

        public void OnPointerEnter(PointerEventData eventData) {
            setCustomPointer();
        }
        public void OnPointerExit(PointerEventData eventData) {
            clearCustomPointer();
        }

        public void OnDisable() {
            if (setPointer) {
                clearCustomPointer();
            }
        }

        public void OnDestroy() {
            if (setPointer) {
                clearCustomPointer();
            }
        }
        public void Awake() {
            originalButtonPos = transform.localPosition;
        }
        private void setCustomPointer() {
            Cursor.SetCursor(active ? cursorActive : cursorInactive, active ? hotspotActive : hotspotInactive, CursorMode.Auto);
            setPointer = true;
        }
        private void clearCustomPointer() {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
            setPointer = false;
        }

        public void ShakeButton() {
            StartCoroutine(UIUtil.ShakeCO(transform, originalButtonPos));
        }
    }
}
