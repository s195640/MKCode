using System.Collections.Generic;
using cna.poo;
namespace cna {
    public partial class RejuvenateVO : CardActionVO {
        public RejuvenateVO(int uniqueId, Image_Enum avatar) : base(
            uniqueId,
            "Rejuvenate",
            Image_Enum.CB_rejuvenate,
            CardType_Enum.Basic,
            new List<string> { "Heal 1, draw a card, gain a green mana token, or ready a level I or II Unit.","Heal 2, draw two cards, gain a green mana crystal to your inventory, or ready a level I, II or III Unit." },
            new List<List<Crystal_Enum>> {new List<Crystal_Enum>() {Crystal_Enum.NA}, new List<Crystal_Enum>() {Crystal_Enum.Green}},
            new List<List<TurnPhase_Enum>> {new List<TurnPhase_Enum>() {TurnPhase_Enum.Move, TurnPhase_Enum.Influence, TurnPhase_Enum.Battle, TurnPhase_Enum.AfterBattle}, new List<TurnPhase_Enum>() {TurnPhase_Enum.Move, TurnPhase_Enum.Influence, TurnPhase_Enum.Battle, TurnPhase_Enum.AfterBattle}},
            new List<List<BattlePhase_Enum>> {new List<BattlePhase_Enum>() {BattlePhase_Enum.RangeSiege, BattlePhase_Enum.Block, BattlePhase_Enum.AssignDamage, BattlePhase_Enum.Attack, BattlePhase_Enum.EndOfBattle}, new List<BattlePhase_Enum>() {BattlePhase_Enum.RangeSiege, BattlePhase_Enum.Block, BattlePhase_Enum.AssignDamage, BattlePhase_Enum.Attack, BattlePhase_Enum.EndOfBattle}},
            CardColor_Enum.Green
            ) { Avatar = avatar; }
    }
}
