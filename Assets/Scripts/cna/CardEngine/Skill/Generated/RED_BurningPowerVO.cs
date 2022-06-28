using System.Collections.Generic;
using cna.poo;
namespace cna {
    public partial class RED_BurningPowerVO : CardSkillVO {
        public RED_BurningPowerVO(int uniqueId) : base(
            uniqueId,
            "Burning Power",
            CardType_Enum.Skill,
            SkillRefresh_Enum.Turn,
            Image_Enum.SKR_burning_power,
            Image_Enum.SKR_back,
            Image_Enum.A_meeple_arythea,
            new List<List<TurnPhase_Enum>> { new List<TurnPhase_Enum>() { TurnPhase_Enum.Battle } },
            new List<List<BattlePhase_Enum>> { new List<BattlePhase_Enum>() { BattlePhase_Enum.RangeSiege, BattlePhase_Enum.Attack } },
            false,
            "Once a turn: Siege Attack 1, or Fire Siege Attack 1."
            ) { }
    }
}
