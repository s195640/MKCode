using System.Collections.Generic;
using cna.poo;
namespace cna {
    public partial class BloodRitualVO : CardActionVO {
        public BloodRitualVO(int uniqueId) : base(
            uniqueId,
            "Blood Ritual",
            Image_Enum.CA_blood_ritual,
            CardType_Enum.Advanced,
            new List<string> { "Take a Wound. Gain a red crystal to your Inventory and a mana token of any color (including non-basic).", "Take a Wound. Gain three mana tokens of any colors (including non-basic). You may pay one mana of a basic color to gain a crystal of that color to your Inventory." },
            new List<List<Crystal_Enum>> { new List<Crystal_Enum>() { Crystal_Enum.NA }, new List<Crystal_Enum>() { Crystal_Enum.Red } },
            new List<List<TurnPhase_Enum>> { new List<TurnPhase_Enum>() { TurnPhase_Enum.Move, TurnPhase_Enum.Influence, TurnPhase_Enum.Battle, TurnPhase_Enum.AfterBattle }, new List<TurnPhase_Enum>() { TurnPhase_Enum.Move, TurnPhase_Enum.Influence, TurnPhase_Enum.Battle, TurnPhase_Enum.AfterBattle } },
            new List<List<BattlePhase_Enum>> { new List<BattlePhase_Enum>() { BattlePhase_Enum.RangeSiege, BattlePhase_Enum.Block, BattlePhase_Enum.AssignDamage, BattlePhase_Enum.Attack, BattlePhase_Enum.EndOfBattle }, new List<BattlePhase_Enum>() { BattlePhase_Enum.RangeSiege, BattlePhase_Enum.Block, BattlePhase_Enum.AssignDamage, BattlePhase_Enum.Attack, BattlePhase_Enum.EndOfBattle } },
            CardColor_Enum.Red
            ) { }
    }
}
