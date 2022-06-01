using System.Collections.Generic;
using cna.poo;
namespace cna {
    public partial class MeditationVO : CardSpellVO {
        public MeditationVO(int uniqueId) : base(
            uniqueId,
            "Meditation",
            "Trance",
            CardType_Enum.Spell,
            Image_Enum.CS_meditation,
            new List<string> { "Randomly pick two cards from your discard pile and place them either on the top or bottom of your Deed deck. At the end of this turn, draw two cards over your hand limit.", "Randomly pick four cards from your discard pile and place them either on the top or bottom of your Deed deck. At the end of this turn, draw four cards over your hand limit." },
            new List<List<Crystal_Enum>> { new List<Crystal_Enum>() { Crystal_Enum.Green }, new List<Crystal_Enum>() { Crystal_Enum.Green, Crystal_Enum.Black } },
            new List<List<TurnPhase_Enum>> { new List<TurnPhase_Enum>() { TurnPhase_Enum.Move, TurnPhase_Enum.Influence, TurnPhase_Enum.Battle, TurnPhase_Enum.AfterBattle }, new List<TurnPhase_Enum>() { TurnPhase_Enum.Move, TurnPhase_Enum.Influence, TurnPhase_Enum.Battle, TurnPhase_Enum.AfterBattle } },
            new List<List<BattlePhase_Enum>> { new List<BattlePhase_Enum>() { BattlePhase_Enum.RangeSiege, BattlePhase_Enum.Block, BattlePhase_Enum.AssignDamage, BattlePhase_Enum.Attack, BattlePhase_Enum.EndOfBattle }, new List<BattlePhase_Enum>() { BattlePhase_Enum.RangeSiege, BattlePhase_Enum.Block, BattlePhase_Enum.AssignDamage, BattlePhase_Enum.Attack, BattlePhase_Enum.EndOfBattle } },
            CardColor_Enum.Green
            ) { }
    }
}
