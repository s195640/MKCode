using System;
using System.Collections.Generic;
using cna.poo;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace cna.ui {
    public class SelectCardsPanel : BasePanel {

        [SerializeField] ActionCardSlot ActionCard;
        [SerializeField] private TextMeshProUGUI TitleText;
        [SerializeField] private TextMeshProUGUI DescText;
        [SerializeField] private NormalCardSlot cardSlot_Prefab;
        [SerializeField] private CNA_Button button_Prefab;
        [SerializeField] private List<NormalCardSlot> cardSlots = new List<NormalCardSlot>();
        [SerializeField] private Transform content;
        [SerializeField] private Transform buttonGroup;


        private GameAPI ar;
        private List<int> cards;
        private string title;
        private V2IntVO selectCount;
        private List<int> selectedCards;
        private List<CNA_Button> buttonSlots = new List<CNA_Button>();
        private List<Action<GameAPI>> buttonCallback;
        private List<bool> buttonForce;


        public void SetupUI(GameAPI ar, List<int> cards, string title, string description, V2IntVO selectCount, Image_Enum selectionImage, List<string> buttonText, List<Color> buttonColor, List<Action<GameAPI>> buttonCallback, List<bool> buttonForce) {
            gameObject.SetActive(true);
            selectedCards = new List<int>();
            this.ar = ar;
            this.cards = cards;
            this.title = title;
            this.selectCount = selectCount;
            this.buttonCallback = buttonCallback;
            this.buttonForce = buttonForce;
            DescText.text = description;
            UpdateUI_CardTitle();
            cardSlots.ForEach(c => Destroy(c.gameObject));
            cardSlots.Clear();
            this.cards.ForEach(c => {
                NormalCardSlot cardSlot = Instantiate(cardSlot_Prefab, Vector3.zero, Quaternion.identity);
                cardSlot.transform.SetParent(content);
                cardSlot.transform.localScale = Vector3.one;
                cardSlot.SetupUI(c, CardHolder_Enum.Rewards);
                cardSlot.SelectionImage.ImageEnum = selectionImage;
                cardSlot.GetComponent<Button>().onClick.AddListener(() => {
                    OnClick_SelectCard(cardSlot);
                });
                cardSlots.Add(cardSlot);
            });
            buttonSlots.ForEach(c => Destroy(c.gameObject));
            buttonSlots.Clear();
            for (int i = 0; i < buttonText.Count; i++) {
                CNA_Button buttonSlot = Instantiate(button_Prefab, Vector3.zero, Quaternion.identity);
                buttonSlot.transform.SetParent(buttonGroup);
                buttonSlot.transform.localScale = Vector3.one;
                buttonSlot.SetupUI(buttonText[i], buttonColor[i]);
                buttonSlot.addButtonClick(i, OnClick_Button);
                buttonSlots.Add(buttonSlot);
            }

        }

        private void UpdateUI_CardTitle() {
            TitleText.text = title + " (" + selectedCards.Count + " of " + selectCount.Y + ")";
        }

        public void OnClick_Button(int i) {
            if (buttonForce[i]) {
                if (selectedCards.Count >= selectCount.X) {
                    ar.SelectedCardIds = selectedCards;
                    gameObject.SetActive(false);
                    buttonCallback[i](ar);
                } else {
                    ActionCard.Msg("You must select at least " + selectCount.X + " cards!");
                    buttonSlots[i].ShakeButton();
                }
            } else {
                ar.SelectedCardIds = selectedCards;
                gameObject.SetActive(false);
                buttonCallback[i](ar);
            }
        }

        public void OnClick_SelectCard(NormalCardSlot cardSlot) {
            if (selectedCards.Contains(cardSlot.UniqueCardId)) {
                selectedCards.Remove(cardSlot.UniqueCardId);
                cardSlot.SpecialCardSelection.SetActive(false);
            } else {
                if (selectedCards.Count < selectCount.Y) {
                    selectedCards.Add(cardSlot.UniqueCardId);
                    cardSlot.SpecialCardSelection.SetActive(true);
                } else {
                    ActionCard.Msg("You have already selected the max number of cards!");
                }
            }
            UpdateUI_CardTitle();
        }








        //public void SetupUI(ActionResultVO ar, Action<ActionResultVO>[] acceptCallbacks, List<int> cards, string title, string description) {
        //    //this.ar = ar;
        //    //acceptCallback = acceptCallbacks[0];
        //    //if (acceptCallbacks.Length > 1) {
        //    //    noneCallback = acceptCallbacks[1];
        //    //    none.gameObject.SetActive(true);
        //    //    if (acceptCallbacks.Length > 2) {
        //    //        accept2Callback = acceptCallbacks[2];
        //    //        accept2.gameObject.SetActive(true);
        //    //    } else {
        //    //        accept2.gameObject.SetActive(false);
        //    //    }
        //    //} else {
        //    //    none.gameObject.SetActive(false);
        //    //}
        //    //this.cards = cards;
        //    //DescText.text = description;
        //    //TitleText.text = title;
        //    //gameObject.SetActive(true);

        //    //cardSlots.ForEach(c => Destroy(c.gameObject));
        //    //cardSlots.Clear();
        //    //this.cards.ForEach(c => {
        //    //    NormalCardSlot normalCardSlot = Instantiate(cardSlot_Prefab, Vector3.zero, Quaternion.identity);
        //    //    normalCardSlot.transform.SetParent(content);
        //    //    normalCardSlot.transform.localScale = Vector3.one;
        //    //    normalCardSlot.SetupUI(c, CardHolder_Enum.Rewards);
        //    //    cardSlots.Add(normalCardSlot);
        //    //});
        //}

        //public void OnClick_Accept() {
        //    if (ActionCard.SelectedCardSlot != null && ActionCard.SelectedCardSlot.CardHolder == CardHolder_Enum.Rewards) {
        //        gameObject.SetActive(false);
        //        ar.SelectedUniqueCardId = ActionCard.SelectedCardSlot.UniqueCardId;
        //        ActionCard.SelectedCardSlot = null;
        //        acceptCallback(ar);
        //    } else {
        //        ActionCard.Msg("No valid card has been selected!");
        //        accept.ShakeButton();
        //    }
        //}

        //public void OnClick_Accept2() {
        //    if (ActionCard.SelectedCardSlot != null && ActionCard.SelectedCardSlot.CardHolder == CardHolder_Enum.Rewards) {
        //        gameObject.SetActive(false);
        //        ar.SelectedUniqueCardId = ActionCard.SelectedCardSlot.UniqueCardId;
        //        ActionCard.SelectedCardSlot = null;
        //        accept2Callback(ar);
        //    } else {
        //        ActionCard.Msg("No valid card has been selected!");
        //        accept2.ShakeButton();
        //    }
        //}

        //public void OnClick_None() {
        //    gameObject.SetActive(false);
        //    ar.SelectedUniqueCardId = 0;
        //    noneCallback(ar);
        //}
    }
}
