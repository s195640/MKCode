using System.Collections.Generic;
using cna.poo;
namespace cna {
    public partial class TranquilityVO : CardActionVO {
        public TranquilityVO(int uniqueId, Image_Enum avatar) : base(
            uniqueId,
            "Tranquility",
            Image_Enum.CB_tranquility,
            CardType_Enum.Basic,
            new List<string> { "Heal 1 or draw a card", "Heal 2 or draw 2 cards" },
            new List<List<Crystal_Enum>> { new List<Crystal_Enum>() { Crystal_Enum.NA }, new List<Crystal_Enum>() { Crystal_Enum.Green } },
            new List<List<TurnPhase_Enum>> { new List<TurnPhase_Enum>() { TurnPhase_Enum.Move, TurnPhase_Enum.Influence, TurnPhase_Enum.AfterBattle }, new List<TurnPhase_Enum>() { TurnPhase_Enum.Move, TurnPhase_Enum.Influence, TurnPhase_Enum.AfterBattle } },
            new List<List<BattlePhase_Enum>> { new List<BattlePhase_Enum>() { }, new List<BattlePhase_Enum>() { } },
            CardColor_Enum.Green
            ) { Avatar = avatar; }
    }
}
