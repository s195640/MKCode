using System.Collections.Generic;
using cna.poo;
namespace cna {
    public partial class EnergyFlowVO : CardSpellVO {
        public EnergyFlowVO(int uniqueId) : base(
            uniqueId,
            "Energy Flow",
            "Energy Steal",
            CardType_Enum.Spell,
            Image_Enum.CS_energy_flow,
            new List<string> { "Ready a Unit. If you do, you may spend one Unit of level II or less in each other player's Unit area.", "Ready a Unit. If you do, that Unit also gets Healed, and you may spend one unit of level III or less in each other player's Unit area." },
            new List<List<Crystal_Enum>> { new List<Crystal_Enum>() { Crystal_Enum.Green }, new List<Crystal_Enum>() { Crystal_Enum.Green, Crystal_Enum.Black } },
            new List<List<TurnPhase_Enum>> { new List<TurnPhase_Enum>() { TurnPhase_Enum.Move, TurnPhase_Enum.Influence, TurnPhase_Enum.Battle, TurnPhase_Enum.AfterBattle }, new List<TurnPhase_Enum>() { TurnPhase_Enum.Move, TurnPhase_Enum.Influence, TurnPhase_Enum.Battle, TurnPhase_Enum.AfterBattle } },
            new List<List<BattlePhase_Enum>> { new List<BattlePhase_Enum>() { BattlePhase_Enum.RangeSiege, BattlePhase_Enum.Block, BattlePhase_Enum.AssignDamage, BattlePhase_Enum.Attack, BattlePhase_Enum.EndOfBattle }, new List<BattlePhase_Enum>() { BattlePhase_Enum.RangeSiege, BattlePhase_Enum.Block, BattlePhase_Enum.AssignDamage, BattlePhase_Enum.Attack, BattlePhase_Enum.EndOfBattle } },
            CardColor_Enum.Green
            ) { }
    }
}
