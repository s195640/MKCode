using System.Collections.Generic;
using cna.poo;
namespace cna {
    public partial class WHITE_DaySharpshootingVO : CardSkillVO {
        public WHITE_DaySharpshootingVO(int uniqueId) : base(
            uniqueId,
            "Day Sharpshooting",
            CardType_Enum.Skill,
            SkillRefresh_Enum.Turn,
            Image_Enum.SKW_day_sharpshooting,
            Image_Enum.SKW_back,
            Image_Enum.A_MEEPLE_WHITE,
            new List<List<TurnPhase_Enum>> { new List<TurnPhase_Enum>() { TurnPhase_Enum.Battle } },
            new List<List<BattlePhase_Enum>> { new List<BattlePhase_Enum>() { BattlePhase_Enum.RangeSiege, BattlePhase_Enum.Attack } },
            false,
            "Once a turn: Ranged Attack 2 (during the Day), or Ranged attack 1 (at Night)"
            ) { }
    }
}
