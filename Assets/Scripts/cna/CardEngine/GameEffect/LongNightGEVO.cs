using System;
using System.Collections.Generic;
using cna.poo;
using UnityEngine;

namespace cna {
    public class LongNightGEVO : CardGameEffectVO {
        public LongNightGEVO(int uniqueId) : base(
            uniqueId, "Long Night", Image_Enum.I_cardBackRounded,
                CardType_Enum.GameEffect,
                GameEffect_Enum.T_LongNight,
                GameEffectDuration_Enum.Round,
                CNAColor.ColorLightBlue,
                "Once this round if your deck is empty, shuffle your discard and add 3 cards from your discard to your hand.",
                true, false, false
            ) {
            GameEffectClickable = true;
            Actions = new List<string>() { "" };
            Costs = new List<List<Crystal_Enum>> { new List<Crystal_Enum> { Crystal_Enum.NA } };
            Allowed = new List<List<TurnPhase_Enum>>() { new List<TurnPhase_Enum>() { TurnPhase_Enum.StartTurn, TurnPhase_Enum.Move, TurnPhase_Enum.Influence, TurnPhase_Enum.Battle, TurnPhase_Enum.AfterBattle } };
            BattleAllowed = new List<List<BattlePhase_Enum>>() { new List<BattlePhase_Enum>() { BattlePhase_Enum.Provoke, BattlePhase_Enum.RangeSiege, BattlePhase_Enum.Block, BattlePhase_Enum.AssignDamage, BattlePhase_Enum.Attack, BattlePhase_Enum.EndOfBattle } };
        }

        public override void ActionPaymentComplete_00(GameAPI ar) {
            if (ar.P.Deck.Deck.Count == 0) {
                ar.AcceptPanel("Warning!",
                    "You are about to reveal new information, You Will NOT be able to UNDO this action, would you like to continue?",
                    new List<Action<GameAPI>>() { (a) => { ActionValid_00_Yes(a); }, (a) => { } },
                    new List<string> { "Yes", "No" },
                    new List<Color32> { CNAColor.ColorLightGreen, CNAColor.ColorLightRed },
                    CNAColor.ColorLightBlue);
            } else {
                ar.ErrorMsg = "Your Deck must be empty to use this";
            }
        }

        public void ActionValid_00_Yes(GameAPI ar) {
            ar.change();
            D.Action.Clear();
            ar.RemoveGameEffect(GameEffect_Enum.T_LongNight);
            ar.P.Deck.Discard.ShuffleDeck();
            for (int i = 0; i < 3; i++) {
                ar.P.Deck.Deck.Add(BasicUtil.DrawCard(ar.P.Deck.Discard));
            }
            ar.FinishCallback(ar);
        }
    }
}
