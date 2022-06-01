using cna.poo;
using UnityEngine;

namespace cna.ui {
    public class TopBar : MonoBehaviour {
        private Color BLACK = new Color32(51, 51, 51, 255);
        private Color YELLOW = new Color32(255, 255, 0, 255);


        [Header("Left")]
        [SerializeField] private CNA_Button undoButton;
        [SerializeField] private CNA_Button restButton;
        [SerializeField] private CNA_Button endTurnButton;

        [Header("Right")]
        [SerializeField] private CNA_Button mapButton;
        [SerializeField] private CNA_Button boardButton;
        [SerializeField] private CNA_Button battleButton;
        [SerializeField] private CNA_Button scoreButton;

        [SerializeField] private GameObject PlayerOffering;
        [SerializeField] private GameObject WorldMap;
        [SerializeField] private GameObject Battle;
        [SerializeField] private GameObject OptionsCanvas;
        [SerializeField] private NotificationCanvas Notification;
        [SerializeField] private YesNoPanel YesNoPanel;

        public void Start() {
            UpdateUI();
        }

        public void UpdateUI() {
            UpdateEndTurnButton();
            switch (D.ScreenState) {
                case ScreenState_Enum.Map: {
                    mapButton.Active = false;
                    boardButton.Active = true;
                    battleButton.Active = true;
                    scoreButton.Active = true;
                    break;
                }
                case ScreenState_Enum.Offering: {
                    mapButton.Active = true;
                    boardButton.Active = false;
                    battleButton.Active = true;
                    scoreButton.Active = true;
                    break;
                }
                case ScreenState_Enum.Combat: {
                    mapButton.Active = true;
                    boardButton.Active = true;
                    battleButton.Active = false;
                    scoreButton.Active = true;
                    break;
                }
                case ScreenState_Enum.Score: {
                    mapButton.Active = true;
                    boardButton.Active = true;
                    battleButton.Active = true;
                    scoreButton.Active = false;
                    break;
                }
            }
            PlayerOffering.SetActive(!boardButton.Active);
            WorldMap.SetActive(!mapButton.Active);
            Battle.SetActive(!battleButton.Active);
            OptionsCanvas.SetActive(!scoreButton.Active);
        }

        public void OnClick_Map() {
            D.ScreenState = ScreenState_Enum.Map;
            UpdateUI();
        }

        public void OnClick_Offering() {
            D.ScreenState = ScreenState_Enum.Offering;
            UpdateUI();
        }

        public void OnClick_Combat() {
            D.ScreenState = ScreenState_Enum.Combat;
            UpdateUI();
        }

        public void OnClick_Score() {
            D.ScreenState = ScreenState_Enum.Score;
            UpdateUI();
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
                    ActionResultVO ar = new ActionResultVO(D.GetGameEffectCard(GameEffect_Enum.SH_MagicGlade).UniqueId, CardState_Enum.NA, 0);
                    if (MagicGlade_woundInHand > 0 && (MagicGlade_woundInDiscard > 0 || MagicGlade_woundInHandDiscard > 0)) {
                        D.Action.SelectOptions(ar, EndOfTurn_MagicGladeReject, EndOfTurn_MagicGladeAccept, new OptionVO("Wound From Hand", Image_Enum.I_healHand), new OptionVO("Wound From Discard", Image_Enum.I_healHand));
                    } else {
                        D.Action.SelectOptions(ar, EndOfTurn_MagicGladeReject, EndOfTurn_MagicGladeAccept, new OptionVO(MagicGlade_woundInHand > 0 ? "Wound From Hand" : "Wound From Discard", Image_Enum.I_healHand));
                    }
                } else {
                    EndOfTurn_Result();
                }
            } else {
                EndOfTurn_Result();
            }
        }
        public void EndOfTurn_MagicGladeReject(ActionResultVO ar) {
            D.C.LogMessage("[Magical Glade] - No wound removed, rejected by player!");
            EndOfTurn_Result();
        }
        public void EndOfTurn_MagicGladeAccept(ActionResultVO ar) {
            switch (ar.SelectedButtonIndex) {
                case 0: {
                    if (MagicGlade_woundInHand > 0) {
                        D.C.LogMessage("[Magical Glade] - Wound in hand Trashed!");
                        ar.LocalPlayer.Deck.AddState(MagicGlade_woundInHand, CardState_Enum.Trashed);
                    } else {
                        D.C.LogMessage("[Magical Glade] - Wound in discard Trashed!");
                        if (MagicGlade_woundInHandDiscard > 0) {
                            ar.LocalPlayer.Deck.AddState(MagicGlade_woundInHandDiscard, CardState_Enum.Trashed);
                        } else {
                            ar.LocalPlayer.Deck.Discard.Remove(MagicGlade_woundInDiscard);
                        }
                    }
                    break;
                }
                case 1: {
                    D.C.LogMessage("[Magical Glade] - Wound in discard Trashed!");
                    if (MagicGlade_woundInHandDiscard > 0) {
                        ar.LocalPlayer.Deck.AddState(MagicGlade_woundInHandDiscard, CardState_Enum.Trashed);
                    } else {
                        ar.LocalPlayer.Deck.Discard.Remove(MagicGlade_woundInDiscard);
                    }
                    break;
                }
            }
            EndOfTurn_Result();
        }

        private void EndOfTurn_Result() {
            D.A.Gd_StartOfTurnFlag = false;
            if (D.LocalPlayer.GameEffects.ContainsKey(GameEffect_Enum.Rewards)) {
                D.A.PlayerRewards();
            } else {
                D.LocalPlayer.PlayerTurnPhase = TurnPhase_Enum.EndTurn;
                D.C.Send_GameData();
            }
        }


        public void DeclareEndOfRound() {
            D.A.Gd_StartOfTurnFlag = false;
            D.LocalPlayer.PlayerTurnPhase = TurnPhase_Enum.EndTurn;
            if (D.G.EndOfRound == -1) {
                D.G.EndOfRound = D.LocalPlayer.Key;
            }
            D.C.Send_GameData();
        }

        public void NO() { }

        public void OnClick_Rest() {
            if (D.isTurn) {
                if (D.LocalPlayer.PlayerTurnPhase <= TurnPhase_Enum.StartTurn) {
                    D.LocalPlayer.PlayerTurnPhase = TurnPhase_Enum.Resting;
                    D.C.LogMessage("[Resting]");
                    D.C.Send_GameData();
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
