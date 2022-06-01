using System.Collections.Generic;
using cna.poo;
namespace cna {
    public partial class AmotepGunnersVO : CardUnitVO {
        public AmotepGunnersVO(int uniqueId) : base(
            uniqueId,
            "Amotep Gunners",
            Image_Enum.CUE_amotep_gunners_x2,
            CardType_Enum.Unit_Elite,
            new List<string> { "Attack or Block 5", "Ranged Fire Attack 6" },
            new List<List<Crystal_Enum>> { new List<Crystal_Enum>() { Crystal_Enum.NA }, new List<Crystal_Enum>() { Crystal_Enum.Red }, new List<Crystal_Enum>() { Crystal_Enum.NA } },
            new List<List<TurnPhase_Enum>> { new List<TurnPhase_Enum>() { TurnPhase_Enum.Battle }, new List<TurnPhase_Enum>() { TurnPhase_Enum.Battle }, new List<TurnPhase_Enum>() { } },
            new List<List<BattlePhase_Enum>> { new List<BattlePhase_Enum>() { BattlePhase_Enum.Block, BattlePhase_Enum.Attack }, new List<BattlePhase_Enum>() { BattlePhase_Enum.RangeSiege, BattlePhase_Enum.Attack }, new List<BattlePhase_Enum>() { } },
            CardColor_Enum.NA,
            8,
            3,
            6,
            new List<Image_Enum> { Image_Enum.I_unitkeep, Image_Enum.I_unitcity },
            new List<Image_Enum> { }
            ) { }
    }
}
