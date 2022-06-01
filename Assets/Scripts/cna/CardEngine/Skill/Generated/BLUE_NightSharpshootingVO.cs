using System.Collections.Generic;
using cna.poo;
namespace cna {
    public partial class BLUE_NightSharpshootingVO : CardSkillVO {
        public BLUE_NightSharpshootingVO(int uniqueId) : base(
            uniqueId,
            "Night Sharpshooting",
            CardType_Enum.Skill,
            SkillRefresh_Enum.Turn,
            Image_Enum.SKB_night_shooting,
            Image_Enum.SKB_back,
            Image_Enum.A_MEEPLE_BLUE,
            new List<List<TurnPhase_Enum>> { new List<TurnPhase_Enum>() { TurnPhase_Enum.Battle } },
            new List<List<BattlePhase_Enum>> { new List<BattlePhase_Enum>() { BattlePhase_Enum.RangeSiege, BattlePhase_Enum.Attack } },
            false,
            "Once a turn: Ranged attack 1 (durng the Day), or Ranged attack 2 (during the Night)"
            ) { }
    }
}
