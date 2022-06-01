using System.Collections.Generic;
using cna.poo;
namespace cna {
    public partial class UtemGuardsmenVO : CardUnitVO {
        public UtemGuardsmenVO(int uniqueId) : base(
            uniqueId,
            "Utem Guardsmen",
            Image_Enum.CUR_utem_guardsmen_x2,
            CardType_Enum.Unit_Normal,
            new List<string> { "Attack 2", "Block 4. counts twice against swiftness attack" },
            new List<List<Crystal_Enum>> { new List<Crystal_Enum>() { Crystal_Enum.NA }, new List<Crystal_Enum>() { Crystal_Enum.NA }, new List<Crystal_Enum>() { Crystal_Enum.NA } },
            new List<List<TurnPhase_Enum>> { new List<TurnPhase_Enum>() { TurnPhase_Enum.Battle }, new List<TurnPhase_Enum>() { TurnPhase_Enum.Battle }, new List<TurnPhase_Enum>() { } },
            new List<List<BattlePhase_Enum>> { new List<BattlePhase_Enum>() { BattlePhase_Enum.Attack }, new List<BattlePhase_Enum>() { BattlePhase_Enum.Block }, new List<BattlePhase_Enum>() { } },
            CardColor_Enum.NA,
            5,
            2,
            5,
            new List<Image_Enum> { Image_Enum.I_unitvillage, Image_Enum.I_unitkeep },
            new List<Image_Enum> { }
            ) { }
    }
}
