using System.Collections.Generic;
using cna.poo;
namespace cna {
    public partial class UtemCrossbowmenVO : CardUnitVO {
        public UtemCrossbowmenVO(int uniqueId) : base(
            uniqueId,
            "Utem Crossbowmen",
            Image_Enum.CUR_utem_crossbowmen_x2,
            CardType_Enum.Unit_Normal,
            new List<string> { "Attack or Block 3", "Ranged Attack 2" },
            new List<List<Crystal_Enum>> { new List<Crystal_Enum>() { Crystal_Enum.NA }, new List<Crystal_Enum>() { Crystal_Enum.NA }, new List<Crystal_Enum>() { Crystal_Enum.NA } },
            new List<List<TurnPhase_Enum>> { new List<TurnPhase_Enum>() { TurnPhase_Enum.Battle }, new List<TurnPhase_Enum>() { TurnPhase_Enum.Battle }, new List<TurnPhase_Enum>() { } },
            new List<List<BattlePhase_Enum>> { new List<BattlePhase_Enum>() { BattlePhase_Enum.Block, BattlePhase_Enum.Attack }, new List<BattlePhase_Enum>() { BattlePhase_Enum.RangeSiege, BattlePhase_Enum.Attack }, new List<BattlePhase_Enum>() { } },
            CardColor_Enum.NA,
            6,
            2,
            4,
            new List<Image_Enum> { Image_Enum.I_unitvillage, Image_Enum.I_unitkeep },
            new List<Image_Enum> { }
            ) { }
    }
}
