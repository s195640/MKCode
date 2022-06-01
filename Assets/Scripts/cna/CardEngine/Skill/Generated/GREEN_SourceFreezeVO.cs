using System.Collections.Generic;
using cna.poo;
namespace cna {
    public partial class GREEN_SourceFreezeVO : CardSkillVO {
        public GREEN_SourceFreezeVO(int uniqueId) : base(
            uniqueId,
            "Source Freeze",
            CardType_Enum.Skill,
            SkillRefresh_Enum.Round,
            Image_Enum.SKG_source_freeze,
            Image_Enum.SKG_back,
            Image_Enum.A_MEEPLE_GREEN,
            new List<List<TurnPhase_Enum>> { new List<TurnPhase_Enum>() { TurnPhase_Enum.Move, TurnPhase_Enum.Influence, TurnPhase_Enum.Battle, TurnPhase_Enum.AfterBattle } },
            new List<List<BattlePhase_Enum>> { new List<BattlePhase_Enum>() { BattlePhase_Enum.RangeSiege, BattlePhase_Enum.Block, BattlePhase_Enum.AssignDamage, BattlePhase_Enum.Attack, BattlePhase_Enum.EndOfBattle } },
            true,
            "Once a Round:  Put this skill token in the Source. While it is there other players cannot use their usual one die from the Source.On your next turn, gain one crystal of any basic color represented in the Source to your inventory (do not reroll that die) then take this token back and flip it."
            ) { }
    }
}
