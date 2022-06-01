using System.Collections.Generic;
using cna.poo;
namespace cna {
    public partial class LearningVO : CardActionVO {
        public LearningVO(int uniqueId) : base(
            uniqueId,
            "Learning",
            Image_Enum.CA_learning,
            CardType_Enum.Advanced,
            new List<string> { "Influence 2. Once during this turn, you may pay Influence 6 to gain an Advanced Action card from the Advanced Actions offer to your discard pile.", "Influence 4. Once during this turn, you may pay Influence 9 to gain an Advanced Action card from the Advanced Actions offer to your hand." },
            new List<List<Crystal_Enum>> { new List<Crystal_Enum>() { Crystal_Enum.NA }, new List<Crystal_Enum>() { Crystal_Enum.White } },
            new List<List<TurnPhase_Enum>> { new List<TurnPhase_Enum>() { TurnPhase_Enum.Move, TurnPhase_Enum.Influence, TurnPhase_Enum.Battle, TurnPhase_Enum.AfterBattle }, new List<TurnPhase_Enum>() { TurnPhase_Enum.Move, TurnPhase_Enum.Influence, TurnPhase_Enum.Battle, TurnPhase_Enum.AfterBattle } },
            new List<List<BattlePhase_Enum>> { new List<BattlePhase_Enum>() { BattlePhase_Enum.RangeSiege, BattlePhase_Enum.Block, BattlePhase_Enum.AssignDamage, BattlePhase_Enum.Attack, BattlePhase_Enum.EndOfBattle }, new List<BattlePhase_Enum>() { BattlePhase_Enum.RangeSiege, BattlePhase_Enum.Block, BattlePhase_Enum.AssignDamage, BattlePhase_Enum.Attack, BattlePhase_Enum.EndOfBattle } },
            CardColor_Enum.White
            ) { }
    }
}
