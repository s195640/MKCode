using UnityEngine;
using UnityEngine.EventSystems;

namespace cna.ui {
    public class CustomCursor : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
        [SerializeField] private Texture2D cursor;
        [SerializeField] private Vector2 hotspot = new Vector2(10, 0);


        private bool setPointer = false;

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

        private void setCustomPointer() {
            Cursor.SetCursor(cursor, hotspot, CursorMode.Auto);
            setPointer = true;
        }
        private void clearCustomPointer() {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
            setPointer = false;
        }
    }
}
