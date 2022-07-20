using System.Collections.Generic;
using cna.poo;
namespace cna {
    public partial class DruidicPathsVO : CardActionVO {
        public DruidicPathsVO(int uniqueId, Image_Enum avatar) : base(
            uniqueId,
            "Druidic Paths",
            Image_Enum.CB_druidic_paths,
            CardType_Enum.Basic,
            new List<string> { "Move 2. The Move cost of one type of terrain is Reduced by 1 this turn, to a minimum of 2.","Move 4. The Move cost of one type of terrain is reduced by 1 this turn, to a minimum of 2." },
            new List<List<Crystal_Enum>> {new List<Crystal_Enum>() {Crystal_Enum.NA}, new List<Crystal_Enum>() {Crystal_Enum.Blue}},
            new List<List<TurnPhase_Enum>> {new List<TurnPhase_Enum>() {TurnPhase_Enum.Move}, new List<TurnPhase_Enum>() {TurnPhase_Enum.Move}},
            new List<List<BattlePhase_Enum>> {new List<BattlePhase_Enum>() {}, new List<BattlePhase_Enum>() {}},
            CardColor_Enum.Blue
            ) { Avatar = avatar; }
    }
}
