using System.Collections.Generic;
using cna.poo;
namespace cna {
    public partial class SwiftBoltVO : CardActionVO {
        public SwiftBoltVO(int uniqueId) : base(
            uniqueId,
            "Swift Bolt",
            Image_Enum.CA_swift_bolt,
            CardType_Enum.Advanced,
            new List<string> { "Gain a white crystal to your Inventory.", "Ranged Attack 4" },
            new List<List<Crystal_Enum>> { new List<Crystal_Enum>() { Crystal_Enum.NA }, new List<Crystal_Enum>() { Crystal_Enum.White } },
            new List<List<TurnPhase_Enum>> { new List<TurnPhase_Enum>() { TurnPhase_Enum.Move, TurnPhase_Enum.Influence, TurnPhase_Enum.Battle, TurnPhase_Enum.AfterBattle }, new List<TurnPhase_Enum>() { TurnPhase_Enum.Battle } },
            new List<List<BattlePhase_Enum>> { new List<BattlePhase_Enum>() { BattlePhase_Enum.RangeSiege, BattlePhase_Enum.Block, BattlePhase_Enum.AssignDamage, BattlePhase_Enum.Attack, BattlePhase_Enum.EndOfBattle }, new List<BattlePhase_Enum>() { BattlePhase_Enum.RangeSiege, BattlePhase_Enum.Attack } },
            CardColor_Enum.White
            ) { }
    }
}
