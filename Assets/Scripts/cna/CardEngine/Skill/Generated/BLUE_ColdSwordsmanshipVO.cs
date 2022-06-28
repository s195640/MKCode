using System.Collections.Generic;
using cna.poo;
namespace cna {
    public partial class BLUE_ColdSwordsmanshipVO : CardSkillVO {
        public BLUE_ColdSwordsmanshipVO(int uniqueId) : base(
            uniqueId,
            "Cold Swordsmanship",
            CardType_Enum.Skill,
            SkillRefresh_Enum.Turn,
            Image_Enum.SKB_cold_swordsmanship,
            Image_Enum.SKB_back,
            Image_Enum.A_meeple_tovak,
            new List<List<TurnPhase_Enum>> { new List<TurnPhase_Enum>() { TurnPhase_Enum.Battle } },
            new List<List<BattlePhase_Enum>> { new List<BattlePhase_Enum>() { BattlePhase_Enum.Attack } },
            false,
            "Once a turn: Attack 2, or Ice attack 2"
            ) { }
    }
}
