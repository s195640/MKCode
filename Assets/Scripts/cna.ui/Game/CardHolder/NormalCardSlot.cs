using System.Collections.Generic;
using System.Linq;
using cna.poo;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace cna.ui {
    public class NormalCardSlot : CardSlot {


        [SerializeField] private List<CardState_Enum> cardState;
        [SerializeField] private int banner;
        [SerializeField] private GameObject emptyCardContainer;
        [SerializeField] private GameObject cardContainer;
        [SerializeField] private GameObject actionTaken;
        [SerializeField] private TextMeshProUGUI actionTakenText;
        [SerializeField] private TextMeshProUGUI cardTitle;
        [SerializeField] private AddressableImage cardImage;
        [SerializeField] private UnitSlotContainer unitSlot;



        [Header("Basic/Adanced/Wound Card")]
        [SerializeField] private GameObject cardSlotContainer;
        [SerializeField] private GameObject actionContainer;

        [SerializeField] private TextMeshProUGUI normalText;
        [SerializeField] private TextMeshProUGUI advancedText;
        [SerializeField] private Image normalColor;
        [SerializeField] private Image advancedColor;

        [Header("Spell Card")]
        [SerializeField] private GameObject SpellSlotContainer;
        [SerializeField] private ICNA_Base[] SpellButtons;
        [SerializeField] private TextMeshProUGUI[] SpellTitleText;

        [Header("Tactics Card")]
        [SerializeField] private GameObject TacticsSlotContainer;
        [SerializeField] private AddressableImage TacticsCardImage;
        [SerializeField] private GameObject TacticsSelectedContainer;
        [SerializeField] private AddressableImage TacticsSelectedAvatarImage;

        [Header("SpecialCardSelection")]
        [SerializeField] public GameObject SpecialCardSelection;
        [SerializeField] public AddressableImage SelectionImage;

        protected GameObject EmptyCardContainer { get => emptyCardContainer; set => emptyCardContainer = value; }
        protected GameObject CardContainer { get => cardContainer; set => cardContainer = value; }
        public GameObject ActionTaken { get => actionTaken; set => actionTaken = value; }
        public TextMeshProUGUI ActionTakenText { get => actionTakenText; set => actionTakenText = value; }
        protected TextMeshProUGUI CardTitle { get => cardTitle; set => cardTitle = value; }
        protected AddressableImage CardImage { get => cardImage; set => cardImage = value; }
        public GameObject CardSlotContainer { get => cardSlotContainer; set => cardSlotContainer = value; }

        //  Constant Values

        //  Changing Values
        public List<CardState_Enum> CardState { get => cardState; }
        public int Banner { get => banner; set => banner = value; }

        public override void SetupUI(PlayerData pd, int key, CardHolder_Enum holder) {
            CardHolder = holder;
            UniqueCardId = key;
            Card = D.Cards[UniqueCardId];
            SetupUI();
            UpdateUI(pd);
        }

        private void SetupUI() {
            SpecialCardSelection.SetActive(false);
            cardState = new List<CardState_Enum>();
            banner = 0;
            if (CardHolder == CardHolder_Enum.NA || UniqueCardId == 0) {
                cardContainer.gameObject.SetActive(false);
                EmptyCardContainer.gameObject.SetActive(true);
            } else {
                actionTaken.SetActive(false);
                cardContainer.gameObject.SetActive(true);
                EmptyCardContainer.gameObject.SetActive(false);
                switch (Card.CardType) {
                    case CardType_Enum.Artifact:
                    case CardType_Enum.Basic:
                    case CardType_Enum.Advanced: {
                        SetupUI_CardSlot();
                        break;
                    }
                    case CardType_Enum.Unit_Normal:
                    case CardType_Enum.Unit_Elite: {
                        CardSlotContainer.SetActive(false);
                        TacticsSlotContainer.SetActive(false);
                        SpellSlotContainer.SetActive(false);
                        actionContainer.SetActive(true);
                        unitSlot.gameObject.SetActive(true);
                        unitSlot.SetupUI(Card, CardState, D.AvatarMetaDataMap[D.LocalPlayer.Avatar].AvatarShieldId);
                        break;
                    }
                    case CardType_Enum.Wound: {
                        unitSlot.gameObject.SetActive(false);
                        SpellSlotContainer.SetActive(false);
                        TacticsSlotContainer.SetActive(false);
                        CardSlotContainer.gameObject.SetActive(true);
                        actionContainer.SetActive(false);
                        CardImage.ImageEnum = Card.CardImage;
                        cardTitle.text = Card.CardTitle;
                        break;
                    }
                    case CardType_Enum.Spell: {
                        SetupUI_SpellSlot();
                        break;
                    }
                    case CardType_Enum.Tactics_Day:
                    case CardType_Enum.Tactics_Night: {
                        SetupUI_Tactics();
                        break;
                    }
                }
            }
        }
        public override void UpdateUI(PlayerData pd) {
            UpdateUI_CardState(pd);
        }
        private void UpdateUI_CardState(PlayerData pd) {
            if (CardHolder == CardHolder_Enum.PlayerHand ||
                CardHolder == CardHolder_Enum.PlayerUnitHand ||
                CardHolder == CardHolder_Enum.PlayerSkillHand
                ) {
                if (UpdateCardState(pd)) {
                    actionTaken.SetActive(false);
                    switch (Card.CardType) {
                        case CardType_Enum.Wound:
                        case CardType_Enum.Basic:
                        case CardType_Enum.Spell:
                        case CardType_Enum.Artifact:
                        case CardType_Enum.Advanced: {
                            if (CardState.Contains(CardState_Enum.Discard)) {
                                actionTaken.SetActive(true);
                                actionTakenText.text = "Discard";
                            } else if (CardState.Contains(CardState_Enum.Basic)) {
                                actionTaken.SetActive(true);
                                actionTakenText.text = "Basic";
                            } else if (CardState.Contains(CardState_Enum.Normal)) {
                                actionTaken.SetActive(true);
                                actionTakenText.text = "Normal";
                            } else if (CardState.Contains(CardState_Enum.Advanced)) {
                                actionTaken.SetActive(true);
                                actionTakenText.text = "Advanced";
                            } else if (CardState.Contains(CardState_Enum.Trashed)) {
                                actionTaken.SetActive(true);
                                actionTakenText.text = "Trashed";
                            }
                            break;
                        }
                        case CardType_Enum.Unit_Normal:
                        case CardType_Enum.Unit_Elite: {
                            unitSlot.UpdateUI(CardState, Banner);
                            break;
                        }
                    }
                }
            }
        }

        private bool UpdateCardState(PlayerData pd) {
            bool forReturn = false;
            List<CardState_Enum> currentCardState = new List<CardState_Enum>();
            CNAMap<int, WrapList<CardState_Enum>> states = pd.Deck.State;
            if (states.ContainsKey(UniqueCardId)) {
                currentCardState = states[UniqueCardId].Values;
            }
            if (!Enumerable.SequenceEqual(CardState, currentCardState)) {
                forReturn = true;
            }
            cardState.Clear();
            cardState.AddRange(currentCardState);

            int currentBanner = 0;
            if (pd.Deck.Banners.ContainsKey(UniqueCardId)) {
                currentBanner = pd.Deck.Banners[UniqueCardId];
            }
            if (currentBanner != banner) {
                banner = currentBanner;
                forReturn = true;
            }
            return forReturn;
        }

        public void SetupUI_CardSlot() {
            unitSlot.gameObject.SetActive(false);
            SpellSlotContainer.SetActive(false);
            CardSlotContainer.SetActive(true);
            actionContainer.SetActive(true);
            TacticsSlotContainer.SetActive(false);
            CardImage.ImageEnum = Card.CardImage;
            cardTitle.text = Card.CardTitle;
            normalText.text = Card.Actions[0];
            advancedText.text = Card.Actions[1];
            if (Card.CardType == CardType_Enum.Artifact) {
                normalColor.color = UIUtil.getColor(CardColor_Enum.NA);
                advancedColor.color = UIUtil.getColor(CardColor_Enum.Orange);
            } else {
                normalColor.color = UIUtil.getColor(Card.Costs[0][0]);
                advancedColor.color = UIUtil.getColor(Card.Costs[1][0]);
            }
        }

        public void SetupUI_SpellSlot() {
            unitSlot.gameObject.SetActive(false);
            SpellSlotContainer.SetActive(true);
            CardSlotContainer.SetActive(false);
            TacticsSlotContainer.SetActive(false);
            SpellTitleText[0].text = Card.SpellTitle[0];
            SpellTitleText[1].text = Card.SpellTitle[1];
            Color color = UIUtil.getColor(Card.Costs[0][0]);
            SpellButtons[0].SetupUI(Card.Actions[0], color);
            Color dark = color * .5f;
            dark.a = 1;
            SpellButtons[1].SetupUI(Card.Actions[1], dark);
        }

        public void SetupUI_Tactics() {
            unitSlot.gameObject.SetActive(false);
            SpellSlotContainer.SetActive(false);
            CardSlotContainer.SetActive(false);
            TacticsSlotContainer.SetActive(true);
            TacticsCardImage.ImageEnum = Card.CardImage;
            TacticsSelectedContainer.SetActive(false);
            UpdateUI_Tactics();
        }

        public void UpdateUI_Tactics() {
            if (Card.CardType == CardType_Enum.Tactics_Day ||
                Card.CardType == CardType_Enum.Tactics_Night) {
                TacticsSelectedContainer.SetActive(false);
                D.G.Players.ForEach(p => {
                    if (p.Deck.TacticsCardId.Equals(Card.UniqueId)) {
                        TacticsSelectedContainer.SetActive(true);
                        TacticsSelectedAvatarImage.ImageEnum = D.AvatarMetaDataMap[p.Avatar].AvatarShieldId;
                    }
                });
            }
        }

        public override void OnClickCard() {
            PlayerData localPlayer = D.LocalPlayer;
            if (ActionCard.SelectSingleCardPanel.gameObject.activeSelf) {
                ActionCard.SelectSingleCardPanel.OnClick_SelectCard(this);
            } else {
                ActionCard.SelectedCardSlot = this;
                if (D.isTurn) {
                    if (localPlayer.PlayerTurnPhase == TurnPhase_Enum.Battle) {
                        if (localPlayer.Battle.BattlePhase == BattlePhase_Enum.AssignDamage) {
                            if (CardHolder == CardHolder_Enum.PlayerUnitHand) {
                                bool bondsOfLoyalty = false;
                                if (localPlayer.Deck.State.ContainsKey(Card.UniqueId)) {
                                    bondsOfLoyalty = localPlayer.Deck.State[Card.UniqueId].Contains(CardState_Enum.Unit_BondsOfLoyalty);
                                }
                                bool noUnits = localPlayer.GameEffects.ContainsKeyAny(GameEffect_Enum.SH_Monastery, GameEffect_Enum.SH_Dungeon, GameEffect_Enum.SH_Tomb);

                                if (noUnits && !bondsOfLoyalty) {
                                    D.Msg("You can not use Units in this battle!");
                                } else {
                                    localPlayer.Battle.SelectedUnit = Card.UniqueId;
                                    D.A.UpdateUI();
                                }
                            }
                        }
                    }
                }
            }
        }

        public void UpdateUI_DisableClick() {
            GetComponentInChildren<Button>().enabled = false;
            GetComponentInChildren<CustomCursor>().enabled = false;
        }
    }
}
