using System;
using System.Collections.Generic;
using cna.poo;
using UnityEngine;

namespace cna {
    public class MidnightMeditationGEVO : CardGameEffectVO {
        public MidnightMeditationGEVO(int uniqueId) : base(
            uniqueId, "Midnight Meditation", Image_Enum.I_rest,
                CardType_Enum.GameEffect,
                GameEffect_Enum.T_MidnightMeditation,
                GameEffectDuration_Enum.Round,
                CNAColor.ColorLightBlue,
                "At the start of turn, shuffle up to 5 cards from your hand into your deck and draw that many cards.",
                true, false, false
            ) {
            GameEffectClickable = true;
            Actions = new List<string>() { "" };
            Costs = new List<List<Crystal_Enum>> { new List<Crystal_Enum> { Crystal_Enum.NA } };
            Allowed = new List<List<TurnPhase_Enum>>() { new List<TurnPhase_Enum>() { TurnPhase_Enum.StartTurn } };
            BattleAllowed = new List<List<BattlePhase_Enum>>() { new List<BattlePhase_Enum>() { } };
        }

        public override void ActionPaymentComplete_00(ActionResultVO ar) {
            List<int> cards = ar.LocalPlayer.Deck.Hand;
            string title = ar.Card.CardTitle;
            string description = "Select up to 5 cards to shuffle back into your deck, you will then draw that many cards into your hand.";
            V2IntVO selectCount = new V2IntVO(0, 5);
            Image_Enum selectionImage = Image_Enum.I_disable;
            List<string> buttonText = new List<string>() { "Discard" };
            List<Color> buttonColor = new List<Color>() { CNAColor.ColorLightBlue };
            List<Action<ActionResultVO>> buttonCallback = new List<Action<ActionResultVO>>() { acceptCallback_00 };
            List<bool> buttonForce = new List<bool>() { false };
            ar.SelectCards(cards, title, description, selectCount, selectionImage, buttonText, buttonColor, buttonCallback, buttonForce);
        }

        public void acceptCallback_00(ActionResultVO ar) {
            D.Action.Clear();
            ar.RemoveGameEffect(GameEffect_Enum.T_MidnightMeditation);
            ar.SelectedCardIds.ForEach(c => {
                ar.LocalPlayer.Deck.Deck.Add(c);
                ar.LocalPlayer.Deck.Hand.Remove(c);
            });
            ar.LocalPlayer.Deck.Deck.ShuffleDeck();
            ar.DrawCard(ar.SelectedCardIds.Count, ar.FinishCallback);
        }
    }
}
