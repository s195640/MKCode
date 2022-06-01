using System.Collections.Generic;
using cna.poo;
namespace cna {
    public partial class WingsofWindVO : CardSpellVO {
        public WingsofWindVO(int uniqueId) : base(
            uniqueId,
            "Wings of Wind",
            "Wings of Night",
            CardType_Enum.Spell,
            Image_Enum.CS_wings_of_wind,
            new List<string> { "When you play this, spend 1-5 Move points and move one revealed space on the map for each. You must end your move in a safe space. Moving this way does not provoke rampaging enemies.", "Target enemy does not attack this combat. Your movement points will be reset to 0.  You may target additional enemies: pay 1 Move point for the second enemy, 2 Move points for the third enemy, etc." },
            new List<List<Crystal_Enum>> { new List<Crystal_Enum>() { Crystal_Enum.White }, new List<Crystal_Enum>() { Crystal_Enum.White, Crystal_Enum.Black } },
            new List<List<TurnPhase_Enum>> { new List<TurnPhase_Enum>() { TurnPhase_Enum.Move }, new List<TurnPhase_Enum>() { TurnPhase_Enum.Battle } },
            new List<List<BattlePhase_Enum>> { new List<BattlePhase_Enum>() { }, new List<BattlePhase_Enum>() { BattlePhase_Enum.AssignDamage } },
            CardColor_Enum.White
            ) { }
    }
}
