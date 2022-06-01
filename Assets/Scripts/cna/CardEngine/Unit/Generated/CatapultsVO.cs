using System.Collections.Generic;
using cna.poo;
namespace cna {
    public partial class CatapultsVO : CardUnitVO {
        public CatapultsVO(int uniqueId) : base(
            uniqueId,
            "Catapults",
            Image_Enum.CUE_catapults_x3,
            CardType_Enum.Unit_Elite,
            new List<string> { "Siege Attack 3", "Siege Fire Attack 5", "Siege Ice Attack 5" },
            new List<List<Crystal_Enum>> { new List<Crystal_Enum>() { Crystal_Enum.NA }, new List<Crystal_Enum>() { Crystal_Enum.Red }, new List<Crystal_Enum>() { Crystal_Enum.Blue } },
            new List<List<TurnPhase_Enum>> { new List<TurnPhase_Enum>() { TurnPhase_Enum.Battle }, new List<TurnPhase_Enum>() { TurnPhase_Enum.Battle }, new List<TurnPhase_Enum>() { TurnPhase_Enum.Battle } },
            new List<List<BattlePhase_Enum>> { new List<BattlePhase_Enum>() { BattlePhase_Enum.RangeSiege, BattlePhase_Enum.Attack }, new List<BattlePhase_Enum>() { BattlePhase_Enum.RangeSiege, BattlePhase_Enum.Attack }, new List<BattlePhase_Enum>() { BattlePhase_Enum.RangeSiege, BattlePhase_Enum.Attack } },
            CardColor_Enum.NA,
            9,
            3,
            4,
            new List<Image_Enum> { Image_Enum.I_unitkeep, Image_Enum.I_unitcity },
            new List<Image_Enum> { }
            ) { }
    }
}
