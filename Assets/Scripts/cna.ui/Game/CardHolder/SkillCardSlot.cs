using cna.poo;
using UnityEngine;

namespace cna.ui {
    public class SkillCardSlot : CardSlot {

        [SerializeField] private GameObject emptyCardContainer;
        [SerializeField] private AddressableImage emptyCardImage;
        [SerializeField] private GameObject cardContainer;
        [SerializeField] private GameObject actionTaken;
        [SerializeField] private AddressableImage cardImage;
        [SerializeField] private CardState_Enum cardState;

        public CardState_Enum CardState { get => cardState; set => cardState = value; }

        public override void SetupUI(int key, CardHolder_Enum holder) {
            CardHolder = holder;
            UniqueCardId = key;
            Card = D.Cards[UniqueCardId];
            SetupUI();
            UpdateUI();
        }

        private void SetupUI() {
            cardState = CardState_Enum.NA;
            emptyCardImage.ImageEnum = Card.SkillBackCardId;
            cardImage.ImageEnum = Card.CardImage;
            actionTaken.SetActive(false);
            if (CardHolder == CardHolder_Enum.NA) {
                cardContainer.SetActive(false);
                emptyCardContainer.SetActive(true);
            } else {
                cardContainer.SetActive(true);
                emptyCardContainer.SetActive(false);
            }
        }

        public override void UpdateUI() {
            if (CardHolder == CardHolder_Enum.PlayerSkillHand) {
                if (UpdateCardState()) {
                    actionTaken.SetActive(cardState != CardState_Enum.NA);
                }
            }
        }

        private bool UpdateCardState() {
            bool update = false;
            CNAMap<int, WrapList<CardState_Enum>> states = D.LocalPlayer.Deck.State;
            if (states.ContainsKey(UniqueCardId) && states[UniqueCardId].Values.Count > 0) {
                if (states[UniqueCardId].Values[0] != cardState) {
                    cardState = states[UniqueCardId].Values[0];
                    update = true;
                }
            } else {
                if (cardState != CardState_Enum.NA) {
                    update = true;
                    cardState = CardState_Enum.NA;
                }
            }
            return update;
        }

        public override void OnClickCard() {
            ActionCard.SelectedCardSlot = this;
        }
    }
}
