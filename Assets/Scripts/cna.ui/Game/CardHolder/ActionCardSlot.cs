using System;
using System.Collections.Generic;
using System.Linq;
using cna.poo;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace cna.ui {
    public class ActionCardSlot : CardEngine {
        private CardSlot selectedCardSlot;
        public HexItemDetail Hex { get; set; }

        [SerializeField] private TriggerBattlePanel TriggerBattlePanel;
        [SerializeField] private ManaPayPanel ManaPayPanel;
        [SerializeField] public ConformationCanvas ConformationCanvas;
        [SerializeField] public SelectSingleCardPanel SelectSingleCardPanel;
        [SerializeField] public SelectOptionsPanel SelectOptionsPanel;
        [SerializeField] public SelectCardsPanel SelectCardsPanel;
        [SerializeField] public SelectManaPanel SelectManaPanel;
        [SerializeField] public LevelUpPanel LevelUpPanel;
        [SerializeField] public YesNoPanel YesNoPanel;
        [SerializeField] public AcceptPanel AcceptPanel;
        [SerializeField] public WaitingOnServerPanel WaitingOnServerPan;

        [SerializeField] private GameObject emptyCardContainer;
        [SerializeField] private GameObject cardContainer;
        [SerializeField] private GameObject actionTaken;
        [SerializeField] private TextMeshProUGUI actionTakenText;
        [SerializeField] private TextMeshProUGUI cardTitle;
        [SerializeField] private AddressableImage cardImage;
        [SerializeField] private UnitSlotContainer unitSlot;

        [Header("Basic/Adanced/Wound Card")]
        [SerializeField] private List<CardState_Enum> cardState;
        [SerializeField] private GameObject cardSlotContainer;
        [SerializeField] private GameObject actionContainer;
        [SerializeField] private int banner;

        [Header("Action - Botton Buttons")]
        [SerializeField] private BottomButtonContainer BottomButtonContainer;
        [SerializeField] private CNA_Button NormalButton;
        [SerializeField] private CNA_Button AdvancedButton;
        [SerializeField] private CNA_Button UnitFearButton;
        [SerializeField] private CNA_Button UnitCourageButton;


        [Header("Board Hex")]
        [SerializeField] private Transform BoardGameHexContainer;
        [SerializeField] private TextMeshProUGUI BoardGameHexTitle;
        [SerializeField] private AddressableImage BoardGameHexImage;
        [SerializeField] private Transform BoardGameObjectContainer;
        [SerializeField] private TextMeshProUGUI BoardGameObjectTitle;
        [SerializeField] private AddressableImage BoardGameObjectImage;
        [SerializeField] private BoardGameMonsterContainer BoardGameMonsterContainer;
        [SerializeField] private Tilemap StructureTilemap;
        [SerializeField] private BoardGameShieldContainer BoardGameShieldContainer;
        [SerializeField] private GameObject BoardGameInfoButton;

        [Header("Error Msg")]
        [SerializeField] private NotificationCanvas Notification;

        [Header("Game Effect")]
        [SerializeField] private GameObject GameEffectContainer;
        [SerializeField] private TextMeshProUGUI GameEffectTitle;
        [SerializeField] private TextMeshProUGUI GameEffectDescription;
        [SerializeField] private AddressableImage GameEffectImage;

        [Header("Skill Card")]
        [SerializeField] private GameObject SkillContainer;
        [SerializeField] private TextMeshProUGUI SkillTitle;
        [SerializeField] private TextMeshProUGUI SkillDescription;
        [SerializeField] private AddressableImage SkillImage;

        [Header("Spell Card")]
        [SerializeField] private GameObject SpellSlotContainer;
        [SerializeField] private CNA_Button[] SpellButtons;
        [SerializeField] private TextMeshProUGUI[] SpellTitleText;

        [Header("Tactics Card")]
        [SerializeField] private GameObject TacticsSlotContainer;
        [SerializeField] private AddressableImage TacticsCardImage;
        [SerializeField] private GameObject TacticsSelectedContainer;
        [SerializeField] private AddressableImage TacticsSelectedAvatarImage;


        public void Awake() {
            Clear();
        }


        protected GameObject EmptyCardContainer { get => emptyCardContainer; set => emptyCardContainer = value; }
        protected GameObject CardContainer { get => cardContainer; set => cardContainer = value; }
        public GameObject ActionTaken { get => actionTaken; set => actionTaken = value; }
        public TextMeshProUGUI ActionTakenText { get => actionTakenText; set => actionTakenText = value; }
        protected TextMeshProUGUI CardTitle { get => cardTitle; set => cardTitle = value; }
        protected AddressableImage CardImage { get => cardImage; set => cardImage = value; }
        public GameObject CardSlotContainer { get => cardSlotContainer; set => cardSlotContainer = value; }
        public CardSlot SelectedCardSlot {
            get => selectedCardSlot; set {
                if (selectedCardSlot != null) {
                    selectedCardSlot.SetSelected(false);
                }
                selectedCardSlot = value;
                if (selectedCardSlot != null) {
                    selectedCardSlot.SetSelected(true);
                    SetupUI(selectedCardSlot.UniqueCardId, selectedCardSlot.CardHolder);
                } else {
                    SetupUI(0, CardHolder_Enum.NA);
                }
            }
        }
        public List<CardState_Enum> CardState { get => cardState; set => cardState = value; }
        public int Banner { get => banner; set => banner = value; }

        #region SetupUI
        public void SetupUI(int key, CardHolder_Enum holder) {
            CardHolder = holder;
            UniqueCardId = key;
            Card = D.Cards[UniqueCardId];
            SetupUI();
            UpdateUI();
        }
        public void SetupUI(HexItemDetail h) {
            CardHolder = CardHolder_Enum.BoardHex;
            UniqueCardId = 0;
            if (selectedCardSlot != null) {
                selectedCardSlot.SetSelected(false);
            }
            selectedCardSlot = null;
            Hex = h;
            SetupUI();
            UpdateUI();
        }
        private void SetupUI() {
            cardState = new List<CardState_Enum>();
            if (CardHolder == CardHolder_Enum.NA) {
                cardContainer.gameObject.SetActive(false);
                EmptyCardContainer.gameObject.SetActive(true);
            } else {
                EmptyCardContainer.gameObject.SetActive(false);
                BottomButtonContainer.gameObject.SetActive(true);
                cardContainer.SetActive(true);
                GameEffectContainer.SetActive(false);
                unitSlot.gameObject.SetActive(false);
                SkillContainer.SetActive(false);
                CardSlotContainer.SetActive(false);
                SpellSlotContainer.SetActive(false);
                TacticsSlotContainer.SetActive(false);
                if (CardHolder == CardHolder_Enum.BoardHex) {
                } else {
                    switch (Card.CardType) {
                        case CardType_Enum.GameEffect: {
                            SetupUI_GameEffect();
                            break;
                        }
                        case CardType_Enum.Artifact:
                        case CardType_Enum.Basic:
                        case CardType_Enum.Advanced: {
                            SetupUI_CardSlot();
                            break;
                        }
                        case CardType_Enum.Unit_Normal:
                        case CardType_Enum.Unit_Elite: {
                            actionContainer.SetActive(true);
                            unitSlot.gameObject.SetActive(true);
                            unitSlot.SetupUI(Card, CardState, D.AvatarMetaDataMap[D.LocalPlayer.Avatar].AvatarShieldId);
                            break;
                        }
                        case CardType_Enum.Wound: {
                            CardSlotContainer.gameObject.SetActive(true);
                            actionContainer.SetActive(false);
                            CardImage.ImageEnum = Card.CardImage;
                            cardTitle.text = Card.CardTitle;
                            break;
                        }
                        case CardType_Enum.Skill: {
                            actionContainer.SetActive(true);
                            SkillContainer.SetActive(true);
                            SkillTitle.text = Card.CardTitle;
                            SkillDescription.text = Card.Actions[0];
                            SkillImage.ImageEnum = Card.CardImage;
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
        }

        public void SetupUI_GameEffect() {
            actionContainer.SetActive(false);
            GameEffectContainer.SetActive(true);
            GameEffectTitle.text = Card.CardTitle;
            GameEffectDescription.text = Card.GameEffectDescription;
            GameEffectImage.ImageEnum = Card.CardImage;
        }
        public void SetupUI_CardSlot() {
            CardSlotContainer.SetActive(true);
            actionContainer.SetActive(true);
            CardImage.ImageEnum = Card.CardImage;
            cardTitle.text = Card.CardTitle;
            if (Card.CardType == CardType_Enum.Artifact) {
                NormalButton.SetupUI(Card.Actions[0], UIUtil.getColor(CardColor_Enum.NA));
                AdvancedButton.SetupUI(Card.Actions[1], UIUtil.getColor(CardColor_Enum.Orange));
            } else {
                NormalButton.SetupUI(Card.Actions[0], UIUtil.getColor(Card.Costs[0][0]));
                AdvancedButton.SetupUI(Card.Actions[1], UIUtil.getColor(Card.Costs[1][0]));
            }
        }
        public void SetupUI_SpellSlot() {
            SpellSlotContainer.SetActive(true);
            SpellTitleText[0].text = Card.SpellTitle[0];
            SpellTitleText[1].text = Card.SpellTitle[1];
            Color color = UIUtil.getColor(Card.Costs[0][0]);
            SpellButtons[0].SetupUI(Card.Actions[0], color);
            Color dark = color * .5f;
            dark.a = 1;
            SpellButtons[1].SetupUI(Card.Actions[1], dark);
        }

        public void SetupUI_Tactics() {
            TacticsSlotContainer.SetActive(true);
            TacticsCardImage.ImageEnum = Card.CardImage;
            TacticsSelectedContainer.SetActive(false);
            UpdateUI_Tactics();
        }

        public void UpdateUI_Tactics() {
            if (Card.CardType == CardType_Enum.Tactics_Day ||
                Card.CardType == CardType_Enum.Tactics_Night) {
                D.G.Players.ForEach(p => {
                    if (p.Deck.TacticsCardId.Equals(Card.UniqueId)) {
                        TacticsSelectedContainer.SetActive(true);
                        TacticsSelectedAvatarImage.ImageEnum = D.AvatarMetaDataMap[p.Avatar].AvatarShieldId;
                    }
                });
            }
        }

        #endregion

        #region UpdateUI
        public override void UpdateUI() {
            UpdateUI_Hex();
            UpdateUI_CardState();
            BottomButtonContainer.UpdateUI();
        }

        private void UpdateUI_CardState() {
            ActionTaken.SetActive(false);
            if (CardHolder == CardHolder_Enum.PlayerHand ||
                CardHolder == CardHolder_Enum.PlayerUnitHand ||
                CardHolder == CardHolder_Enum.PlayerSkillHand ||
                CardHolder == CardHolder_Enum.PlayerSkillHand
                ) {
                UpdateCardState();
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
                    case CardType_Enum.Skill: {
                        if (CardState.Contains(CardState_Enum.Skill_Used)) {
                            actionTaken.SetActive(true);
                            actionTakenText.text = "Used";
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
        private void UpdateCardState() {
            cardState.Clear();
            CNAMap<int, WrapList<CardState_Enum>> states = D.LocalPlayer.Deck.State;
            if (states.ContainsKey(UniqueCardId)) {
                List<CardState_Enum> currentCardState = states[UniqueCardId].Values;
                cardState.AddRange(currentCardState);
            }
            banner = 0;
            if (D.LocalPlayer.Deck.Banners.ContainsKey(UniqueCardId)) {
                banner = D.LocalPlayer.Deck.Banners[UniqueCardId];
            }
        }

        private void UpdateUI_Hex() {
            if (CardHolder == CardHolder_Enum.BoardHex) {
                if (Hex.Terrain == Image_Enum.NA) {
                    EmptyCardContainer.gameObject.SetActive(true);
                } else {
                    BoardGameHexContainer.gameObject.SetActive(true);
                    BoardGameHexTitle.text = Hex.Terrain.ToString().Substring(3);
                    BoardGameHexImage.ImageEnum = Hex.Terrain;
                    if (Hex.Structure == Image_Enum.NA) {
                        BoardGameObjectContainer.gameObject.SetActive(false);
                    } else {
                        BoardGameObjectContainer.gameObject.SetActive(true);
                        BoardGameObjectTitle.text = Hex.Structure.ToString().Substring(3);
                        BoardGameObjectImage.ImageEnum = Hex.Structure;
                    }
                    if (D.LocalPlayer.Board.MonsterData.ContainsKey(Hex.GridPosition)) {
                        BoardGameMonsterContainer.gameObject.SetActive(true);
                        BoardGameMonsterContainer.UpdateUI(D.LocalPlayer, Hex);
                    } else {
                        BoardGameMonsterContainer.gameObject.SetActive(false);
                    }
                    bool cleared = BasicUtil.getAllShieldsAtPos(D.G, Hex.GridPosition).Count > 0;
                    if (cleared) {
                        BoardGameShieldContainer.gameObject.SetActive(true);
                        BoardGameShieldContainer.UpdateUI(Hex);
                    } else {
                        BoardGameShieldContainer.gameObject.SetActive(false);
                    }
                }
            }
        }

        #endregion

        #region Buttons

        public override bool CheckTurnAndUI(GameAPI ar) {
            if (D.isTurn) {
                if (isConformationCanvasOpen()) {
                    ar.ErrorMsg = "Can not perform action while another window is open!";
                } else {
                    if (ar.ActionTaken == CardState_Enum.NA) {
                    } else {
                        if (CardHolder == CardHolder_Enum.PlayerHand ||
                            CardHolder == CardHolder_Enum.PlayerSkillHand ||
                            CardHolder == CardHolder_Enum.PlayerUnitHand) {
                        } else {
                            bool callToArms = D.LocalPlayer.GameEffects.ContainsKey(GameEffect_Enum.CS_CallToArms);
                            if (callToArms && CardHolder == CardHolder_Enum.UnitOffering
                                && (Card.CardType == CardType_Enum.Unit_Elite || Card.CardType == CardType_Enum.Unit_Normal)) {
                                D.LocalPlayer.RemoveGameEffect(GameEffect_Enum.CS_CallToArms);
                            } else {
                                ar.ErrorMsg = "You can only play cards that are in your hand!";
                            }
                        }
                    }
                }
            } else {
                ar.ErrorMsg = "You can only perform this action on your turn!";
            }
            return ar.Status;
        }

        public void OnClick_ActionDiscardCard() {
            GameAPI ar = new GameAPI(Card.UniqueId, CardState_Enum.Discard);
            if (CheckTurnAndUI(ar)) {
                if (ar.P.PlayerTurnPhase >= TurnPhase_Enum.StartTurn && ar.P.PlayerTurnPhase < TurnPhase_Enum.EndTurn) {
                    ar.AddCardState();
                    if (ar.P.PlayerTurnPhase == TurnPhase_Enum.Battle && ar.P.Battle.BattlePhase == BattlePhase_Enum.Attack) {
                        if (ar.P.GameEffects.Keys.Contains(GameEffect_Enum.CT_SwordOfJustice01)) {
                            ar.BattleAttack(new AttackData(3));
                        }
                    }
                } else {
                    ar.ErrorMsg = string.Format("Discard action can not be played in the current player phase.");
                }
            }
            ProcessActionResultVO(ar);
        }

        public void OnClick_ActionBasicCard() {
            GameAPI ar = new GameAPI(Card.UniqueId, CardState_Enum.Basic);
            D.Cards[1].OnClick_ActionBasicButton(ar);
        }

        public void OnClick_ActionNormalButton() {
            GameAPI ar = new GameAPI(Card.UniqueId, CardState_Enum.Normal, 0);
            BottomButtonContainer.ButtonClicked = NormalButton;
            Card.OnClick_ActionButton(ar);
        }

        public void OnClick_ActionAdvancedButton() {
            GameAPI ar = new GameAPI(Card.UniqueId, CardState_Enum.Advanced, 1);
            BottomButtonContainer.ButtonClicked = AdvancedButton;
            Card.OnClick_ActionButton(ar);
        }

        public void OnClick_ActionUnitButton(GameAPI ar, ICNA_Base clicked) {
            BottomButtonContainer.ButtonClicked = (CNA_Button)clicked;
            Card.OnClick_ActionButton(ar);
        }

        public void OnClick_ActionSpellButton(int index) {
            CardState_Enum cardState = index == 0 ? CardState_Enum.Normal : CardState_Enum.Advanced;
            GameAPI ar = new GameAPI(Card.UniqueId, cardState, index);
            BottomButtonContainer.ButtonClicked = SpellButtons[index];
            Card.OnClick_ActionButton(ar);
        }

        public void OnClick_ActionSkillButton() {
            GameAPI ar = new GameAPI(Card.UniqueId, CardState_Enum.Skill_Used, 0);
            Card.OnClick_ActionButton(ar);
        }

        public void OnClick_ActionGameEffect() {
            GameAPI ar = new GameAPI(Card.UniqueId, CardState_Enum.NA, 0);
            Card.OnClick_ActionButton(ar);
        }

        public void OnClick_Learn() {
            GameAPI ar = new GameAPI(Card.UniqueId, CardState_Enum.NA);
            if (CheckTurnAndUI(ar)) {
                bool learn01 = ar.P.GameEffects.ContainsKey(GameEffect_Enum.AC_Learning01);
                bool learn02 = ar.P.GameEffects.ContainsKey(GameEffect_Enum.AC_Learning02);
                if (learn01 || learn02) {
                    int actionCost = learn01 ? 6 : 9;
                    if (ar.P.Influence >= actionCost) {
                        ar.ActionInfluence(-1 * actionCost);
                        if (learn01) {
                            ar.AddCardToDiscardDeck(Card.UniqueId);
                            ar.RemoveGameEffect(GameEffect_Enum.AC_Learning01);
                        } else {
                            ar.AddCardToHandDeck(Card.UniqueId);
                            ar.RemoveGameEffect(GameEffect_Enum.AC_Learning02);
                        }
                        ar.removeFromAdvancedOffering(Card.UniqueId);
                        ar.drawAdvancedToOffering();
                        SelectedCardSlot = null;
                    } else {
                        ar.ErrorMsg = "you do not have enough influence to learn this action.";
                    }
                } else {
                    ar.ErrorMsg = "You can not perform this action.";
                }
            }
            ProcessActionResultVO(ar);
        }

        public void OnClick_Train() {
            GameAPI ar = new GameAPI(Card.UniqueId, CardState_Enum.NA);
            if (CheckTurnAndUI(ar)) {
                Image_Enum structure = BasicUtil.GetTilemapId(ar.P.CurrentGridLoc, StructureTilemap);
                bool monasteryBurned = structure == Image_Enum.SH_Monastery && BasicUtil.getAllShieldsAtPos(D.G, ar.P.CurrentGridLoc).Count > 0;
                if (!monasteryBurned) {
                    if (ar.P.PlayerTurnPhase <= TurnPhase_Enum.Influence) {
                        int actionCost = 6;
                        if (ar.P.Influence >= actionCost) {
                            ar.TurnPhase(TurnPhase_Enum.Influence);
                            ar.ActionInfluence(-1 * actionCost);
                            ar.AddCardToTopOfDeck(Card.UniqueId);
                            ar.removeFromUnitOffering(Card.UniqueId);
                            SelectedCardSlot = null;
                        } else {
                            ar.ErrorMsg = "you do not have enough influence to learn this action. You need " + actionCost + ".";
                        }
                    } else {
                        ar.ErrorMsg = "You can only Train during the influence phase!";
                    }
                } else {
                    ar.ErrorMsg = "the monastery has been destroyed, you can not train from this location.";
                }
            }
            ProcessActionResultVO(ar);
        }

        public void OnClick_Explore() {
            GameAPI ar = new GameAPI(0, CardState_Enum.NA);
            if (CheckTurnAndUI(ar)) {
                if (ar.P.PlayerTurnPhase <= TurnPhase_Enum.Move) {
                    if (ar.P.Movement >= 2) {
                        ar.AcceptPanel("Warning!",
                            "You are about to explore an undiscovered tile, You Will NOT be able to UNDO this action, would you like to continue?",
                            new List<Action<GameAPI>>() { Explore_Yes, (a) => { } },
                            new List<string> { "Yes", "No" },
                            new List<Color32> { CNAColor.ColorLightGreen, CNAColor.ColorLightRed },
                            CNAColor.ColorLightBlue);
                    } else {
                        ar.ErrorMsg = "You do not have enough movement to Explore, you need 2 movement points!";
                    }
                } else {
                    ar.ErrorMsg = "You can only Explore during the movement phase!";
                }
            }
            ProcessActionResultVO(ar);
        }

        public void Explore_Yes(GameAPI ar) {
            int index = D.Scenario.ConvertWorldToIndex(Hex.GridPosition);
            ar.P.Board.PlayerMap[index] = MapHexId_Enum.Explore;
            ar.P.WaitOnServer = true;
            ar.P.UndoLock = true;
            SelectedCardSlot = null;
            ar.AddLog("[Explore]");
            ar.ActionMovement(-2);
            ar.WaitOnServerPanel();
        }

        public void OnClick_Recruit_BondsOfLoyalty() {
            Recruit(true);
        }

        public void OnClick_Recruit() {
            Recruit(false);
        }

        private void Recruit(bool bondsOfLoyalty) {
            GameAPI ar = new GameAPI(Card.UniqueId, CardState_Enum.NA);
            if (CheckTurnAndUI(ar)) {
                CardUnitVO c = (CardUnitVO)selectedCardSlot.Card;
                Image_Enum structure = BasicUtil.GetTilemapId(ar.P.CurrentGridLoc, StructureTilemap);
                bool callToGlory = D.LocalPlayer.GameEffects.ContainsKey(GameEffect_Enum.CS_CallToGlory);
                if (structure == Image_Enum.SH_Village && c.UnitRecruitLocation.Contains(Image_Enum.I_unitvillage) ||
                    structure == Image_Enum.SH_Keep && c.UnitRecruitLocation.Contains(Image_Enum.I_unitkeep) ||
                    structure == Image_Enum.SH_MageTower && c.UnitRecruitLocation.Contains(Image_Enum.I_unitmage) ||
                    structure == Image_Enum.SH_Monastery && c.UnitRecruitLocation.Contains(Image_Enum.I_unitmonastery) ||
                    structure == Image_Enum.SH_City_Blue && c.UnitRecruitLocation.Contains(Image_Enum.I_unitcity) ||
                    structure == Image_Enum.SH_City_Red && c.UnitRecruitLocation.Contains(Image_Enum.I_unitcity) ||
                    structure == Image_Enum.SH_City_Green && c.UnitRecruitLocation.Contains(Image_Enum.I_unitcity) ||
                    structure == Image_Enum.SH_City_White ||
                    callToGlory
                    ) {
                    bool monasteryBurned = structure == Image_Enum.SH_Monastery && BasicUtil.getAllShieldsAtPos(D.G, ar.P.CurrentGridLoc).Count > 0;
                    if (!monasteryBurned) {
                        if (ar.P.PlayerTurnPhase <= TurnPhase_Enum.Influence || callToGlory) {
                            int unitCount = ar.P.Deck.Unit.Count;
                            ar.P.Deck.State.Keys.ForEach(u => {
                                if (ar.P.Deck.State[u].ContainsAny(CardState_Enum.Unit_BondsOfLoyalty)) {
                                    unitCount--;
                                }
                            });
                            if (bondsOfLoyalty || unitCount < ar.P.Deck.UnitHandLimit) {
                                int unitCost = c.UnitCost;
                                if (bondsOfLoyalty) {
                                    unitCost -= 5;
                                    if (unitCost < 0) {
                                        unitCost = 0;
                                    }
                                }
                                if (callToGlory) {
                                    unitCost = 0;
                                }
                                if (ar.P.Influence >= unitCost) {
                                    if (bondsOfLoyalty) {
                                        ar.AddCardState(c.UniqueId, CardState_Enum.Unit_BondsOfLoyalty);
                                        ar.AddLog("[Recruit *Bonds of Loyalty] " + c.CardTitle);
                                    } else {
                                        ar.AddLog("[Recruit] " + c.CardTitle);
                                    }
                                    if (!callToGlory) {
                                        ar.TurnPhase(TurnPhase_Enum.Influence);
                                    }
                                    ar.ActionInfluence(-1 * unitCost);
                                    ar.removeFromUnitOffering(c.UniqueId);
                                    ar.P.Deck.Unit.Add(c.UniqueId);
                                    ar.P.GameEffects.Keys.ForEach(ge => {
                                        int count = ar.P.GameEffects[ge].Count;
                                        switch (ge) {
                                            case GameEffect_Enum.AC_HeroicTale01: { ar.Rep(count); break; }
                                            case GameEffect_Enum.AC_HeroicTale02: { ar.Rep(count); ar.Fame(count); break; }
                                        }
                                    });
                                    ar.P.RemoveGameEffect(GameEffect_Enum.CS_CallToGlory);
                                    SelectedCardSlot = null;
                                } else {
                                    ar.ErrorMsg = "you do not have enough influence to recruit this unit.";
                                }
                            } else {
                                ar.ErrorMsg = "you do can not carry any more units.";
                            }
                        } else {
                            ar.ErrorMsg = "you can not recruit during the " + D.LocalPlayer.PlayerTurnPhase + " player phase.";
                        }
                    } else {
                        ar.ErrorMsg = "the monastery has been destroyed, you can not recruit from this location.";
                    }
                } else {
                    ar.ErrorMsg = "this unit can not be recruited at your location";
                }
            }
            ProcessActionResultVO(ar);
        }

        public void OnClick_Disband() {
            GameAPI ar = new GameAPI(Card.UniqueId, CardState_Enum.NA);
            if (D.isTurn) {
                if (!isConformationCanvasOpen() || D.LocalPlayer.PlayerTurnPhase == TurnPhase_Enum.Reward) {
                    if (ar.P.Deck.State.ContainsKey(Card.UniqueId) && ar.P.Deck.State[Card.UniqueId].ContainsAny(CardState_Enum.Unit_BondsOfLoyalty)) {
                        ar.ErrorMsg = "Units Recruited with Bonds of Loyalty can not be disbanded!";
                    } else {
                        if (ar.P.Deck.State.ContainsKey(Card.UniqueId)) {
                            ar.P.Deck.State.Remove(Card.UniqueId);
                        }
                        ar.P.Deck.Unit.Remove(Card.UniqueId);
                        ar.AddLog("[Disband] " + Card.CardTitle);
                        ar.change();
                        SelectedCardSlot = null;
                    }
                } else {
                    ar.ErrorMsg = "Can not perform action while another window is open!";
                }
            } else {
                ar.ErrorMsg = "You can only perform this action on your turn!";
            }
            ProcessActionResultVO(ar);
        }

        public void OnClick_HealWound() {
            GameAPI ar = new GameAPI(Card.UniqueId, CardState_Enum.Trashed);
            if (CheckTurnAndUI(ar)) {
                if (ar.P.PlayerTurnPhase != TurnPhase_Enum.Battle) {
                    if (ar.P.Healpoints > 0) {
                        ar.AddCardState();
                        ar.Healing(-1);
                        if (ar.P.GameEffects.ContainsKey(GameEffect_Enum.CT_GoldenGrail)) {
                            ar.DrawCard(1, ProcessActionResultVO);
                            return;
                        }
                    } else {
                        ar.ErrorMsg = "You do not have enough healing points to heal a wound!";
                    }
                } else {
                    ar.ErrorMsg = "You can not heal in a battle!";
                }
            }
            ProcessActionResultVO(ar);
        }

        public void OnClick_HealUnit() {
            GameAPI ar = new GameAPI(Card.UniqueId, CardState_Enum.NA);
            if (CheckTurnAndUI(ar)) {
                if (ar.P.PlayerTurnPhase != TurnPhase_Enum.Battle) {
                    if (ar.P.Healpoints >= Card.UnitLevel) {
                        ar.Healing(-1 * Card.UnitLevel);
                        if (ar.P.Deck.State[Card.UniqueId].Contains(CardState_Enum.Unit_Poisoned)) {
                            ar.RemoveCardState(Card.UniqueId, CardState_Enum.Unit_Poisoned);
                        } else {
                            ar.RemoveCardState(Card.UniqueId, CardState_Enum.Unit_Wounded);
                        }
                    } else {
                        ar.ErrorMsg = "You do not have enough healing points to heal this unit! you need " + Card.UnitLevel + " points per wound!";
                    }
                } else {
                    ar.ErrorMsg = "You can not heal in a battle!";
                }
            }
            ProcessActionResultVO(ar);
        }

        public void OnClick_ProvokeMonster() {
            GameAPI ar = new GameAPI(0, CardState_Enum.NA);
            if (CheckTurnAndUI(ar)) {
                if (D.LocalPlayer.PlayerTurnPhase < TurnPhase_Enum.Influence) {
                    SelectedCardSlot = null;
                    TriggerBattlePanel.SetupUI(Hex, (h) => { D.A.UpdateUI(); });
                } else {
                    ar.ErrorMsg = "You can not perform this action";
                }
            }
            ProcessActionResultVO(ar);
        }

        public void OnClick_UnitBannerFear() {
            GameAPI ar = new GameAPI(Card.UniqueId, CardState_Enum.Unit_Banner_Fear, 2);
            BottomButtonContainer.ButtonClicked = UnitFearButton;
            D.Cards[D.LocalPlayer.Deck.Banners[Card.UniqueId]].OnClick_ActionButton(ar);
        }

        public void OnClick_UnitBannerCourage() {
            GameAPI ar = new GameAPI(Card.UniqueId, CardState_Enum.Unit_Banner_Courage, 2);
            BottomButtonContainer.ButtonClicked = UnitCourageButton;
            D.Cards[D.LocalPlayer.Deck.Banners[Card.UniqueId]].OnClick_ActionButton(ar);
        }

        public void OnClick_AdventureDungeon() {
            GameAPI ar = new GameAPI(0, CardState_Enum.NA);
            if (CheckTurnAndUI(ar)) {
                if (D.LocalPlayer.PlayerTurnPhase < TurnPhase_Enum.Influence) {
                    SelectedCardSlot = null;
                    MonsterMetaData md = new MonsterMetaData(D.Scenario.DrawMonster(MonsterType_Enum.Brown), D.LocalPlayer.CurrentGridLoc, Image_Enum.SH_Dungeon);
                    Hex.Monsters.Add(md);
                    Hex.AdventureAction = Image_Enum.SH_Dungeon;
                    ar.AddGameEffect(GameEffect_Enum.SH_Dungeon);
                    TriggerBattlePanel.SetupUI(Hex, (h) => { D.A.pd_StartOfTurn = ar.P.Clone(); D.A.UpdateUI(); }, TriggerBattlePanel.STANDARD_BATTLE_NO_UNDO);
                } else {
                    ar.ErrorMsg = "You can not perform this action during " + D.LocalPlayer.PlayerTurnPhase + " player phase.";
                }
            }
            ProcessActionResultVO(ar);
        }

        public void OnClick_AdventureTomb() {
            GameAPI ar = new GameAPI(0, CardState_Enum.NA);
            if (CheckTurnAndUI(ar)) {
                if (D.LocalPlayer.PlayerTurnPhase < TurnPhase_Enum.Influence) {
                    SelectedCardSlot = null;
                    MonsterMetaData md = new MonsterMetaData(D.Scenario.DrawMonster(MonsterType_Enum.Red), D.LocalPlayer.CurrentGridLoc, Image_Enum.SH_Dungeon);
                    Hex.Monsters.Add(md);
                    Hex.AdventureAction = Image_Enum.SH_Tomb;
                    ar.AddGameEffect(GameEffect_Enum.SH_Tomb);
                    TriggerBattlePanel.SetupUI(Hex, (h) => { D.A.pd_StartOfTurn = ar.P.Clone(); D.A.UpdateUI(); }, TriggerBattlePanel.STANDARD_BATTLE_NO_UNDO);
                } else {
                    ar.ErrorMsg = "You can not perform this action during " + D.LocalPlayer.PlayerTurnPhase + " player phase.";
                }
            }
            ProcessActionResultVO(ar);
        }

        public void OnClick_AdventureSpawningGround() {
            if (D.isTurn) {
                if (!isConformationCanvasOpen()) {
                    if (D.LocalPlayer.PlayerTurnPhase < TurnPhase_Enum.Influence) {
                        SelectedCardSlot = null;
                        Hex.Monsters.AddRange(D.LocalPlayer.Board.MonsterData[Hex.GridPosition].Values.ConvertAll(m => {
                            return new MonsterMetaData(m, Hex.GridPosition, Hex.Structure);
                        }));
                        Hex.AdventureAction = Image_Enum.SH_SpawningGround;
                        if (Hex.Monsters.TrueForAll(m => D.LocalPlayer.VisableMonsters.Contains(m.Uniqueid))) {
                            TriggerBattlePanel.SetupUI(Hex, (h) => { D.A.UpdateUI(); });
                        } else {
                            TriggerBattlePanel.SetupUI(Hex, (h) => { D.A.pd_StartOfTurn = D.LocalPlayer.Clone(); D.A.UpdateUI(); }, TriggerBattlePanel.STANDARD_BATTLE_NO_UNDO);
                        }
                    } else {
                        Msg("You can not perform this action");
                        BottomButtonContainer.Shake();
                    }
                } else {
                    Msg("Can not perform action while another window is open!");
                    BottomButtonContainer.Shake();
                }
            } else {
                Msg("It is not your turn");
                BottomButtonContainer.Shake();
            }
        }

        public void OnClick_AdventureMonsterDen() {
            if (D.isTurn) {
                if (!isConformationCanvasOpen()) {
                    if (D.LocalPlayer.PlayerTurnPhase < TurnPhase_Enum.Influence) {
                        SelectedCardSlot = null;
                        Hex.Monsters.AddRange(D.LocalPlayer.Board.MonsterData[Hex.GridPosition].Values.ConvertAll(m => {
                            return new MonsterMetaData(m, Hex.GridPosition, Hex.Structure);
                        }));
                        Hex.AdventureAction = Image_Enum.SH_MonsterDen;
                        if (Hex.Monsters.TrueForAll(m => D.LocalPlayer.VisableMonsters.Contains(m.Uniqueid))) {
                            TriggerBattlePanel.SetupUI(Hex, (h) => { D.A.UpdateUI(); });
                        } else {
                            TriggerBattlePanel.SetupUI(Hex, (h) => { D.A.pd_StartOfTurn = D.LocalPlayer.Clone(); D.A.UpdateUI(); }, TriggerBattlePanel.STANDARD_BATTLE_NO_UNDO);
                        }
                    } else {
                        Msg("You can not perform this action");
                        BottomButtonContainer.Shake();
                    }
                } else {
                    Msg("Can not perform action while another window is open!");
                    BottomButtonContainer.Shake();
                }
            } else {
                Msg("It is not your turn");
                BottomButtonContainer.Shake();
            }
        }

        public void OnClick_VillageHeal() {
            GameAPI ar = new GameAPI(0, CardState_Enum.NA);
            if (CheckTurnAndUI(ar)) {
                if (ar.P.PlayerTurnPhase <= TurnPhase_Enum.Influence) {
                    if (ar.P.Influence >= 3) {
                        ar.TurnPhase(TurnPhase_Enum.Influence);
                        ar.ActionInfluence(-3);
                        ar.Healing(1);
                    } else {
                        ar.ErrorMsg = "you do not have enough influence to purchase healing.";
                    }
                } else {
                    ar.ErrorMsg = "you can not recruit during the " + D.LocalPlayer.PlayerTurnPhase + " player phase.";
                }
            }
            ProcessActionResultVO(ar);
        }

        public void OnClick_VillageRaid() {
            GameAPI ar = new GameAPI(0, CardState_Enum.NA);
            if (CheckTurnAndUI(ar)) {
                if (ar.P.PlayerTurnPhase < TurnPhase_Enum.Move && !ar.P.ActionTaken) {
                    ar.P.ActionTaken = true;
                    ar.Rep(-1);
                    ar.DrawCard(2, ProcessActionResultVO);
                    return;
                } else {
                    ar.ErrorMsg = "The Raid action must be the first action of your turn.";
                }
            }
            ProcessActionResultVO(ar);
        }

        public void OnClick_MonasteryHeal() {
            GameAPI ar = new GameAPI(0, CardState_Enum.NA);
            if (CheckTurnAndUI(ar)) {
                if (ar.P.PlayerTurnPhase <= TurnPhase_Enum.Influence) {
                    if (ar.P.Influence >= 2) {
                        ar.TurnPhase(TurnPhase_Enum.Influence);
                        ar.ActionInfluence(-2);
                        ar.Healing(1);
                    } else {
                        ar.ErrorMsg = "you do not have enough influence to purchase healing.";
                    }
                } else {
                    ar.ErrorMsg = "you can not recruit during the " + D.LocalPlayer.PlayerTurnPhase + " player phase.";
                }
            }
            ProcessActionResultVO(ar);
        }

        public void OnClick_MonasteryBurn() {
            GameAPI ar = new GameAPI(0, CardState_Enum.NA);
            if (CheckTurnAndUI(ar)) {
                if (D.LocalPlayer.PlayerTurnPhase < TurnPhase_Enum.Influence) {
                    SelectedCardSlot = null;
                    MonsterMetaData md = new MonsterMetaData(D.Scenario.DrawMonster(MonsterType_Enum.Violet), D.LocalPlayer.CurrentGridLoc, Image_Enum.SH_Monastery);
                    Hex.Monsters.Add(md);
                    Hex.AdventureAction = Image_Enum.SH_Monastery;
                    ar.AddGameEffect(GameEffect_Enum.SH_Monastery);
                    ar.Rep(-3);
                    TriggerBattlePanel.SetupUI(Hex, (h) => { D.A.pd_StartOfTurn = ar.P.Clone(); D.A.UpdateUI(); }, TriggerBattlePanel.STANDARD_BATTLE_NO_UNDO);
                } else {
                    ar.ErrorMsg = "You can not perform this action";
                }
            }
            ProcessActionResultVO(ar);
        }

        public void OnClick_CityRed_Artifact() {
            GameAPI ar = new GameAPI(0, CardState_Enum.NA);
            if (CheckTurnAndUI(ar)) {
                if (ar.P.PlayerTurnPhase <= TurnPhase_Enum.Influence) {
                    if (ar.P.Influence >= 12) {
                        ar.TurnPhase(TurnPhase_Enum.Influence);
                        ar.ActionInfluence(-12);
                        ar.Reward_Artifact(1);
                        ar.AddLog("+1 Artifact was added to your Rewards for the end of the turn.");
                    } else {
                        ar.ErrorMsg = "you do not have enough influence to purchase artifact.  You need 12 points.";
                    }
                } else {
                    ar.ErrorMsg = "you can not recruit during the " + D.LocalPlayer.PlayerTurnPhase + " player phase.";
                }
            }
            ProcessActionResultVO(ar);
        }

        public void OnClick_CityWhite_UnitAdd() {
            GameAPI ar = new GameAPI(0, CardState_Enum.NA);
            if (CheckTurnAndUI(ar)) {
                if (ar.P.PlayerTurnPhase <= TurnPhase_Enum.Influence) {
                    if (ar.P.Influence >= 2) {
                        if (ar.P.Board.UnitEliteIndex < D.Scenario.UnitEliteDeck.Count) {
                            ar.TurnPhase(TurnPhase_Enum.Influence);
                            ar.ActionInfluence(-2);
                            int cardId = ar.drawEliteUnitToOffering();
                            CardVO unitAdded = D.Cards[cardId];
                            ar.AddLog("Unit " + unitAdded.CardTitle + " was added to the Offering!");
                        } else {
                            ar.ErrorMsg = "You can not add more units, there are none left to add to the offering.";
                        }
                    } else {
                        ar.ErrorMsg = "you do not have enough influence to add a new unit to the offering.  You need 2 points.";
                    }
                } else {
                    ar.ErrorMsg = "you can not recruit during the " + D.LocalPlayer.PlayerTurnPhase + " player phase.";
                }
            }
            ProcessActionResultVO(ar);
        }

        public void OnClick_CityGreen_LearnAction() {
            GameAPI ar = new GameAPI(Card.UniqueId, CardState_Enum.NA);
            if (CheckTurnAndUI(ar)) {
                if (ar.P.PlayerTurnPhase <= TurnPhase_Enum.Influence) {
                    int actionCost = 6;
                    if (ar.P.Influence >= actionCost) {
                        ar.TurnPhase(TurnPhase_Enum.Influence);
                        ar.ActionInfluence(-1 * actionCost);
                        ar.AddCardToTopOfDeck(Card.UniqueId);
                        ar.removeFromAdvancedOffering(Card.UniqueId);
                        SelectedCardSlot = null;
                        ar.drawAdvancedToOffering();
                    } else {
                        ar.ErrorMsg = "you do not have enough influence to learn this action. You need " + actionCost + ".";
                    }
                } else {
                    ar.ErrorMsg = "You can only Learn Actions during the influence phase!";
                }
            }
            ProcessActionResultVO(ar);
        }

        public void OnClick_LearnSpell() {
            GameAPI ar = new GameAPI(Card.UniqueId, CardState_Enum.NA);
            if (CheckTurnAndUI(ar)) {
                if (ar.P.PlayerTurnPhase <= TurnPhase_Enum.Influence) {
                    Image_Enum structure = BasicUtil.GetStructureAtLoc(ar.P.CurrentGridLoc);
                    if (structure == Image_Enum.SH_MageTower || structure == Image_Enum.SH_City_Blue) {
                        if (ar.P.Influence >= 7) {
                            List<Crystal_Enum> cost = new List<Crystal_Enum>() { Card.Costs[0][0] };
                            ar.PayForAction(cost, LearnSpellPaid);
                        } else {
                            ar.ErrorMsg = "you do not have enough influence to learn this spell. You need " + 7 + ".";
                        }
                    } else {
                        ar.ErrorMsg = "You must be in the Blue city or a Mage Tower to Learn a Spell";
                    }
                } else {
                    ar.ErrorMsg = "You can only Learn Spells during the influence phase!";
                }
            }
            ProcessActionResultVO(ar);
        }

        private void LearnSpellPaid(GameAPI ar) {
            ar.TurnPhase(TurnPhase_Enum.Influence);
            ar.ActionInfluence(-7);
            ar.AddCardToTopOfDeck(Card.UniqueId);
            ar.removeFromSpellOffering(Card.UniqueId);
            SelectedCardSlot = null;
            ar.drawSpellToOffering();
            ProcessActionResultVO(ar);
        }

        public void OnClick_AdventureAncientRuins() {
            GameAPI ar = new GameAPI(Card.UniqueId, CardState_Enum.NA);
            if (CheckTurnAndUI(ar)) {
                if (ar.P.PlayerTurnPhase < TurnPhase_Enum.Influence) {
                    SelectedCardSlot = null;
                    CardVO ruinCard = D.Cards[ar.P.Board.MonsterData[ar.P.CurrentGridLoc].Values[0]];
                    ar.UniqueCardId = ruinCard.UniqueId;
                    if (ruinCard.CardType == CardType_Enum.AncientRuins_Alter) {
                        ar.PayForAction(ruinCard.Costs[0], AncientRuins_AlterPaid);
                    } else {
                        List<int> monsterIds = new List<int>();
                        D.Cards[ar.UniqueCardId].Monsters.ForEach(m => {
                            int randomMonster = 0;
                            do {
                                randomMonster = D.Scenario.GetRandomMonster(m);
                            } while (monsterIds.Contains(randomMonster));
                            monsterIds.Add(randomMonster);
                        });
                        List<MonsterMetaData> mmd = monsterIds.ConvertAll(m => new MonsterMetaData(m, Hex.GridPosition, Hex.Structure));
                        Hex.Monsters.Clear();
                        Hex.Monsters.AddRange(mmd);
                        Hex.AdventureAction = Image_Enum.SH_AncientRuins;
                        TriggerBattlePanel.SetupUI(Hex, (h) => {
                            D.A.pd_StartOfTurn = D.LocalPlayer.Clone();
                            D.A.UpdateUI();
                        }, TriggerBattlePanel.RUIN_BATTLE);
                    }
                } else {
                    Msg("You can not perform this action");
                    BottomButtonContainer.Shake();
                }
            }
            ProcessActionResultVO(ar);
        }
        private void AncientRuins_AlterPaid(GameAPI ar) {
            ar.P.Board.MonsterData.Remove(ar.P.CurrentGridLoc);
            ar.TurnPhase(TurnPhase_Enum.AfterBattle);
            ar.Fame(7);
            ar.AddShieldLocation(ar.P.CurrentGridLoc);
            ProcessActionResultVO(ar);
        }
        public void OnClick_Adventure_AncientRuins_RevealMonster() {
            GameAPI ar = new GameAPI();
            if (CheckTurnAndUI(ar)) {
                ar.AcceptPanel("Warning!",
                    "You are about to reveal new information, You Will NOT be able to UNDO this action, would you like to continue?",
                    new List<Action<GameAPI>>() { Adventure_AncientRuins_RevealMonster_Yes, (a) => { } },
                    new List<string> { "Yes", "No" },
                    new List<Color32> { CNAColor.ColorLightGreen, CNAColor.ColorLightRed },
                    CNAColor.ColorLightBlue);
            }
            ProcessActionResultVO(ar);
        }

        public void OnClick_RevealMonster() {
            GameAPI ar = new GameAPI();
            if (CheckTurnAndUI(ar)) {
                ar.AcceptPanel("Warning!",
                    "You are about to reveal new information, You Will NOT be able to UNDO this action, would you like to continue?",
                    new List<Action<GameAPI>>() { RevealMonster_Yes, (a) => { } },
                    new List<string> { "Yes", "No" },
                    new List<Color32> { CNAColor.ColorLightGreen, CNAColor.ColorLightRed },
                    CNAColor.ColorLightBlue);
            }
            ProcessActionResultVO(ar);
        }

        public void RevealMonster_Yes(GameAPI ar) {
            ar.P.VisableMonsters.Add(Hex.Monsters[0].Uniqueid);
            D.A.pd_StartOfTurn = ar.P.Clone();
            ar.PushForce();
        }

        public void Adventure_AncientRuins_RevealMonster_Yes(GameAPI ar) {
            ar.P.VisableMonsters.Add(ar.P.Board.MonsterData[ar.P.CurrentGridLoc].Values[0]);
            D.A.pd_StartOfTurn = ar.P.Clone();
            ar.PushForce();
        }


        #endregion

        #region HexInfoButton
        public void OnClick_HexInfoButton() {
            int cardKey = 0;
            switch (Hex.Structure) {
                case Image_Enum.SH_CrystalMines_Blue: { cardKey = D.GetGameEffectCard(GameEffect_Enum.SH_CrystalMines_Blue).UniqueId; break; }
                case Image_Enum.SH_CrystalMines_Red: { cardKey = D.GetGameEffectCard(GameEffect_Enum.SH_CrystalMines_Red).UniqueId; break; }
                case Image_Enum.SH_CrystalMines_Green: { cardKey = D.GetGameEffectCard(GameEffect_Enum.SH_CrystalMines_Green).UniqueId; break; }
                case Image_Enum.SH_CrystalMines_White: { cardKey = D.GetGameEffectCard(GameEffect_Enum.SH_CrystalMines_White).UniqueId; break; }
                case Image_Enum.SH_Keep: { cardKey = D.GetGameEffectCard(GameEffect_Enum.SH_Keep).UniqueId; break; }
                case Image_Enum.SH_MageTower: { cardKey = D.GetGameEffectCard(GameEffect_Enum.SH_MageTower).UniqueId; break; }
                case Image_Enum.SH_City_Blue: { cardKey = D.GetGameEffectCard(GameEffect_Enum.SH_City_Blue).UniqueId; break; }
                case Image_Enum.SH_City_Red: { cardKey = D.GetGameEffectCard(GameEffect_Enum.SH_City_Red).UniqueId; break; }
                case Image_Enum.SH_City_Green: { cardKey = D.GetGameEffectCard(GameEffect_Enum.SH_City_Green).UniqueId; break; }
                case Image_Enum.SH_City_White: { cardKey = D.GetGameEffectCard(GameEffect_Enum.SH_City_White).UniqueId; break; }
                case Image_Enum.SH_MagicGlade: { cardKey = D.GetGameEffectCard(GameEffect_Enum.SH_MagicGlade).UniqueId; break; }
                case Image_Enum.SH_Draconum: { cardKey = D.GetGameEffectCard(GameEffect_Enum.SH_Draconum).UniqueId; break; }
                case Image_Enum.SH_MaraudingOrcs: { cardKey = D.GetGameEffectCard(GameEffect_Enum.SH_MaraudingOrcs).UniqueId; break; }
                case Image_Enum.SH_Monastery: { cardKey = D.GetGameEffectCard(GameEffect_Enum.SH_Monastery).UniqueId; break; }
                case Image_Enum.SH_StartShrine: { cardKey = D.GetGameEffectCard(GameEffect_Enum.SH_StartShrine).UniqueId; break; }
                case Image_Enum.SH_Village: { cardKey = D.GetGameEffectCard(GameEffect_Enum.SH_Village).UniqueId; break; }
                case Image_Enum.SH_AncientRuins: { cardKey = D.GetGameEffectCard(GameEffect_Enum.SH_AncientRuins).UniqueId; break; }
                case Image_Enum.SH_Dungeon: { cardKey = D.GetGameEffectCard(GameEffect_Enum.SH_Dungeon).UniqueId; break; }
                case Image_Enum.SH_MonsterDen: { cardKey = D.GetGameEffectCard(GameEffect_Enum.SH_MonsterDen).UniqueId; break; }
                case Image_Enum.SH_SpawningGround: { cardKey = D.GetGameEffectCard(GameEffect_Enum.SH_SpawningGround).UniqueId; break; }
                case Image_Enum.SH_Tomb: { cardKey = D.GetGameEffectCard(GameEffect_Enum.SH_Tomb).UniqueId; break; }
            }
            SetupUI(cardKey, CardHolder_Enum.Info);
        }
        #endregion

        public override void Msg(string msg) {
            Notification.Msg(msg);
        }

        public override void ProcessActionResultVO(GameAPI ar) {
            if (ar.Status) {
                ar.CompleteAction();
            } else {
                Msg(ar.ErrorMsg);
                BottomButtonContainer.Shake();
                ar.Rollback();
            }
        }

        public override void PayForAction(GameAPI ar, List<Crystal_Enum> cost, Action<GameAPI> cancelCallback, Action<GameAPI> acceptCallback, bool all = true) {
            bool day = D.Scenario.isDay;
            string paymentTitle = "Payment :: " + Card.CardTitle;
            ManaPayPanel.SetupUI(paymentTitle, D.LocalPlayer.Crystal, D.LocalPlayer.Mana,
                D.LocalPlayer.ManaPoolAvailable, ar.P.ManaPool.ConvertAll(m => m.ManaColor), day, ar, cost,
                cancelCallback, acceptCallback, all);
        }

        public override bool isConformationCanvasOpen() {
            return ConformationCanvas.Active;
        }

        public override void SelectSingleCard(GameAPI ar, Action<GameAPI> cancelCallback, Action<GameAPI> acceptCallback, bool allowNone = false) {
            SelectSingleCardPanel.SetupUI(ar, cancelCallback, acceptCallback, allowNone);
        }

        public override void SelectOptions(GameAPI ar, Action<GameAPI> cancelCallback, Action<GameAPI> acceptCallback, params OptionVO[] options) {
            SelectOptionsPanel.SetupUI(ar, cancelCallback, acceptCallback, options);
        }

        public override void SelectCards(GameAPI ar, List<int> cards, string title, string description, V2IntVO selectCount, Image_Enum selectionImage, List<string> buttonText, List<Color> buttonColor, List<Action<GameAPI>> buttonCallback, List<bool> buttonForce) {
            SelectCardsPanel.SetupUI(ar, cards, title, description, selectCount, selectionImage, buttonText, buttonColor, buttonCallback, buttonForce);
        }

        public override void SelectLevelUp(GameAPI ar, Action<GameAPI> callback, List<int> actionOffering, List<int> skillOffering, List<int> skills) {
            LevelUpPanel.SetupUI(ar, callback, actionOffering, skillOffering, skills);
        }

        public override void SelectManaDie(GameAPI ar, List<Image_Enum> die, string title, string description, V2IntVO selectCount, Image_Enum selectionImage, List<string> buttonText, List<Color> buttonColor, List<Action<GameAPI>> buttonCallback, List<bool> buttonForce) {
            SelectManaPanel.SetupUI(ar, die, title, description, selectCount, Image_Enum.I_check, buttonText, buttonColor, buttonCallback, buttonForce);
        }

        public override void SelectYesNo(string title, string description, Action yes, Action no) {
            YesNoPanel.SetupUI(title, description, yes, no);
        }
        public override void SelectAcceptPanel(GameAPI ar, string head, string body, List<Action<GameAPI>> callbacks, List<string> buttonText, List<Color32> buttonColor, Color32 backgroundColor) {
            AcceptPanel.SetupUI(ar, head, body, callbacks, buttonText, buttonColor, backgroundColor);
        }
        public override void WaitOnServerPanel(GameAPI ar) {
            WaitingOnServerPan.SetupUI(ar);
        }

        public override void Clear() {
            SelectedCardSlot = null;
        }
    }
}
