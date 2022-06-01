using System.Collections.Generic;
using cna.poo;
namespace cna {
    public partial class HornofWrathVO : CardArtifactVO {
        public HornofWrathVO(int uniqueId) : base(
            uniqueId,
            "Horn of Wrath",
            Image_Enum.CT_horn_of_wrath,
            new List<string> { "Siege Attack 6. Take a wound.", "Siege Attack 6. You may add up to +6 to this Siege Attack. For each +1 you added take a wound." },
            new List<List<TurnPhase_Enum>> { new List<TurnPhase_Enum>() { TurnPhase_Enum.Battle }, new List<TurnPhase_Enum>() { TurnPhase_Enum.Battle } },
            new List<List<BattlePhase_Enum>> { new List<BattlePhase_Enum>() { BattlePhase_Enum.RangeSiege, BattlePhase_Enum.Attack }, new List<BattlePhase_Enum>() { BattlePhase_Enum.RangeSiege, BattlePhase_Enum.Attack } }
            ) { }
    }
}
