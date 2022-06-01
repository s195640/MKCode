using System.Collections.Generic;
using cna.poo;
namespace cna {
    public partial class DemolishVO : CardSpellVO {
        public DemolishVO(int uniqueId) : base(
            uniqueId,
            "Demolish",
            "Disintegrate",
            CardType_Enum.Spell,
            Image_Enum.CS_demolish,
            new List<string> { "Ignore site fortifications this turn. Enemies get Armor -1 (to a minimum of 1).", "Play this only in the Attack phase of combat. Destroy target enemy. Other enemies get Armor -1 (to a minimum of 1)." },
            new List<List<Crystal_Enum>> { new List<Crystal_Enum>() { Crystal_Enum.Red }, new List<Crystal_Enum>() { Crystal_Enum.Red, Crystal_Enum.Black } },
            new List<List<TurnPhase_Enum>> { new List<TurnPhase_Enum>() { TurnPhase_Enum.Battle }, new List<TurnPhase_Enum>() { TurnPhase_Enum.Battle } },
            new List<List<BattlePhase_Enum>> { new List<BattlePhase_Enum>() { BattlePhase_Enum.RangeSiege, BattlePhase_Enum.Block, BattlePhase_Enum.AssignDamage, BattlePhase_Enum.Attack, BattlePhase_Enum.EndOfBattle }, new List<BattlePhase_Enum>() { BattlePhase_Enum.Attack } },
            CardColor_Enum.Red
            ) { }
    }
}
