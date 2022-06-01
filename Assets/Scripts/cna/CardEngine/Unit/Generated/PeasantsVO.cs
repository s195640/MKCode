using System.Collections.Generic;
using cna.poo;
namespace cna {
    public partial class PeasantsVO : CardUnitVO {
        public PeasantsVO(int uniqueId) : base(
            uniqueId,
            "Peasants",
            Image_Enum.CUR_peasants_x3,
            CardType_Enum.Unit_Normal,
            new List<string> { "Attack or Block 2", "Influence 2", "Move 2" },
            new List<List<Crystal_Enum>> { new List<Crystal_Enum>() { Crystal_Enum.NA }, new List<Crystal_Enum>() { Crystal_Enum.NA }, new List<Crystal_Enum>() { Crystal_Enum.NA } },
            new List<List<TurnPhase_Enum>> { new List<TurnPhase_Enum>() { TurnPhase_Enum.Battle }, new List<TurnPhase_Enum>() { TurnPhase_Enum.Influence }, new List<TurnPhase_Enum>() { TurnPhase_Enum.Move } },
            new List<List<BattlePhase_Enum>> { new List<BattlePhase_Enum>() { BattlePhase_Enum.Block, BattlePhase_Enum.Attack }, new List<BattlePhase_Enum>() { }, new List<BattlePhase_Enum>() { } },
            CardColor_Enum.NA,
            4,
            1,
            3,
            new List<Image_Enum> { Image_Enum.I_unitvillage },
            new List<Image_Enum> { }
            ) { }
    }
}
