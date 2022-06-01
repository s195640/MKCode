using System.Collections.Generic;
using cna.poo;
namespace cna {
    public partial class WillFocusVO : CardActionVO {
        public WillFocusVO(int uniqueId, Image_Enum avatar) : base(
            uniqueId,
            "Will Focus",
            Image_Enum.CB_will_focus,
            CardType_Enum.Basic,
            new List<string> { "Gain a blue, white or red mana token, or gain a green crystal to your Inventory", "When you play this, play another Action card with it. Get the stronger effect of that card for free. If that effect gives you Move, Influence, Block or any type of Attack, get that amount plus 3." },
            new List<List<Crystal_Enum>> { new List<Crystal_Enum>() { Crystal_Enum.NA }, new List<Crystal_Enum>() { Crystal_Enum.Green } },
            new List<List<TurnPhase_Enum>> { new List<TurnPhase_Enum>() { TurnPhase_Enum.Move, TurnPhase_Enum.Influence, TurnPhase_Enum.Battle, TurnPhase_Enum.AfterBattle }, new List<TurnPhase_Enum>() { TurnPhase_Enum.Move, TurnPhase_Enum.Influence, TurnPhase_Enum.Battle, TurnPhase_Enum.AfterBattle } },
            new List<List<BattlePhase_Enum>> { new List<BattlePhase_Enum>() { BattlePhase_Enum.RangeSiege, BattlePhase_Enum.Block, BattlePhase_Enum.AssignDamage, BattlePhase_Enum.Attack, BattlePhase_Enum.EndOfBattle }, new List<BattlePhase_Enum>() { BattlePhase_Enum.RangeSiege, BattlePhase_Enum.Block, BattlePhase_Enum.AssignDamage, BattlePhase_Enum.Attack, BattlePhase_Enum.EndOfBattle } },
            CardColor_Enum.Green
            ) { Avatar = avatar; }
    }
}
