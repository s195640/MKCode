using System.Collections.Generic;
using cna.poo;
namespace cna {
    public partial class FireGolemsVO : CardUnitVO {
        public FireGolemsVO(int uniqueId) : base(
            uniqueId,
            "Fire Golems",
            Image_Enum.CUE_fire_golems_x2,
            CardType_Enum.Unit_Elite,
            new List<string> { "Attack or Block 3", "Ranged Fire Attack 4" },
            new List<List<Crystal_Enum>> { new List<Crystal_Enum>() { Crystal_Enum.NA }, new List<Crystal_Enum>() { Crystal_Enum.Red }, new List<Crystal_Enum>() { Crystal_Enum.NA } },
            new List<List<TurnPhase_Enum>> { new List<TurnPhase_Enum>() { TurnPhase_Enum.Battle }, new List<TurnPhase_Enum>() { TurnPhase_Enum.Battle }, new List<TurnPhase_Enum>() { } },
            new List<List<BattlePhase_Enum>> { new List<BattlePhase_Enum>() { BattlePhase_Enum.Block, BattlePhase_Enum.Attack }, new List<BattlePhase_Enum>() { BattlePhase_Enum.RangeSiege, BattlePhase_Enum.Attack }, new List<BattlePhase_Enum>() { } },
            CardColor_Enum.NA,
            8,
            3,
            4,
            new List<Image_Enum> { Image_Enum.I_unitkeep, Image_Enum.I_unitmage },
            new List<Image_Enum> { Image_Enum.I_resistphysical, Image_Enum.I_resistfire }
            ) { }
    }
}
