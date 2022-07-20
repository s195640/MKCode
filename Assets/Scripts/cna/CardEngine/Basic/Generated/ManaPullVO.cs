using System.Collections.Generic;
using cna.poo;
namespace cna {
    public partial class ManaPullVO : CardActionVO {
        public ManaPullVO(int uniqueId, Image_Enum avatar) : base(
            uniqueId,
            "Mana Pull",
            Image_Enum.CB_mana_pull,
            CardType_Enum.Basic,
            new List<string> { "You can use one additional mana die from the Source this turn.","Take a mana die from the source and set it to any color except gold. Gain 1 mana and 1 crystal of that color. Do not reroll this die when you return it to the Source." },
            new List<List<Crystal_Enum>> {new List<Crystal_Enum>() {Crystal_Enum.NA}, new List<Crystal_Enum>() {Crystal_Enum.White}},
            new List<List<TurnPhase_Enum>> {new List<TurnPhase_Enum>() {TurnPhase_Enum.Move, TurnPhase_Enum.Influence, TurnPhase_Enum.Battle, TurnPhase_Enum.AfterBattle}, new List<TurnPhase_Enum>() {TurnPhase_Enum.Move, TurnPhase_Enum.Influence, TurnPhase_Enum.Battle, TurnPhase_Enum.AfterBattle}},
            new List<List<BattlePhase_Enum>> {new List<BattlePhase_Enum>() {BattlePhase_Enum.RangeSiege, BattlePhase_Enum.Block, BattlePhase_Enum.AssignDamage, BattlePhase_Enum.Attack, BattlePhase_Enum.EndOfBattle}, new List<BattlePhase_Enum>() {BattlePhase_Enum.RangeSiege, BattlePhase_Enum.Block, BattlePhase_Enum.AssignDamage, BattlePhase_Enum.Attack, BattlePhase_Enum.EndOfBattle}},
            CardColor_Enum.White
            ) { Avatar = avatar; }
    }
}
