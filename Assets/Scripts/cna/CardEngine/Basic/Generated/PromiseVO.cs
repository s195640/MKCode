using System.Collections.Generic;
using cna.poo;
namespace cna {
    public partial class PromiseVO : CardActionVO {
        public PromiseVO(int uniqueId, Image_Enum avatar) : base(
            uniqueId,
            "Promise",
            Image_Enum.CB_promise,
            CardType_Enum.Basic,
            new List<string> { "Influence 2", "Influence 4" },
            new List<List<Crystal_Enum>> { new List<Crystal_Enum>() { Crystal_Enum.NA }, new List<Crystal_Enum>() { Crystal_Enum.White } },
            new List<List<TurnPhase_Enum>> { new List<TurnPhase_Enum>() { TurnPhase_Enum.Influence }, new List<TurnPhase_Enum>() { TurnPhase_Enum.Influence } },
            new List<List<BattlePhase_Enum>> { new List<BattlePhase_Enum>() { }, new List<BattlePhase_Enum>() { } },
            CardColor_Enum.White
            ) { Avatar = avatar; }
    }
}
