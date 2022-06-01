using System.Collections.Generic;
using cna.poo;
namespace cna {
    public partial class ExposeVO : CardSpellVO {
        public ExposeVO(int uniqueId) : base(
            uniqueId,
            "Expose",
            "Mass Expose",
            CardType_Enum.Spell,
            Image_Enum.CS_expose,
            new List<string> { "Target enemy loses all fortifications and resistances this combat. Ranged Attack 2.", "Enemies lose all fortifications this combat, or enemies lose all resistances this combat. Ranged Attack 3." },
            new List<List<Crystal_Enum>> { new List<Crystal_Enum>() { Crystal_Enum.White }, new List<Crystal_Enum>() { Crystal_Enum.White, Crystal_Enum.Black } },
            new List<List<TurnPhase_Enum>> { new List<TurnPhase_Enum>() { TurnPhase_Enum.Battle }, new List<TurnPhase_Enum>() { TurnPhase_Enum.Battle } },
            new List<List<BattlePhase_Enum>> { new List<BattlePhase_Enum>() { BattlePhase_Enum.RangeSiege, BattlePhase_Enum.Block, BattlePhase_Enum.AssignDamage, BattlePhase_Enum.Attack, BattlePhase_Enum.EndOfBattle }, new List<BattlePhase_Enum>() { BattlePhase_Enum.RangeSiege, BattlePhase_Enum.Block, BattlePhase_Enum.AssignDamage, BattlePhase_Enum.Attack, BattlePhase_Enum.EndOfBattle } },
            CardColor_Enum.White
            ) { }
    }
}
