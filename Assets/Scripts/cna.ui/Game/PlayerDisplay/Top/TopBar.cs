using System.Collections.Generic;
using cna.poo;
using UnityEngine;

namespace cna.ui {
    public class TopBar : UIGameBase {

        [Header("Left")]
        [SerializeField] private CNA_Button undoButton;
        [SerializeField] private CNA_Button restButton;
        [SerializeField] private CNA_Button endTurnButton;

        [Header("Right")]
        [SerializeField] private CNA_Button mapButton;
        [SerializeField] private CNA_Button boardButton;
        [SerializeField] private CNA_Button battleButton;
        [SerializeField] private CNA_Button multiBattleButton;
        [SerializeField] private List<CNA_Button> ledendMonsterButton;
        [SerializeField] private List<CNA_Button> ledendRuinsButton;
        [SerializeField] private List<CNA_Button> ledendIconButton;
        [SerializeField] private CNA_Button scoreButton;
        [SerializeField] private CNA_Button settingsButton;
        [SerializeField] private CNA_Button finalScoreButton;

        [SerializeField] private ScreensCanvas ScreensCanvas;
        [SerializeField] private NotificationCanvas Notification;
        [SerializeField] private YesNoPanel YesNoPanel;

        public override void SetupUI() {
            if (D.HostPlayer.Key != D.LocalPlayerKey) {
                settingsButton.gameObject.SetActive(false);
            }
        }

        public void UpdateUI() {
            CheckSetupUI();
            UpdateEndTurnButton();
            UpdateUI_ButtonActive(D.ScreenState);
        }

        private void UpdateUI_ButtonActive(ScreenState_Enum screenState) {
            mapButton.Active = screenState != ScreenState_Enum.Map;
            boardButton.Active = screenState != ScreenState_Enum.Offering;
            battleButton.Active = screenState != ScreenState_Enum.Combat;
            multiBattleButton.Active = screenState != ScreenState_Enum.MultiCombat;
            ledendMonsterButton.ForEach(z => z.Active = screenState != ScreenState_Enum.Legend_Monsters);
            ledendRuinsButton.ForEach(z => z.Active = screenState != ScreenState_Enum.Legend_Ruins);
            ledendIconButton.ForEach(z => z.Active = screenState != ScreenState_Enum.Legend_Icons);
            scoreButton.Active = screenState != ScreenState_Enum.Score;
            settingsButton.Active = screenState != ScreenState_Enum.Settings;
            battleButton.BlickButton(D.LocalPlayer.PlayerTurnPhase == TurnPhase_Enum.Battle && screenState != ScreenState_Enum.Combat);
            battleButton.ButtonColor = D.LocalPlayer.PlayerTurnPhase == TurnPhase_Enum.Battle ? CNAColor.ColorLightRed : CNAColor.ColorLightGreen;
        }

        public void OnClick_Map() {
            D.ScreenState = ScreenState_Enum.Map;
            UpdateUI_ButtonActive(D.ScreenState);
            ScreensCanvas.UpdateUI();
        }

        public void OnClick_Offering() {
            D.ScreenState = ScreenState_Enum.Offering;
            UpdateUI_ButtonActive(D.ScreenState);
            ScreensCanvas.UpdateUI();
        }

        public void OnClick_Combat() {
            D.ScreenState = ScreenState_Enum.Combat;
            UpdateUI_ButtonActive(D.ScreenState);
            ScreensCanvas.UpdateUI();
        }

        public void OnClick_MultiCombat() {
            D.ScreenState = ScreenState_Enum.MultiCombat;
            UpdateUI_ButtonActive(D.ScreenState);
            ScreensCanvas.UpdateUI();
        }

        public void OnClick_LedendMonster() {
            D.ScreenState = ScreenState_Enum.Legend_Monsters;
            UpdateUI_ButtonActive(D.ScreenState);
            ScreensCanvas.UpdateUI();
        }
        public void OnClick_LedendRuins() {
            D.ScreenState = ScreenState_Enum.Legend_Ruins;
            UpdateUI_ButtonActive(D.ScreenState);
            ScreensCanvas.UpdateUI();
        }
        public void OnClick_LedendIcons() {
            D.ScreenState = ScreenState_Enum.Legend_Icons;
            UpdateUI_ButtonActive(D.ScreenState);
            ScreensCanvas.UpdateUI();
        }

        public void OnClick_Score() {
            D.ScreenState = ScreenState_Enum.Score;
            UpdateUI_ButtonActive(D.ScreenState);
            ScreensCanvas.UpdateUI();
        }

        public void OnClick_Settings() {
            D.ScreenState = ScreenState_Enum.Settings;
            UpdateUI_ButtonActive(D.ScreenState);
            ScreensCanvas.UpdateUI();
        }

