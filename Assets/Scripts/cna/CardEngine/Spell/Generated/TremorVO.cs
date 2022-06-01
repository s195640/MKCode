using System.Collections.Generic;
using cna.poo;
namespace cna {
    public partial class TremorVO : CardSpellVO {
        public TremorVO(int uniqueId) : base(
            uniqueId,
            "Tremor",
            "Earthquake",
            CardType_Enum.Spell,
            Image_Enum.CS_tremor,
            new List<string> { "Target enemy gets Armor -3, or all enemies get Armor -2. Armor cannot be reduced below 1.", "Target enemy gets Armor -3 (Armor -6 if it is fortified), or all enemies get Armor -2 (Armor -4 if they are fortified). Armor cannot be reduced below 1." },
            new List<List<Crystal_Enum>> { new List<Crystal_Enum>() { Crystal_Enum.Green }, new List<Crystal_Enum>() { Crystal_Enum.Green, Crystal_Enum.Black } },
            new List<List<TurnPhase_Enum>> { new List<TurnPhase_Enum>() { TurnPhase_Enum.Battle }, new List<TurnPhase_Enum>() { TurnPhase_Enum.Battle } },
            new List<List<BattlePhase_Enum>> { new List<BattlePhase_Enum>() { BattlePhase_Enum.RangeSiege, BattlePhase_Enum.Block, BattlePhase_Enum.AssignDamage, BattlePhase_Enum.Attack, BattlePhase_Enum.EndOfBattle }, new List<BattlePhase_Enum>() { BattlePhase_Enum.RangeSiege, BattlePhase_Enum.Block, BattlePhase_Enum.AssignDamage, BattlePhase_Enum.Attack, BattlePhase_Enum.EndOfBattle } },
            CardColor_Enum.Green
            ) { }
    }
}
