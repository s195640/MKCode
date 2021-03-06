using System.Collections.Generic;
using cna.poo;
namespace cna {
    public partial class BLUE_MotivationVO : CardSkillVO {
        public BLUE_MotivationVO(int uniqueId) : base(
            uniqueId,
            "Motivation",
            CardType_Enum.Skill,
            SkillRefresh_Enum.Round,
            Image_Enum.SKB_motivation,
            Image_Enum.SKB_back,
            Image_Enum.A_meeple_tovak,
            new List<List<TurnPhase_Enum>> { new List<TurnPhase_Enum>() { TurnPhase_Enum.Move, TurnPhase_Enum.Influence, TurnPhase_Enum.Battle, TurnPhase_Enum.AfterBattle } },
            new List<List<BattlePhase_Enum>> { new List<BattlePhase_Enum>() { BattlePhase_Enum.RangeSiege, BattlePhase_Enum.Block, BattlePhase_Enum.AssignDamage, BattlePhase_Enum.Attack, BattlePhase_Enum.EndOfBattle } },
            false,
            "Once a round, on any players turn: Flip this to draw 2 cards. If you have the least fame (not tied), also gain one white mana "
            ) { }
    }
}
