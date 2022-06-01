using System;
using cna.poo;
using TMPro;
using UnityEngine;

namespace cna.ui {
    public class SelectOptionsPanel : BasePanel {
        [SerializeField] private TextMeshProUGUI cardTitleText;
        [SerializeField] private TextMeshProUGUI actionDescriptionText;
        [SerializeField] private CNA_Button[] acceptButton;
        [SerializeField] private GameObject cancelButton;

        private CardActionVO card;
        private Action<ActionResultVO> cancelCallback;
        private Action<ActionResultVO> acceptCallback;
        private ActionResultVO ar;

        public void SetupUI(ActionResultVO ar, Action<ActionResultVO> cancelCallback, Action<ActionResultVO> acceptCallback, params OptionVO[] options) {
            if (cancelCallback == null) {
                cancelButton.SetActive(false);
            } else {
                cancelButton.SetActive(true);
                this.cancelCallback = cancelCallback;
            }

            this.acceptCallback = acceptCallback;
            this.ar = ar;
            for (int i = 0; i < 10; i++) {
                acceptButton[i].gameObject.SetActive(false);
            }
            for (int i = 0; i < options.Length; i++) {
                acceptButton[i].gameObject.SetActive(true);
                acceptButton[i].UpdateUI_TextAndImage(options[i].Text, options[i].Image);
            }
            gameObject.SetActive(true);
            card = (CardActionVO)D.Cards[ar.UniqueCardId];
            cardTitleText.text = card.CardTitle;
            actionDescriptionText.text = card.Actions[ar.ActionIndex];
        }

        public void OnClick_Accept(int index) {
            ar.SelectedButtonIndex = index;
            gameObject.SetActive(false);
            acceptCallback(ar);
        }

        public void OnClick_Cancel() {
            gameObject.SetActive(false);
            cancelCallback(ar);
        }
    }
}
