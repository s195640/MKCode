using System.Collections.Generic;
using cna.poo;
namespace cna {
    public partial class AltemGuardiansVO : CardUnitVO {
        public AltemGuardiansVO(int uniqueId) : base(
            uniqueId,
            "Altem Guardians",
            Image_Enum.CUE_altem_guardians_x3,
            CardType_Enum.Unit_Elite,
            new List<string> { "Attack 5", "Block 8. Counts twice against an attack with Swiftness.", "Pay a green mana: All units you control gain all resistances this turn." },
            new List<List<Crystal_Enum>> { new List<Crystal_Enum>() { Crystal_Enum.NA }, new List<Crystal_Enum>() { Crystal_Enum.NA }, new List<Crystal_Enum>() { Crystal_Enum.Green } },
            new List<List<TurnPhase_Enum>> { new List<TurnPhase_Enum>() { TurnPhase_Enum.Battle }, new List<TurnPhase_Enum>() { TurnPhase_Enum.Battle }, new List<TurnPhase_Enum>() { TurnPhase_Enum.Battle } },
            new List<List<BattlePhase_Enum>> { new List<BattlePhase_Enum>() { BattlePhase_Enum.Attack }, new List<BattlePhase_Enum>() { BattlePhase_Enum.Block }, new List<BattlePhase_Enum>() { BattlePhase_Enum.AssignDamage, BattlePhase_Enum.Provoke, BattlePhase_Enum.RangeSiege, BattlePhase_Enum.Block, BattlePhase_Enum.AssignDamage, BattlePhase_Enum.Attack, BattlePhase_Enum.EndOfBattle } },
            CardColor_Enum.NA,
            11,
            4,
            7,
            new List<Image_Enum> { Image_Enum.I_unitcity },
            new List<Image_Enum> { }
            ) { }
    }
}
