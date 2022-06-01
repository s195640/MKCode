using System.Collections.Generic;
using cna.poo;
namespace cna {
    public partial class CrystallizeVO : CardActionVO {
        public CrystallizeVO(int uniqueId, Image_Enum avatar) : base(
            uniqueId,
            "Crystallize",
            Image_Enum.CB_crystallize,
            CardType_Enum.Basic,
            new List<string> { "When you play this, also pay one mana of a basic color. Gain a crystal of that color to your Inventory.", "Gain a crystal of any color to your Inventory." },
            new List<List<Crystal_Enum>> { new List<Crystal_Enum>() { Crystal_Enum.NA }, new List<Crystal_Enum>() { Crystal_Enum.Blue } },
            new List<List<TurnPhase_Enum>> { new List<TurnPhase_Enum>() { TurnPhase_Enum.Move, TurnPhase_Enum.Influence, TurnPhase_Enum.Battle, TurnPhase_Enum.AfterBattle }, new List<TurnPhase_Enum>() { TurnPhase_Enum.Move, TurnPhase_Enum.Influence, TurnPhase_Enum.Battle, TurnPhase_Enum.AfterBattle } },
            new List<List<BattlePhase_Enum>> { new List<BattlePhase_Enum>() { BattlePhase_Enum.RangeSiege, BattlePhase_Enum.Block, BattlePhase_Enum.AssignDamage, BattlePhase_Enum.Attack, BattlePhase_Enum.EndOfBattle }, new List<BattlePhase_Enum>() { BattlePhase_Enum.RangeSiege, BattlePhase_Enum.Block, BattlePhase_Enum.AssignDamage, BattlePhase_Enum.Attack, BattlePhase_Enum.EndOfBattle } },
            CardColor_Enum.Blue
            ) { Avatar = avatar; }
    }
}
