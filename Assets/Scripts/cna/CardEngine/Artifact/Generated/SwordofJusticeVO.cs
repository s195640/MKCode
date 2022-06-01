using System.Collections.Generic;
using cna.poo;
namespace cna {
    public partial class SwordofJusticeVO : CardArtifactVO {
        public SwordofJusticeVO(int uniqueId) : base(
            uniqueId,
            "Sword of Justice",
            Image_Enum.CT_sword_of_justice,
            new List<string> { "When you play this, discard any number of cards from your hand. You get Attack 3 for each card you discard this way. Fame +1 for each enemy you defeat this turn.", "Double the contribution of all physical attacks you play during your Attack phase this turn. Enemies lose physical resistance this turn. Fame +1 for each enemy you defeat this turn." },
            new List<List<TurnPhase_Enum>> { new List<TurnPhase_Enum>() { TurnPhase_Enum.Battle }, new List<TurnPhase_Enum>() { TurnPhase_Enum.Battle } },
            new List<List<BattlePhase_Enum>> { new List<BattlePhase_Enum>() { BattlePhase_Enum.Attack }, new List<BattlePhase_Enum>() { BattlePhase_Enum.Attack } }
            ) { }
    }
}
