using System.Collections.Generic;
using cna.poo;
namespace cna {
    public partial class WHITE_LeavesintheWindVO : CardSkillVO {
        public WHITE_LeavesintheWindVO(int uniqueId) : base(
            uniqueId,
            "Leaves in the Wind",
            CardType_Enum.Skill,
            SkillRefresh_Enum.Round,
            Image_Enum.SKW_leaves_in_the_wind,
            Image_Enum.SKW_back,
            Image_Enum.A_meeple_norowas,
            new List<List<TurnPhase_Enum>> { new List<TurnPhase_Enum>() { TurnPhase_Enum.Move, TurnPhase_Enum.Influence, TurnPhase_Enum.Battle, TurnPhase_Enum.AfterBattle } },
            new List<List<BattlePhase_Enum>> { new List<BattlePhase_Enum>() { BattlePhase_Enum.RangeSiege, BattlePhase_Enum.Block, BattlePhase_Enum.AssignDamage, BattlePhase_Enum.Attack, BattlePhase_Enum.EndOfBattle } },
            false,
            "Once a Round: Flip this to gain one green crystal to your Inventory, and one white mana token"
            ) { }
    }
}
