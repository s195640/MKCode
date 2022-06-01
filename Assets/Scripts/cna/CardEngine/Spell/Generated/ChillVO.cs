using System.Collections.Generic;
using cna.poo;
namespace cna {
    public partial class ChillVO : CardSpellVO {
        public ChillVO(int uniqueId) : base(
            uniqueId,
            "Chill",
            "Lethal Chill",
            CardType_Enum.Spell,
            Image_Enum.CS_chill,
            new List<string> { "Target enemy does not attack this combat. If it has Fire Resistance, it loses it for the rest of the turn.", "Target enemy does not attack this combat and gets Armor -4 (to a minimum of 1) for the rest of the turn." },
            new List<List<Crystal_Enum>> { new List<Crystal_Enum>() { Crystal_Enum.Blue }, new List<Crystal_Enum>() { Crystal_Enum.Blue, Crystal_Enum.Black } },
            new List<List<TurnPhase_Enum>> { new List<TurnPhase_Enum>() { TurnPhase_Enum.Battle }, new List<TurnPhase_Enum>() { TurnPhase_Enum.Battle } },
            new List<List<BattlePhase_Enum>> { new List<BattlePhase_Enum>() { BattlePhase_Enum.RangeSiege, BattlePhase_Enum.Block, BattlePhase_Enum.AssignDamage, BattlePhase_Enum.Attack, BattlePhase_Enum.EndOfBattle }, new List<BattlePhase_Enum>() { BattlePhase_Enum.RangeSiege, BattlePhase_Enum.Block, BattlePhase_Enum.AssignDamage, BattlePhase_Enum.Attack, BattlePhase_Enum.EndOfBattle } },
            CardColor_Enum.Blue
            ) { }
    }
}
