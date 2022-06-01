using System.Collections.Generic;
using cna.poo;
namespace cna {
    public partial class IceBoltVO : CardActionVO {
        public IceBoltVO(int uniqueId) : base(
            uniqueId,
            "Ice Bolt",
            Image_Enum.CA_ice_bolt,
            CardType_Enum.Advanced,
            new List<string> { "Gain a blue crystal to your Inventory.", "Ranged Ice Attack 3" },
            new List<List<Crystal_Enum>> { new List<Crystal_Enum>() { Crystal_Enum.NA }, new List<Crystal_Enum>() { Crystal_Enum.Blue } },
            new List<List<TurnPhase_Enum>> { new List<TurnPhase_Enum>() { TurnPhase_Enum.Move, TurnPhase_Enum.Influence, TurnPhase_Enum.Battle, TurnPhase_Enum.AfterBattle }, new List<TurnPhase_Enum>() { TurnPhase_Enum.Battle } },
            new List<List<BattlePhase_Enum>> { new List<BattlePhase_Enum>() { BattlePhase_Enum.RangeSiege, BattlePhase_Enum.Block, BattlePhase_Enum.AssignDamage, BattlePhase_Enum.Attack, BattlePhase_Enum.EndOfBattle }, new List<BattlePhase_Enum>() { BattlePhase_Enum.RangeSiege, BattlePhase_Enum.Attack } },
            CardColor_Enum.Blue
            ) { }
    }
}
