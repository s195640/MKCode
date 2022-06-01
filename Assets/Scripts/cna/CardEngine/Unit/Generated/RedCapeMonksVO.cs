using System.Collections.Generic;
using cna.poo;
namespace cna {
    public partial class RedCapeMonksVO : CardUnitVO {
        public RedCapeMonksVO(int uniqueId) : base(
            uniqueId,
            "Red Cape Monks",
            Image_Enum.CUR_red_cape_monks,
            CardType_Enum.Unit_Normal,
            new List<string> { "Attack or Block 3", "Fire Attack or Fire Block 4" },
            new List<List<Crystal_Enum>> { new List<Crystal_Enum>() { Crystal_Enum.NA }, new List<Crystal_Enum>() { Crystal_Enum.Red }, new List<Crystal_Enum>() { Crystal_Enum.NA } },
            new List<List<TurnPhase_Enum>> { new List<TurnPhase_Enum>() { TurnPhase_Enum.Battle }, new List<TurnPhase_Enum>() { TurnPhase_Enum.Battle }, new List<TurnPhase_Enum>() { } },
            new List<List<BattlePhase_Enum>> { new List<BattlePhase_Enum>() { BattlePhase_Enum.Block, BattlePhase_Enum.Attack }, new List<BattlePhase_Enum>() { BattlePhase_Enum.Block, BattlePhase_Enum.Attack }, new List<BattlePhase_Enum>() { } },
            CardColor_Enum.NA,
            7,
            2,
            4,
            new List<Image_Enum> { Image_Enum.I_unitmonastery },
            new List<Image_Enum> { }
            ) { }
    }
}
