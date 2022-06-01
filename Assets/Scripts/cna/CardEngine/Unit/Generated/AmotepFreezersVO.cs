using System.Collections.Generic;
using cna.poo;
namespace cna {
    public partial class AmotepFreezersVO : CardUnitVO {
        public AmotepFreezersVO(int uniqueId) : base(
            uniqueId,
            "Amotep Freezers",
            Image_Enum.CUE_amotep_freezers_x2,
            CardType_Enum.Unit_Elite,
            new List<string> { "Attack or Block 5", "Target enemy does not attack this combat and it gets Armor -3 (to a minimum of 1)." },
            new List<List<Crystal_Enum>> { new List<Crystal_Enum>() { Crystal_Enum.NA }, new List<Crystal_Enum>() { Crystal_Enum.Blue }, new List<Crystal_Enum>() { Crystal_Enum.NA } },
            new List<List<TurnPhase_Enum>> { new List<TurnPhase_Enum>() { TurnPhase_Enum.Battle }, new List<TurnPhase_Enum>() { TurnPhase_Enum.Battle }, new List<TurnPhase_Enum>() { } },
            new List<List<BattlePhase_Enum>> { new List<BattlePhase_Enum>() { BattlePhase_Enum.Block, BattlePhase_Enum.Attack }, new List<BattlePhase_Enum>() { BattlePhase_Enum.AssignDamage }, new List<BattlePhase_Enum>() { } },
            CardColor_Enum.NA,
            8,
            3,
            6,
            new List<Image_Enum> { Image_Enum.I_unitkeep, Image_Enum.I_unitcity },
            new List<Image_Enum> { }
            ) { }
    }
}
