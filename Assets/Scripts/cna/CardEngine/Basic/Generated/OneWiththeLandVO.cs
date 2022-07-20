using System.Collections.Generic;
using cna.poo;
namespace cna {
    public partial class OneWiththeLandVO : CardActionVO {
        public OneWiththeLandVO(int uniqueId, Image_Enum avatar) : base(
            uniqueId,
            "One With the Land",
            Image_Enum.CB_one_with_the_land,
            CardType_Enum.Basic,
            new List<string> { "Move 2, Heal 1, or Block 2","Move 4, Heal 2 or Block equal the unmodified Move cost of the space you are in (Mountain 5, Lakes 2). This is Fire Block in the day, and Ice Block at night." },
            new List<List<Crystal_Enum>> {new List<Crystal_Enum>() {Crystal_Enum.NA}, new List<Crystal_Enum>() {Crystal_Enum.Green}},
            new List<List<TurnPhase_Enum>> {new List<TurnPhase_Enum>() {TurnPhase_Enum.Move, TurnPhase_Enum.Influence, TurnPhase_Enum.Battle, TurnPhase_Enum.AfterBattle}, new List<TurnPhase_Enum>() {TurnPhase_Enum.Move, TurnPhase_Enum.Influence, TurnPhase_Enum.Battle, TurnPhase_Enum.AfterBattle}},
            new List<List<BattlePhase_Enum>> {new List<BattlePhase_Enum>() {BattlePhase_Enum.Block}, new List<BattlePhase_Enum>() {BattlePhase_Enum.Block}},
            CardColor_Enum.Green
            ) { Avatar = avatar; }
    }
}
