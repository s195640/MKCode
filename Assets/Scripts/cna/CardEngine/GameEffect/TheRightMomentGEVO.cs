using System.Collections.Generic;
using cna.poo;

namespace cna {
    public class TheRightMomentGEVO : CardGameEffectVO {
        public TheRightMomentGEVO(int uniqueId) : base(
            uniqueId, "The Right Moment", Image_Enum.I_crystal_black,
                CardType_Enum.GameEffect,
                GameEffect_Enum.T_TheRightMoment01,
                GameEffectDuration_Enum.Round,
                CNAColor.ColorLightBlue,
                "You may elect to take another turn after this one.",
                true, false, false
            ) {
            GameEffectClickable = true;
            Actions = new List<string>() { "" };
            Costs = new List<List<Crystal_Enum>> { new List<Crystal_Enum> { Crystal_Enum.NA } };
            Allowed = new List<List<TurnPhase_Enum>>() { new List<TurnPhase_Enum>() { TurnPhase_Enum.StartTurn, TurnPhase_Enum.Move, TurnPhase_Enum.Influence, TurnPhase_Enum.Battle, TurnPhase_Enum.AfterBattle } };
            BattleAllowed = new List<List<BattlePhase_Enum>>() { new List<BattlePhase_Enum>() { BattlePhase_Enum.Provoke, BattlePhase_Enum.RangeSiege, BattlePhase_Enum.Block, BattlePhase_Enum.AssignDamage, BattlePhase_Enum.Attack, BattlePhase_Enum.EndOfBattle } };
        }
        public override GameAPI ActionValid_00(GameAPI ar) {
            D.Action.Clear();
            ar.RemoveGameEffect(GameEffect_Enum.T_TheRightMoment01);
            ar.AddGameEffect(GameEffect_Enum.T_TheRightMoment02);
            return ar;
        }
    }
}
