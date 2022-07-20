using System.Collections.Generic;
using cna.poo;
namespace cna {
    public partial class ImprovisationVO : CardActionVO {
        public ImprovisationVO(int uniqueId, Image_Enum avatar) : base(
            uniqueId,
            "Improvisation",
            Image_Enum.CB_improvisation,
            CardType_Enum.Basic,
            new List<string> { "Discard another card from your hand to get Move 3, Influence 3, Attack 3 or Block 3.","Discard another card from your hand to get Move 5, Influence 5, Attack 5 or Block 5." },
            new List<List<Crystal_Enum>> {new List<Crystal_Enum>() {Crystal_Enum.NA}, new List<Crystal_Enum>() {Crystal_Enum.Red}},
            new List<List<TurnPhase_Enum>> {new List<TurnPhase_Enum>() {TurnPhase_Enum.Move, TurnPhase_Enum.Influence, TurnPhase_Enum.Battle}, new List<TurnPhase_Enum>() {TurnPhase_Enum.Move, TurnPhase_Enum.Influence, TurnPhase_Enum.Battle}},
            new List<List<BattlePhase_Enum>> {new List<BattlePhase_Enum>() {BattlePhase_Enum.Block, BattlePhase_Enum.Attack}, new List<BattlePhase_Enum>() {BattlePhase_Enum.Block, BattlePhase_Enum.Attack}},
            CardColor_Enum.Red
            ) { Avatar = avatar; }
    }
}
