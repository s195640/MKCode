using System.Collections.Generic;
using cna.poo;
namespace cna {
    public partial class InNeedVO : CardActionVO {
        public InNeedVO(int uniqueId) : base(
            uniqueId,
            "In Need",
            Image_Enum.CA_in_need,
            CardType_Enum.Advanced,
            new List<string> { "Influence 3. Get an additional Influence 1 for each Wound card in your hand and/or on a Unit you control.", "Influence 5. Get an additional Influence 2 for each Wound card in your hand and/or on a Unit you control." },
            new List<List<Crystal_Enum>> { new List<Crystal_Enum>() { Crystal_Enum.NA }, new List<Crystal_Enum>() { Crystal_Enum.Green } },
            new List<List<TurnPhase_Enum>> { new List<TurnPhase_Enum>() { TurnPhase_Enum.Influence }, new List<TurnPhase_Enum>() { TurnPhase_Enum.Influence } },
            new List<List<BattlePhase_Enum>> { new List<BattlePhase_Enum>() { }, new List<BattlePhase_Enum>() { } },
            CardColor_Enum.Green
            ) { }
    }
}
