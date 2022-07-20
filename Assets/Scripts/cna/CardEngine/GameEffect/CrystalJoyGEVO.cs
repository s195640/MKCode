using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cna.poo;

namespace cna {
    public class CrystalJoyGEVO : CardGameEffectVO {
        public CrystalJoyGEVO(int uniqueId) : base(
            uniqueId, "Crystal Joy", Image_Enum.I_cardBackRounded,
            CardType_Enum.GameEffect,
            GameEffect_Enum.CB_CrystalJoy,
            GameEffectDuration_Enum.Turn,
            CNAColor.ColorLightBlue,
            "End of turn, discard another non-wound card to keep Crystal Joy in your hand.",
            true, false, false
            ) {
            GameEffectClickable = false;
            Actions = new List<string>() { "You have the option of Keeping Crystal Joy in your hand.  Select a non-wound card from your hand and Accept.  Click the Cancel button to decline." };
            Costs = new List<List<Crystal_Enum>> { new List<Crystal_Enum> { Crystal_Enum.NA } };
            Allowed = new List<List<TurnPhase_Enum>>() { new List<TurnPhase_Enum>() { TurnPhase_Enum.SetupTurn, TurnPhase_Enum.StartTurn, TurnPhase_Enum.Move, TurnPhase_Enum.Influence, TurnPhase_Enum.Battle, TurnPhase_Enum.AfterBattle, TurnPhase_Enum.Resting, TurnPhase_Enum.Exhaustion, TurnPhase_Enum.Reward, TurnPhase_Enum.EndTurn_TheRightMoment, TurnPhase_Enum.EndTurn, TurnPhase_Enum.EndOfRound } };
            BattleAllowed = new List<List<BattlePhase_Enum>>() { new List<BattlePhase_Enum>() { BattlePhase_Enum.NA, BattlePhase_Enum.StartOfBattle, BattlePhase_Enum.SetupProvoke, BattlePhase_Enum.Provoke, BattlePhase_Enum.RangeSiege, BattlePhase_Enum.Block, BattlePhase_Enum.AssignDamage, BattlePhase_Enum.Attack, BattlePhase_Enum.EndOfBattle, BattlePhase_Enum.None } };
        }

        public override void ActionPaymentComplete_00(GameAPI ar) {
            ar.SelectSingleCard(acceptCallback_00, rejectCallback_00);
        }
        public void rejectCallback_00(GameAPI ar) {
            ar.AddLog("[Crystal Joy] - rejected by player!");
            ar.FinishCallback(ar);
        }
        public void acceptCallback_00(GameAPI ar) {
            ar.AddLog("[Crystal Joy] - Will be keep in hand!");
            ar.P.Deck.Hand.ForEach(cardid => {
                if (D.Cards[cardid].CardImage == Image_Enum.CB_crystal_joy) {
                    ar.P.Deck.State.Remove(cardid);
                }
            });
            ar.AddCardState(ar.SelectedUniqueCardId, CardState_Enum.Discard);
            ar.FinishCallback(ar);
        }

        public override string IsSelectionAllowed(CardVO card, CardHolder_Enum cardHolder, GameAPI ar) {
            string msg = base.IsSelectionAllowed(card, cardHolder, ar);
            if (msg.Length == 0) {
                if (card.CardType == CardType_Enum.Wound) {
                    msg = "You can only play Action cards!";
                }
            }
            return msg;
        }
    }
}
