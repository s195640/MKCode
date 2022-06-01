using System.Collections.Generic;
using cna.poo;
namespace cna {
    public partial class IntotheHeatVO : CardActionVO {
        public IntotheHeatVO(int uniqueId) : base(
            uniqueId,
            "Into the Heat",
            Image_Enum.CA_into_the_heat,
            CardType_Enum.Advanced,
            new List<string> { "Play this card at the start of the combat. All of your Units get their Attack and Block values increased by 2 this combat. You cannot assign damage to Units this turn.", "Play this card at the start of the combat. All of your Units get their Attack and Block values increased by 3 this combat. You cannot assign damage to Units this turn." },
            new List<List<Crystal_Enum>> { new List<Crystal_Enum>() { Crystal_Enum.NA }, new List<Crystal_Enum>() { Crystal_Enum.Red } },
            new List<List<TurnPhase_Enum>> { new List<TurnPhase_Enum>() { TurnPhase_Enum.Battle }, new List<TurnPhase_Enum>() { TurnPhase_Enum.Battle } },
            new List<List<BattlePhase_Enum>> { new List<BattlePhase_Enum>() { BattlePhase_Enum.RangeSiege }, new List<BattlePhase_Enum>() { BattlePhase_Enum.RangeSiege } },
            CardColor_Enum.Red
            ) { }
    }
}
