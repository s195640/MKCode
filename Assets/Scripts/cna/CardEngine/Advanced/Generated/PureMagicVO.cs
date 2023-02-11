using System.Collections.Generic;
using cna.poo;
namespace cna {
    public partial class PureMagicVO : CardActionVO {
        public PureMagicVO(int uniqueId) : base(
            uniqueId,
            "Pure Magic",
            Image_Enum.CA_pure_magic,
            CardType_Enum.Advanced,
            new List<string> { "When you play this, pay a mana. If you paid Green, Move 4. If you paid White, Influence 4. If you paid Blue, Block 4. If you paid Red, Attack 4.","When you play this, pay a mana. If you paid Green, Move 7. If you paid White, Influence 7. If you paid Blue, Block 7. If you paid Red, Attack 7." },
            new List<List<Crystal_Enum>> {new List<Crystal_Enum>() {Crystal_Enum.NA}, new List<Crystal_Enum>() {Crystal_Enum.Blue}},
            new List<List<TurnPhase_Enum>> {new List<TurnPhase_Enum>() {TurnPhase_Enum.Move, TurnPhase_Enum.Influence, TurnPhase_Enum.Battle}, new List<TurnPhase_Enum>() {TurnPhase_Enum.Move, TurnPhase_Enum.Influence, TurnPhase_Enum.Battle}},
            new List<List<BattlePhase_Enum>> {new List<BattlePhase_Enum>() {BattlePhase_Enum.Block, BattlePhase_Enum.Attack}, new List<BattlePhase_Enum>() {BattlePhase_Enum.Block, BattlePhase_Enum.Attack}},
            CardColor_Enum.Blue
            ) { }
    }
}
