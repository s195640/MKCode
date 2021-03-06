using System;
using System.Collections.Generic;
using cna.poo;
using UnityEngine;

namespace cna {
    public class CardTacticVO : CardActionVO {
        public CardTacticVO(int uniqueId, string title, Image_Enum image, CardType_Enum cardType, int initiative) : base(
            uniqueId,
            title,
            image,
            cardType,
            new List<string>(),
            new List<List<Crystal_Enum>>(),
            new List<List<TurnPhase_Enum>>()
            ) {
        }
        private List<ManaPoolData> manaDieCalc = new List<ManaPoolData>();
        public override void OnClick_ActionButton(GameAPI ar) {
            ar.TurnPhase(TurnPhase_Enum.TacticsAction);
            ar.UpdateUI();
            switch (ar.Card.CardImage) {
                case Image_Enum.T_day_2: {
                    List<int> cards = ar.P.Deck.Hand;
                    string title = ar.Card.CardTitle;
                    string description = "Select up to 3 cards to discard, you will then draw that many cards into your hand.";
                    V2IntVO selectCount = new V2IntVO(0, 3);
                    Image_Enum selectionImage = Image_Enum.I_disable;
                    List<string> buttonText = new List<string>() { "Discard" };
                    List<Color> buttonColor = new List<Color>() { CNAColor.ColorLightBlue };
                    List<Action<GameAPI>> buttonCallback = new List<Action<GameAPI>>() { Day02 };
                    List<bool> buttonForce = new List<bool>() { false };
                    ar.SelectCards(cards, title, description, selectCount, selectionImage, buttonText, buttonColor, buttonCallback, buttonForce);
                    break;
                }
                case Image_Enum.T_day_3: {
                    manaDieCalc.Clear();
                    manaDieCalc = ar.P.ManaPool.FindAll(m => m.ManaColor != Crystal_Enum.Gold && m.ManaColor != Crystal_Enum.Black);
                    OptionVO[] p = new OptionVO[manaDieCalc.Count];
                    for (int i = 0; i < manaDieCalc.Count; i++) {
                        p[i] = new OptionVO("Mana Die", BasicUtil.Convert_CrystalToManaDieImageId(manaDieCalc[i].ManaColor));
                    }
                    ar.Card.Actions.Clear();
                    ar.ActionIndex = 0;
                    ar.Card.Actions.Add("Select a mana die to reserve for your use during this turn!");
                    ar.SelectOptions(Day03, null, p);
                    break;
                }
                case Image_Enum.T_day_4: {
                    ar.P.AddGameEffect(GameEffect_Enum.T_Planning);
                    Finish(ar);
                    break;
                }
                case Image_Enum.T_day_5: {
                    ar.DrawCard(2, Finish, false);
                    break;
                }
                case Image_Enum.T_day_6: {
                    ar.P.AddGameEffect(GameEffect_Enum.T_TheRightMoment01);
                    Finish(ar);
                    break;
                }
                case Image_Enum.T_night_2: {
                    ar.P.AddGameEffect(GameEffect_Enum.T_LongNight);
                    Finish(ar);
                    break;
                }
                case Image_Enum.T_night_3: {
                    ar.P.AddGameEffect(GameEffect_Enum.T_ManaSearch01);
                    Finish(ar);
                    break;
                }
                case Image_Enum.T_night_4: {
                    ar.P.AddGameEffect(GameEffect_Enum.T_MidnightMeditation);
                    Finish(ar);
                    break;
                }
                case Image_Enum.T_night_5: {
                    List<int> cards = ar.P.Deck.Deck;
                    string title = ar.Card.CardTitle;
                    string description = "Select a card from your Deck to add to your hand.";
                    V2IntVO selectCount = new V2IntVO(1, 1);
                    Image_Enum selectionImage = Image_Enum.I_check;
                    List<string> buttonText = new List<string>() { "Add Card" };
                    List<Color> buttonColor = new List<Color>() { CNAColor.ColorLightBlue };
                    List<Action<GameAPI>> buttonCallback = new List<Action<GameAPI>>() { Night05 };
                    List<bool> buttonForce = new List<bool>() { true };
                    ar.SelectCards(cards, title, description, selectCount, selectionImage, buttonText, buttonColor, buttonCallback, buttonForce);
                    break;
                }
                case Image_Enum.T_night_6: {
                    ar.P.AddGameEffect(GameEffect_Enum.T_SparingPower);
                    Finish(ar);
                    break;
                }
                default: {
                    Finish(ar);
                    break;
                }
            }
        }

        private void Night05(GameAPI ar) {
            ar.P.Deck.Deck.Remove(ar.SelectedCardIds[0]);
            ar.P.Deck.Hand.Add(ar.SelectedCardIds[0]);
            ar.P.Deck.Deck.ShuffleDeck();
            Finish(ar);
        }

        private void Day02(GameAPI ar) {
            ar.DrawCard(ar.SelectedCardIds.Count, Day02b, false);
        }

        private void Day02b(GameAPI ar) {
            ar.SelectedCardIds.ForEach(c => {
                ar.P.Deck.Deck.Add(c);
                ar.P.Deck.Hand.Remove(c);
            });
            ar.P.Deck.Deck.ShuffleDeck();
            Finish(ar);
        }

        private void Day03(GameAPI ar) {
            manaDieCalc[ar.SelectedButtonIndex].Status = ManaPool_Enum.ManaSteal;
            ar.P.AddGameEffect(GameEffect_Enum.T_ManaSteal, (int)manaDieCalc[ar.SelectedButtonIndex].ManaColor);
            Finish(ar);
        }

        public void Finish(GameAPI ar) {
            ar.TurnPhase(TurnPhase_Enum.TacticsEnd);
            ar.CompleteAction();
        }
    }
}
