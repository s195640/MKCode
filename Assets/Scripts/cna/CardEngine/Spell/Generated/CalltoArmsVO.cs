using System.Collections.Generic;
using cna.poo;
namespace cna {
    public partial class CalltoArmsVO : CardSpellVO {
        public CalltoArmsVO(int uniqueId) : base(
            uniqueId,
            "Call to Arms",
            "Call to Glory",
            CardType_Enum.Spell,
            Image_Enum.CS_call_to_arms,
            new List<string> { "You may use an ability of one Unit in the Units offer this turn, as if it were one of your recruits. You cannot assign damage to this Unit.", "Recruit any one Unit from the Units offer for free. (If you are at your Command limit, you must disband one of your Units first.)" },
            new List<List<Crystal_Enum>> { new List<Crystal_Enum>() { Crystal_Enum.White }, new List<Crystal_Enum>() { Crystal_Enum.White, Crystal_Enum.Black } },
            new List<List<TurnPhase_Enum>> { new List<TurnPhase_Enum>() { TurnPhase_Enum.Move, TurnPhase_Enum.Influence, TurnPhase_Enum.Battle, TurnPhase_Enum.AfterBattle }, new List<TurnPhase_Enum>() { TurnPhase_Enum.Move, TurnPhase_Enum.Influence, TurnPhase_Enum.Battle, TurnPhase_Enum.AfterBattle } },
            new List<List<BattlePhase_Enum>> { new List<BattlePhase_Enum>() { BattlePhase_Enum.RangeSiege, BattlePhase_Enum.Block, BattlePhase_Enum.AssignDamage, BattlePhase_Enum.Attack, BattlePhase_Enum.EndOfBattle }, new List<BattlePhase_Enum>() { BattlePhase_Enum.RangeSiege, BattlePhase_Enum.Block, BattlePhase_Enum.AssignDamage, BattlePhase_Enum.Attack, BattlePhase_Enum.EndOfBattle } },
            CardColor_Enum.White
            ) { }
    }
}
