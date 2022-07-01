using System;
using cna.poo;
using TMPro;
using UnityEngine;

namespace cna.ui {
    public class SelectSingleCardPanel : BasePanel {

        [SerializeField] private NormalCardSlot NormalCardSlot;
        [SerializeField] private TextMeshProUGUI cardTitleText;
        [SerializeField] private TextMeshProUGUI actionDescriptionText;
        [SerializeField] private CNA_Button acceptButton;
        [SerializeField] private NotificationCanvas NotificationCanvas;
        [SerializeField] private GameObject None;

        private CardActionVO card;
        private Action<GameAPI> cancelCallback;
        private Action<GameAPI> acceptCallback;
        private GameAPI ar;
        private bool allowNone = false;

        public void SetupUI(GameAPI ar, Action<GameAPI> cancelCallback, Action<GameAPI> acceptCallback, bool allowNone = false) {
            this.allowNone = allowNone;
            None.SetActive(this.allowNone);
            this.cancelCallback = cancelCallback;
            this.acceptCallback = acceptCallback;
            this.ar = ar;
            gameObject.SetActive(true);
            if (ar.SelectedUniqueCardId > 0) {
                card = (CardActionVO)D.Cards[ar.SelectedUniqueCardId];
            } else {
                card = (CardActionVO)D.Cards[ar.UniqueCardId];
            }
            ResetUI();
            cardTitleText.text = card.CardTitle;
            actionDescriptionText.text = card.Actions[ar.ActionIndex];
        }

        public void ResetUI() {
            NormalCardSlot.SetupUI(ar.P, 0, CardHolder_Enum.NA);
        }

        #region Actions
        public void OnClick_SelectCard(NormalCardSlot cardSlot) {
            string msg = card.IsSelectionAllowed(cardSlot.Card, cardSlot.CardHolder, ar);
            if (msg.Length == 0) {
                NormalCardSlot.SetupUI(ar.P, cardSlot.Card.UniqueId, cardSlot.CardHolder);
            } else {
                Message(msg);
            }
        }

        public void OnClick_Clear() {
            ResetUI();
        }

        public void OnClick_Accept() {
            if (NormalCardSlot.Card.UniqueId > 0) {
                gameObject.SetActive(false);
                ar.SelectedUniqueCardId = NormalCardSlot.Card.UniqueId;
                acceptCallback(ar);
            } else {
                Message("No valid card has been selected!");
                acceptButton.ShakeButton();
            }
        }

        public void OnClick_Cancel() {
            gameObject.SetActive(false);
            cancelCallback(ar);
        }

        public void OnClick_None() {
            gameObject.SetActive(false);
            ar.SelectedUniqueCardId = 0;
            acceptCallback(ar);
        }

        #endregion

        private void Message(string msg) {
            NotificationCanvas.Msg(msg);
        }
    }
}
