using System.Collections.Generic;
using cna.poo;
namespace cna {
    public partial class FlameWallVO : CardSpellVO {
        public FlameWallVO(int uniqueId) : base(
            uniqueId,
            "Flame Wall",
            "Flame Wave",
            CardType_Enum.Spell,
            Image_Enum.CS_flame_wall,
            new List<string> { "Fire Attack 5 or Fire Block 7", "Fire Attack 5 or Fire Block 7. This attack or block gets increased by 2 for each enemy token you are facing." },
            new List<List<Crystal_Enum>> { new List<Crystal_Enum>() { Crystal_Enum.Red }, new List<Crystal_Enum>() { Crystal_Enum.Red, Crystal_Enum.Black } },
            new List<List<TurnPhase_Enum>> { new List<TurnPhase_Enum>() { TurnPhase_Enum.Battle }, new List<TurnPhase_Enum>() { TurnPhase_Enum.Battle } },
            new List<List<BattlePhase_Enum>> { new List<BattlePhase_Enum>() { BattlePhase_Enum.Block, BattlePhase_Enum.Attack }, new List<BattlePhase_Enum>() { BattlePhase_Enum.Block, BattlePhase_Enum.Attack } },
            CardColor_Enum.Red
            ) { }
    }
}
