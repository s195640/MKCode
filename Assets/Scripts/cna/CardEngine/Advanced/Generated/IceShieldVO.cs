using System.Collections.Generic;
using cna.poo;
namespace cna {
    public partial class IceShieldVO : CardActionVO {
        public IceShieldVO(int uniqueId) : base(
            uniqueId,
            "Ice Shield",
            Image_Enum.CA_ice_shield,
            CardType_Enum.Advanced,
            new List<string> { "Ice Block 3", "Ice Block 3. Reduce the Armor of one enemy blocked this way by 3. Armor cannot be reduced below 1." },
            new List<List<Crystal_Enum>> { new List<Crystal_Enum>() { Crystal_Enum.NA }, new List<Crystal_Enum>() { Crystal_Enum.Blue } },
            new List<List<TurnPhase_Enum>> { new List<TurnPhase_Enum>() { TurnPhase_Enum.Battle }, new List<TurnPhase_Enum>() { TurnPhase_Enum.Battle } },
            new List<List<BattlePhase_Enum>> { new List<BattlePhase_Enum>() { BattlePhase_Enum.Block }, new List<BattlePhase_Enum>() { BattlePhase_Enum.Block } },
            CardColor_Enum.Blue
            ) { }
    }
}