        public void OnClick_FinalScore() {
            D.ScreenState = ScreenState_Enum.FinalScore;
            UpdateUI_ButtonActive(D.ScreenState);
            ScreensCanvas.UpdateUI();
        }



        public void Msg(string msg) {
            Notification.Msg(msg);
        }


        public void UpdateEndTurnButton() {
            if (D.LocalPlayer.PlayerTurnPhase == TurnPhase_Enum.StartTurn && D.LocalPlayer.Deck.Deck.Count == 0 && !D.LocalPlayer.ActionTaken) {
                endTurnButton.ButtonText = "End Round";
                endTurnButton.ButtonTextColor = CNAColor.YELLOW;
            } else {
                endTurnButton.ButtonText = "End Turn";
                endTurnButton.ButtonTextColor = CNAColor.DefaultText;
            }
        }

        public void OnClick_EndOfTurn() {
            if (D.isTurn) {
                PlayerData localPlayer = D.LocalPlayer;
                if (localPlayer.PlayerTurnPhase != TurnPhase_Enum.Battle) {
                    if (!D.Action.isConformationCanvasOpen()) {
                        if (localPlayer.PlayerTurnPhase == TurnPhase_Enum.StartTurn && localPlayer.Deck.Deck.Count == 0 && !localPlayer.ActionTaken) {
                            bool longNight = localPlayer.GameEffects.ContainsKey(GameEffect_Enum.T_LongNight);
                            string msg = "You are about to declare End of Round, each other player will get 1 more turn.";
                            if (longNight) {
                                msg += "\n\nIf you continue you will LOSE the ability to use Long Night!";
                            }
                            msg += "\n\nDo you want to continue?";
                            YesNoPanel.SetupUI("End of Round", msg, DeclareEndOfRound, NO);
                        } else {
                            int totalNonWoundCards = 0;
                            bool cardHasBeenPlayed = false;
                            localPlayer.Deck.Hand.ForEach(c => {
                                CardVO card = D.Cards[c];
                                if (card.CardType != CardType_Enum.Wound) {
                                    totalNonWoundCards++;
                                    if (localPlayer.Deck.State.ContainsKey(c)) {
                                        cardHasBeenPlayed = cardHasBeenPlayed || localPlayer.Deck.State[c].ContainsAny(CardState_Enum.Discard, CardState_Enum.Basic, CardState_Enum.Normal, CardState_Enum.Advanced, CardState_Enum.Trashed);
                                    }
                                }
                            });
                            if (cardHasBeenPlayed || totalNonWoundCards == 0) {
                                EndOfTurn(localPlayer);
                            } else {
                                Msg("You must play at least 1 non wound card before ending your turn.");
                                endTurnButton.ShakeButton();
                            }
                        }
                    } else {
                        Msg("Can not perform action while another window is open!");
                    }
                } else {
                    Msg("You must complete the battle before ending your turn.");
                    endTurnButton.ShakeButton();
                }
            } else {
                Msg("It is not your turn!");
                endTurnButton.ShakeButton();
            }
        }

