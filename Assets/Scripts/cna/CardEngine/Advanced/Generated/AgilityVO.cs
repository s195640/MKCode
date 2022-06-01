using System.Collections.Generic;
using cna.poo;
namespace cna {
    public partial class AgilityVO : CardActionVO {
        public AgilityVO(int uniqueId) : base(
            uniqueId,
            "Agility",
            Image_Enum.CA_agility,
            CardType_Enum.Advanced,
            new List<string> { "Move 2. During combat this turn, you may spend Move points to get Attack 1 for each.", "Move 4. During combat this turn, you may spend any amount of Move points: 1 to get Attack 1 and/or 2 to get Ranged Attack 1." },
            new List<List<Crystal_Enum>> { new List<Crystal_Enum>() { Crystal_Enum.NA }, new List<Crystal_Enum>() { Crystal_Enum.White } },
            new List<List<TurnPhase_Enum>> { new List<TurnPhase_Enum>() { TurnPhase_Enum.Move, TurnPhase_Enum.Battle }, new List<TurnPhase_Enum>() { TurnPhase_Enum.Move, TurnPhase_Enum.Battle } },
            new List<List<BattlePhase_Enum>> { new List<BattlePhase_Enum>() { BattlePhase_Enum.RangeSiege, BattlePhase_Enum.Block, BattlePhase_Enum.AssignDamage, BattlePhase_Enum.Attack, BattlePhase_Enum.EndOfBattle }, new List<BattlePhase_Enum>() { BattlePhase_Enum.RangeSiege, BattlePhase_Enum.Block, BattlePhase_Enum.AssignDamage, BattlePhase_Enum.Attack, BattlePhase_Enum.EndOfBattle } },
            CardColor_Enum.White
            ) { }
    }
}
