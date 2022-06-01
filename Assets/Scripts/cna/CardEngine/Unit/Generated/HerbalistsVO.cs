using System.Collections.Generic;
using cna.poo;
namespace cna {
    public partial class HerbalistsVO : CardUnitVO {
        public HerbalistsVO(int uniqueId) : base(
            uniqueId,
            "Herbalists",
            Image_Enum.CUR_herbalists_x2,
            CardType_Enum.Unit_Normal,
            new List<string> { "Heal 2", "Ready level 1 or 2 Unit", "Gain a green mana token" },
            new List<List<Crystal_Enum>> { new List<Crystal_Enum>() { Crystal_Enum.Green }, new List<Crystal_Enum>() { Crystal_Enum.NA }, new List<Crystal_Enum>() { Crystal_Enum.NA } },
            new List<List<TurnPhase_Enum>> { new List<TurnPhase_Enum>() { TurnPhase_Enum.Move, TurnPhase_Enum.Influence, TurnPhase_Enum.AfterBattle }, new List<TurnPhase_Enum>() { TurnPhase_Enum.Move, TurnPhase_Enum.Influence, TurnPhase_Enum.AfterBattle }, new List<TurnPhase_Enum>() { TurnPhase_Enum.Move, TurnPhase_Enum.Influence, TurnPhase_Enum.Battle, TurnPhase_Enum.AfterBattle } },
            new List<List<BattlePhase_Enum>> { new List<BattlePhase_Enum>() { }, new List<BattlePhase_Enum>() { }, new List<BattlePhase_Enum>() { BattlePhase_Enum.AssignDamage, BattlePhase_Enum.Provoke, BattlePhase_Enum.RangeSiege, BattlePhase_Enum.Block, BattlePhase_Enum.AssignDamage, BattlePhase_Enum.Attack, BattlePhase_Enum.EndOfBattle } },
            CardColor_Enum.NA,
            3,
            1,
            2,
            new List<Image_Enum> { Image_Enum.I_unitvillage, Image_Enum.I_unitmonastery },
            new List<Image_Enum> { }
            ) { }
    }
}
