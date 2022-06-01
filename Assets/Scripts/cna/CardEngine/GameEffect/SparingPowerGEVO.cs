using System.Collections.Generic;
using cna.poo;

namespace cna {
    public class SparingPowerGEVO : CardGameEffectVO {
        public SparingPowerGEVO(int uniqueId) : base(
            uniqueId, "Sparing Power",
            Image_Enum.I_cardBackRounded,
            CardType_Enum.GameEffect,
            GameEffect_Enum.T_SparingPower,
            GameEffectDuration_Enum.Round,
            CNAColor.ColorLightBlue,
            "At the start of turn, Choose 1, set a card from the top of deck asside for later use.  OR use add that set of cards to your hand.",
            true, false, false
            ) {
            GameEffectClickable = false;
            Actions = new List<string>() { "" };
            Costs = new List<List<Crystal_Enum>> { new List<Crystal_Enum> { Crystal_Enum.NA } };
            Allowed = new List<List<TurnPhase_Enum>>() { new List<TurnPhase_Enum>() { TurnPhase_Enum.StartTurn } };
            BattleAllowed = new List<List<BattlePhase_Enum>>() { new List<BattlePhase_Enum>() { } };
        }

        public override string GameEffectDescription {
            get {
                int totalCards = D.LocalPlayer.GameEffects[GameEffect_Enum.T_SparingPower].Count - 1;
                string msg = "At the start of turn, Choose 1, set a card from the top of deck asside for later use.  OR use add that set of cards to your hand.";
                msg += "\n\nTotal Cards = " + totalCards;
                return msg;
            }
        }

        public override List<string> Actions {
            get {
                int totalCards = D.LocalPlayer.GameEffects[GameEffect_Enum.T_SparingPower].Count - 1;
                return new List<string>() { "Select One, 1) Pull in Sparing Power deck into your hand. 2) Add 1 card from your Deck to the Sparing Power Deck.\n\nTotal Cards = " + totalCards };
            }
        }
    }
}
