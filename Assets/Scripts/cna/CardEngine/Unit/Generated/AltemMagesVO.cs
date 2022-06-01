using System.Collections.Generic;
using cna.poo;
namespace cna {
    public partial class AltemMagesVO : CardUnitVO {
        public AltemMagesVO(int uniqueId) : base(
            uniqueId,
            "Altem Mages",
            Image_Enum.CUE_altem_mages_x2,
            CardType_Enum.Unit_Elite,
            new List<string> { "Gain 2 mana tokens of any colors", "Cold Fire attack or Cold Fire Block 5. You can pay a blue or red mana to increase this to 7, or pay both to increase this to 9.", "Pay a black mana.  All attacks you play this combat become Cold Fire Attacks." },
            new List<List<Crystal_Enum>> { new List<Crystal_Enum>() { Crystal_Enum.NA }, new List<Crystal_Enum>() { Crystal_Enum.NA }, new List<Crystal_Enum>() { Crystal_Enum.Black } },
            new List<List<TurnPhase_Enum>> { new List<TurnPhase_Enum>() { TurnPhase_Enum.Move, TurnPhase_Enum.Influence, TurnPhase_Enum.Battle, TurnPhase_Enum.AfterBattle }, new List<TurnPhase_Enum>() { TurnPhase_Enum.Battle }, new List<TurnPhase_Enum>() { TurnPhase_Enum.Battle } },
            new List<List<BattlePhase_Enum>> { new List<BattlePhase_Enum>() { BattlePhase_Enum.AssignDamage, BattlePhase_Enum.Provoke, BattlePhase_Enum.RangeSiege, BattlePhase_Enum.Block, BattlePhase_Enum.AssignDamage, BattlePhase_Enum.Attack, BattlePhase_Enum.EndOfBattle }, new List<BattlePhase_Enum>() { BattlePhase_Enum.Block, BattlePhase_Enum.Attack }, new List<BattlePhase_Enum>() { BattlePhase_Enum.RangeSiege, BattlePhase_Enum.Attack } },
            CardColor_Enum.NA,
            12,
            4,
            5,
            new List<Image_Enum> { Image_Enum.I_unitcity },
            new List<Image_Enum> { Image_Enum.I_resistice, Image_Enum.I_resistfire }
            ) { }
    }
}