        int MagicGlade_woundInHand = 0;
        int MagicGlade_woundInDiscard = 0;
        int MagicGlade_woundInHandDiscard = 0;
        public void EndOfTurn(PlayerData localPlayer) {
            if (localPlayer.GameEffects.ContainsKey(GameEffect_Enum.SH_MagicGlade)) {
                MagicGlade_woundInHand = 0;
                MagicGlade_woundInDiscard = 0;
                MagicGlade_woundInHandDiscard = 0;
                MagicGlade_woundInHand = localPlayer.Deck.Hand.Find(c => {
                    if (D.Cards[c].CardType == CardType_Enum.Wound) {
                        return !localPlayer.Deck.State.ContainsKey(c);
                    }
                    return false;
                });
                MagicGlade_woundInHandDiscard = localPlayer.Deck.Hand.Find(c => {
                    if (D.Cards[c].CardType == CardType_Enum.Wound) {
                        if (localPlayer.Deck.State.ContainsKey(c)) {
                            if (!localPlayer.Deck.State[c].Contains(CardState_Enum.Trashed)) {
                                return true;
                            }
                        }
                    }
                    return false;
                });
                MagicGlade_woundInDiscard = localPlayer.Deck.Discard.Find(c => D.Cards[c].CardType == CardType_Enum.Wound);
                if (MagicGlade_woundInHand > 0 || MagicGlade_woundInDiscard > 0 || MagicGlade_woundInHandDiscard > 0) {
                    GameAPI ar = new GameAPI(D.GetGameEffectCard(GameEffect_Enum.SH_MagicGlade).UniqueId, CardState_Enum.NA, 0);
                    if (MagicGlade_woundInHand > 0 && (MagicGlade_woundInDiscard > 0 || MagicGlade_woundInHandDiscard > 0)) {
                        D.Action.SelectOptions(ar, EndOfTurn_MagicGladeReject, EndOfTurn_MagicGladeAccept, new OptionVO("Wound From Hand", Image_Enum.I_healHand), new OptionVO("Wound From Discard", Image_Enum.I_healHand));
                    } else {
                        D.Action.SelectOptions(ar, EndOfTurn_MagicGladeReject, EndOfTurn_MagicGladeAccept, new OptionVO(MagicGlade_woundInHand > 0 ? "Wound From Hand" : "Wound From Discard", Image_Enum.I_healHand));
                    }
                } else {
                    EOT_CheckCrystalJoy(localPlayer);
                }
            } else {
                EOT_CheckCrystalJoy(localPlayer);
            }
        }
        public void EndOfTurn_MagicGladeReject(GameAPI ar) {
            D.C.LogMessage("[Magical Glade] - No wound removed, rejected by player!");
            EOT_CheckCrystalJoy(ar.P);
        }
        public void EndOfTurn_MagicGladeAccept(GameAPI ar) {
            switch (ar.SelectedButtonIndex) {
                case 0: {
                    if (MagicGlade_woundInHand > 0) {
                        D.C.LogMessage("[Magical Glade] - Wound in hand Trashed!");
                        ar.P.Deck.AddState(MagicGlade_woundInHand, CardState_Enum.Trashed);
                    } else {
                        D.C.LogMessage("[Magical Glade] - Wound in discard Trashed!");
                        if (MagicGlade_woundInHandDiscard > 0) {
                            ar.P.Deck.AddState(MagicGlade_woundInHandDiscard, CardState_Enum.Trashed);
                        } else {
                            ar.P.Deck.Discard.Remove(MagicGlade_woundInDiscard);
                        }
                    }
                    break;
                }
                case 1: {
                    D.C.LogMessage("[Magical Glade] - Wound in discard Trashed!");
                    if (MagicGlade_woundInHandDiscard > 0) {
                        ar.P.Deck.AddState(MagicGlade_woundInHandDiscard, CardState_Enum.Trashed);
                    } else {
                        ar.P.Deck.Discard.Remove(MagicGlade_woundInDiscard);
                    }
                    break;
                }
            }
            EOT_CheckCrystalJoy(ar.P);
        }

        private void EOT_CheckCrystalJoy(PlayerData localPlayer) {
            if (localPlayer.GameEffects.ContainsKey(GameEffect_Enum.CB_CrystalJoy)) {
                GameAPI ar = new GameAPI(D.GetGameEffectCard(GameEffect_Enum.CB_CrystalJoy).UniqueId, CardState_Enum.NA, 0);
                ar.FinishCallback = (ar) => EndOfTurn_Result();
                ar.Card.OnClick_ActionButton(ar);
            } else {
                EndOfTurn_Result();
            }
        }


        private void EndOfTurn_Result() {
            D.A.Gd_StartOfTurnFlag = false;
            PlayerData localPlayer = D.LocalPlayer;
            if (localPlayer.GameEffects.ContainsKey(GameEffect_Enum.Rewards)) {
                CardVO Card = D.GetGameEffectCard(GameEffect_Enum.Rewards);
                GameAPI ar = new GameAPI(Card.UniqueId, CardState_Enum.NA, 0);
                ar.TurnPhase(TurnPhase_Enum.Reward);
                Card.OnClick_ActionButton(ar);
            } else {
                GameAPI ar = new GameAPI();
                if (ar.P.GameEffects.ContainsKey(GameEffect_Enum.T_TheRightMoment02)) {
                    ar.RemoveGameEffect(GameEffect_Enum.T_TheRightMoment02);
                    ar.TurnPhase(TurnPhase_Enum.EndTurn_TheRightMoment);
                } else {
                    ar.TurnPhase(TurnPhase_Enum.EndTurn);
                }
                ar.CompleteAction();
            }
        }


        public void DeclareEndOfRound() {
            D.A.Gd_StartOfTurnFlag = false;
            GameAPI ar = new GameAPI();
            ar.TurnPhase(TurnPhase_Enum.EndOfRound);
            ar.CompleteAction();
        }

        public void NO() { }

        public void OnClick_Rest() {
            if (D.isTurn) {
                if (D.LocalPlayer.PlayerTurnPhase <= TurnPhase_Enum.StartTurn) {
                    GameAPI ar = new GameAPI(D.G, D.LocalPlayer);
                    ar.TurnPhase(TurnPhase_Enum.Resting);
                    ar.AddLog("[Resting]");
                    ar.CompleteAction();
                } else {
                    Msg("You can only rest at the begining of your turn!");
                    restButton.ShakeButton();
                }
            } else {
                Msg("It is not your turn!");
                restButton.ShakeButton();
            }
        }

        public void OnClick_Undo() {
            if (D.isTurn) {
                D.A.OnClick_Undo();
            } else {
                Msg("It is not your turn!");
                undoButton.ShakeButton();
            }
        }
    }
}
