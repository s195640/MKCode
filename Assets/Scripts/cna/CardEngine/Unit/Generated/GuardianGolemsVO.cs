using System.Collections.Generic;
using cna.poo;
namespace cna {
    public partial class GuardianGolemsVO : CardUnitVO {
        public GuardianGolemsVO(int uniqueId) : base(
            uniqueId,
            "Guardian Golems",
            Image_Enum.CUR_guardian_golems_x2,
            CardType_Enum.Unit_Normal,
            new List<string> { "Attack or Block 2", "Fire Block 4", "Ice Block 4" },
            new List<List<Crystal_Enum>> { new List<Crystal_Enum>() { Crystal_Enum.NA }, new List<Crystal_Enum>() { Crystal_Enum.Red }, new List<Crystal_Enum>() { Crystal_Enum.Blue } },
            new List<List<TurnPhase_Enum>> { new List<TurnPhase_Enum>() { TurnPhase_Enum.Battle }, new List<TurnPhase_Enum>() { TurnPhase_Enum.Battle }, new List<TurnPhase_Enum>() { TurnPhase_Enum.Battle } },
            new List<List<BattlePhase_Enum>> { new List<BattlePhase_Enum>() { BattlePhase_Enum.Block, BattlePhase_Enum.Attack }, new List<BattlePhase_Enum>() { BattlePhase_Enum.Block }, new List<BattlePhase_Enum>() { BattlePhase_Enum.Block } },
            CardColor_Enum.NA,
            7,
            2,
            3,
            new List<Image_Enum> { Image_Enum.I_unitkeep, Image_Enum.I_unitmage },
            new List<Image_Enum> { Image_Enum.I_resistphysical }
            ) { }
    }
}
