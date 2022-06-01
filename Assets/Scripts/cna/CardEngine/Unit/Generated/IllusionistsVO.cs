using System.Collections.Generic;
using cna.poo;
namespace cna {
    public partial class IllusionistsVO : CardUnitVO {
        public IllusionistsVO(int uniqueId) : base(
            uniqueId,
            "Illusionists",
            Image_Enum.CUR_illusionists_x2,
            CardType_Enum.Unit_Normal,
            new List<string> { "Influence 4", "Target unfortified enemy does not attack", "Gain a white crystal" },
            new List<List<Crystal_Enum>> { new List<Crystal_Enum>() { Crystal_Enum.NA }, new List<Crystal_Enum>() { Crystal_Enum.White }, new List<Crystal_Enum>() { Crystal_Enum.NA } },
            new List<List<TurnPhase_Enum>> { new List<TurnPhase_Enum>() { TurnPhase_Enum.Influence }, new List<TurnPhase_Enum>() { TurnPhase_Enum.Battle }, new List<TurnPhase_Enum>() { TurnPhase_Enum.Move, TurnPhase_Enum.Influence, TurnPhase_Enum.Battle, TurnPhase_Enum.AfterBattle } },
            new List<List<BattlePhase_Enum>> { new List<BattlePhase_Enum>() { }, new List<BattlePhase_Enum>() { BattlePhase_Enum.AssignDamage }, new List<BattlePhase_Enum>() { BattlePhase_Enum.AssignDamage, BattlePhase_Enum.Provoke, BattlePhase_Enum.RangeSiege, BattlePhase_Enum.Block, BattlePhase_Enum.AssignDamage, BattlePhase_Enum.Attack, BattlePhase_Enum.EndOfBattle } },
            CardColor_Enum.NA,
            7,
            2,
            2,
            new List<Image_Enum> { Image_Enum.I_unitmage, Image_Enum.I_unitmonastery },
            new List<Image_Enum> { Image_Enum.I_resistphysical }
            ) { }
    }
}
