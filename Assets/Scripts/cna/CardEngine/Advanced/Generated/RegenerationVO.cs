using System.Collections.Generic;
using cna.poo;
namespace cna {
    public partial class RegenerationVO : CardActionVO {
        public RegenerationVO(int uniqueId) : base(
            uniqueId,
            "Regeneration",
            Image_Enum.CA_regeneration,
            CardType_Enum.Advanced,
            new List<string> { "Heal 1. Ready a Level I or II Unit you control.","Heal 2. Ready a Level I, II or III Unit you control." },
            new List<List<Crystal_Enum>> {new List<Crystal_Enum>() {Crystal_Enum.NA}, new List<Crystal_Enum>() {Crystal_Enum.Green}},
            new List<List<TurnPhase_Enum>> {new List<TurnPhase_Enum>() {TurnPhase_Enum.Move, TurnPhase_Enum.Influence, TurnPhase_Enum.AfterBattle}, new List<TurnPhase_Enum>() {TurnPhase_Enum.Move, TurnPhase_Enum.Influence, TurnPhase_Enum.AfterBattle}},
            new List<List<BattlePhase_Enum>> {new List<BattlePhase_Enum>() {}, new List<BattlePhase_Enum>() {}},
            CardColor_Enum.Green
            ) { }
    }
}
