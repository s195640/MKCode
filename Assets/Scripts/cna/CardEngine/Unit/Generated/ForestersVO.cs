using System.Collections.Generic;
using cna.poo;
namespace cna {
    public partial class ForestersVO : CardUnitVO {
        public ForestersVO(int uniqueId) : base(
            uniqueId,
            "Foresters",
            Image_Enum.CUR_foresters_x2,
            CardType_Enum.Unit_Normal,
            new List<string> { "Move 2. move cost forest, hills, swamp reduced by 1", "Block 3" },
            new List<List<Crystal_Enum>> { new List<Crystal_Enum>() { Crystal_Enum.NA }, new List<Crystal_Enum>() { Crystal_Enum.NA }, new List<Crystal_Enum>() { Crystal_Enum.NA } },
            new List<List<TurnPhase_Enum>> { new List<TurnPhase_Enum>() { TurnPhase_Enum.Move }, new List<TurnPhase_Enum>() { TurnPhase_Enum.Battle }, new List<TurnPhase_Enum>() { } },
            new List<List<BattlePhase_Enum>> { new List<BattlePhase_Enum>() { }, new List<BattlePhase_Enum>() { BattlePhase_Enum.Block }, new List<BattlePhase_Enum>() { } },
            CardColor_Enum.NA,
            5,
            1,
            4,
            new List<Image_Enum> { Image_Enum.I_unitvillage },
            new List<Image_Enum> { }
            ) { }
    }
}
