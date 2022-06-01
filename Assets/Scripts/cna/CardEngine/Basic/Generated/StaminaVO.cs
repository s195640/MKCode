using System.Collections.Generic;
using cna.poo;
namespace cna {
    public partial class StaminaVO : CardActionVO {
        public StaminaVO(int uniqueId, Image_Enum avatar) : base(
            uniqueId,
            "Stamina",
            Image_Enum.CB_stamina,
            CardType_Enum.Basic,
            new List<string> { "Move 2", "Move 4" },
            new List<List<Crystal_Enum>> { new List<Crystal_Enum>() { Crystal_Enum.NA }, new List<Crystal_Enum>() { Crystal_Enum.Blue } },
            new List<List<TurnPhase_Enum>> { new List<TurnPhase_Enum>() { TurnPhase_Enum.Move }, new List<TurnPhase_Enum>() { TurnPhase_Enum.Move } },
            new List<List<BattlePhase_Enum>> { new List<BattlePhase_Enum>() { }, new List<BattlePhase_Enum>() { } },
            CardColor_Enum.Blue
            ) { Avatar = avatar; }
    }
}
