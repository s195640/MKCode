using System.Collections.Generic;
using cna.poo;
namespace cna {
    public partial class SongofWindVO : CardActionVO {
        public SongofWindVO(int uniqueId) : base(
            uniqueId,
            "Song of Wind",
            Image_Enum.CA_song_of_wind,
            CardType_Enum.Advanced,
            new List<string> { "Move 2. The Move cost of plains, deserts and wastelands is reduced by 1, to a minimum of 0 this turn.","Move 2. The Move cost of plains, deserts and wastelands is reduced by 2, to a minimum of 0. You may pay a blue mana to be able to travel through lakes for Move cost 0 this turn." },
            new List<List<Crystal_Enum>> {new List<Crystal_Enum>() {Crystal_Enum.NA}, new List<Crystal_Enum>() {Crystal_Enum.White}},
            new List<List<TurnPhase_Enum>> {new List<TurnPhase_Enum>() {TurnPhase_Enum.Move}, new List<TurnPhase_Enum>() {TurnPhase_Enum.Move}},
            new List<List<BattlePhase_Enum>> {new List<BattlePhase_Enum>() {}, new List<BattlePhase_Enum>() {}},
            CardColor_Enum.White
            ) { }
    }
}
