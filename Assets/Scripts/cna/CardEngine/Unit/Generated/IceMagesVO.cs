using System.Collections.Generic;
using cna.poo;
namespace cna {
    public partial class IceMagesVO : CardUnitVO {
        public IceMagesVO(int uniqueId) : base(
            uniqueId,
            "Ice Mages",
            Image_Enum.CUE_ice_mages_x2,
            CardType_Enum.Unit_Elite,
            new List<string> { "Ice Attack or Ice Block 4", "Siege Ice Attack 4", "Gain a blue mana token and a blue crystal to your Inventory." },
            new List<List<Crystal_Enum>> { new List<Crystal_Enum>() { Crystal_Enum.NA }, new List<Crystal_Enum>() { Crystal_Enum.Blue }, new List<Crystal_Enum>() { Crystal_Enum.NA } },
            new List<List<TurnPhase_Enum>> { new List<TurnPhase_Enum>() { TurnPhase_Enum.Battle }, new List<TurnPhase_Enum>() { TurnPhase_Enum.Battle }, new List<TurnPhase_Enum>() { TurnPhase_Enum.Move, TurnPhase_Enum.Influence, TurnPhase_Enum.Battle, TurnPhase_Enum.AfterBattle } },
            new List<List<BattlePhase_Enum>> { new List<BattlePhase_Enum>() { BattlePhase_Enum.Block, BattlePhase_Enum.Attack }, new List<BattlePhase_Enum>() { BattlePhase_Enum.RangeSiege, BattlePhase_Enum.Attack }, new List<BattlePhase_Enum>() { BattlePhase_Enum.AssignDamage, BattlePhase_Enum.Provoke, BattlePhase_Enum.RangeSiege, BattlePhase_Enum.Block, BattlePhase_Enum.AssignDamage, BattlePhase_Enum.Attack, BattlePhase_Enum.EndOfBattle } },
            CardColor_Enum.NA,
            9,
            3,
            4,
            new List<Image_Enum> { Image_Enum.I_unitmage, Image_Enum.I_unitmonastery },
            new List<Image_Enum> { Image_Enum.I_resistice }
            ) { }
    }
}
