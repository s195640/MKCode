using System.Collections.Generic;
using cna.poo;
namespace cna {
    public partial class GREEN_FreezingPowerVO : CardSkillVO {
        public GREEN_FreezingPowerVO(int uniqueId) : base(
            uniqueId,
            "Freezing Power",
            CardType_Enum.Skill,
            SkillRefresh_Enum.Turn,
            Image_Enum.SKG_freezing_power,
            Image_Enum.SKG_back,
            Image_Enum.A_MEEPLE_GREEN,
            new List<List<TurnPhase_Enum>> { new List<TurnPhase_Enum>() { TurnPhase_Enum.Battle } },
            new List<List<BattlePhase_Enum>> { new List<BattlePhase_Enum>() { BattlePhase_Enum.RangeSiege, BattlePhase_Enum.Attack } },
            false,
            "Once a turn: Siege Attack 1, or Ice Siege Attack 1"
            ) { }
    }
}
