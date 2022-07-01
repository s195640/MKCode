using System;
using System.Collections;
using cna.poo;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace cna.ui {
    public class CNA_Button : ICNA_Base, IPointerEnterHandler, IPointerExitHandler {
        [SerializeField] private GameObject disableFilm;
        [SerializeField] private Image image;
        [SerializeField] private AddressableImage addrImage;
        [SerializeField] private GameObject textAndImage;
        [SerializeField] private TextMeshProUGUI textWithImage;
        [SerializeField] private TextMeshProUGUI textWithNoImage;

        [SerializeField] RectTransform _text;
        [SerializeField] RectTransform _parent;
        [SerializeField] RectTransform _image;

        [SerializeField] private bool active = true;
        private bool setPointer = false;
        private string text = "";
        [Header("Active")]
        [SerializeField] private Texture2D cursorActive;
        [SerializeField] private Vector2 hotspotActive = new Vector2(10, 0);

        [Header("Inactive")]
        [SerializeField] private Texture2D cursorInactive;
        [SerializeField] private Vector2 hotspotInactive = new Vector2(100, 100);

        [SerializeField] private Vector3 originalButtonPos;
        [SerializeField] private AddressableImage selected;

        private bool buttonIsShaking = false;
        private bool buttonIsBlinking = false;
        private int index = -1;
        private Action<int> onClickCallback;
        public bool Active { get => active; set { active = value; disableFilm.SetActive(!active); } }
        public Color ButtonColor { get => image.color; set => image.color = value; }
        public Color ButtonTextColor { get => textWithImage.color; set { textWithImage.color = value; textWithNoImage.color = value; } }
        public Image_Enum ButtonImageId { get => addrImage.ImageEnum; set => addrImage.ImageEnum = value; }
        public string ButtonText {
            get => text;
            set {
                text = value;
                textWithImage.text = text;
                textWithNoImage.text = text;
            }
        }

        public AddressableImage Selected { get => selected; set => selected = value; }

        public void Start() {
            SetupUI(addrImage.ImageEnum == Image_Enum.NA ? textWithNoImage.text : textWithImage.text, image.color, addrImage.ImageEnum, !disableFilm.activeSelf);
        }

        public override void SetupUI(string buttonText, Color buttonColor, Image_Enum buttonImageid = Image_Enum.NA, bool isActive = true) {
            if (Selected != null) {
                Selected.gameObject.SetActive(false);
            }
            ButtonText = buttonText;
            ButtonColor = buttonColor;
            Active = isActive;
            ButtonImageId = buttonImageid;

            if (buttonImageid == Image_Enum.NA) {
                textAndImage.SetActive(false);
                textWithNoImage.gameObject.SetActive(true);
            } else {
                textWithNoImage.gameObject.SetActive(false);
                textAndImage.SetActive(true);
                textWithImage.gameObject.SetActive(true);
                addrImage.gameObject.SetActive(true);
            }
        }

        public void SetupUI_SetSize(float w = 0f) {
            _text.anchorMin = new Vector2(1, 0);
            _text.anchorMax = new Vector2(1, 1);
            _text.offsetMax = new Vector2(0, 0);
            if (w > 0) {
                _text.offsetMin = new Vector2(-1 * w, 0);
            } else {
                float width = _parent.rect.width - _image.rect.width;
                _text.offsetMin = new Vector2(-1 * width, 0);
            }
        }


        public void UpdateUI_Image(Image_Enum buttonImageid) {
            SetupUI(ButtonText, ButtonColor, buttonImageid, Active);
        }
        public void UpdateUI_Text(string buttonText) {
            SetupUI(buttonText, ButtonColor, ButtonImageId, Active);
        }

        public void UpdateUI_TextAndImage(string buttonText, Image_Enum buttonImageid) {
            SetupUI(buttonText, ButtonColor, buttonImageid, Active);
        }
        //public void UpdateUI_TextWidthFromLeft(int width) {
        //    rectTransforms[1].sizeDelta = new Vector2(width, 0);
        //}

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
            Cursor.SetCursor(active ? cursorActive : cursorInactive, active ? hotspotActive : hotspotInactive, CursorMode.Auto);
            setPointer = true;
        }
        private void clearCustomPointer() {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
            setPointer = false;
        }

        public void ShakeButton() {
            if (!buttonIsShaking) {
                buttonIsShaking = true;
                originalButtonPos = transform.localPosition;
                StartCoroutine(ShakeCO(transform, originalButtonPos));
            }
        }

        private IEnumerator ShakeCO(Transform t, Vector3 originalPos) {
            float xdiff = 5f;
            for (int i = 0; i < 4; i++) {
                xdiff *= -1;
                t.localPosition = new Vector3(originalPos.x + xdiff, originalPos.y, originalPos.z);
                yield return new WaitForSeconds(.05f);
            }
            t.localPosition = originalPos;
            buttonIsShaking = false;
        }

        public void addButtonClick(int index, Action<int> onClickCallback) {
            this.index = index;
            this.onClickCallback = onClickCallback;
        }

        public void OnClick_Callback() {
            if (index >= 0) {
                onClickCallback(index);
            }
        }

        public void BlickButton(bool val) {
            if (buttonIsBlinking == val) {
                return;
            }
            buttonIsBlinking = val;
            if (buttonIsBlinking) {
                StartCoroutine(BlickCO(transform));
            }
        }

        private IEnumerator BlickCO(Transform t) {
            float scaleINC = .025f;
            float scaleVal = 1f;
            float scaleMax = 1.1f;
            float scaleMin = .9f;
            while (buttonIsBlinking) {
                scaleVal += scaleINC;
                if (scaleVal >= scaleMax || scaleVal <= scaleMin) {
                    scaleINC *= -1;
                }
                t.localScale = new Vector3(scaleVal, scaleVal, 1);
                yield return new WaitForSeconds(.05f);
            }
            t.localScale = Vector3.one;
        }

    }
}
