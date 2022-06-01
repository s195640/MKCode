using System.Collections.Generic;
using cna.poo;
namespace cna {
    public partial class RestorationVO : CardSpellVO {
        public RestorationVO(int uniqueId) : base(
            uniqueId,
            "Restoration",
            "Rebirth",
            CardType_Enum.Spell,
            Image_Enum.CS_restoration,
            new List<string> { "Heal 3. If you are in a forest, Heal 5 instead.", "Heal 3. If you are in a forest, Heal 5 instead. Ready up to 3 levels worth of Units. If you are in a forest, Ready up to 5 levels of Units instead." },
            new List<List<Crystal_Enum>> { new List<Crystal_Enum>() { Crystal_Enum.Green }, new List<Crystal_Enum>() { Crystal_Enum.Green, Crystal_Enum.Black } },
            new List<List<TurnPhase_Enum>> { new List<TurnPhase_Enum>() { TurnPhase_Enum.Move, TurnPhase_Enum.Influence, TurnPhase_Enum.AfterBattle }, new List<TurnPhase_Enum>() { TurnPhase_Enum.Move, TurnPhase_Enum.Influence, TurnPhase_Enum.AfterBattle } },
            new List<List<BattlePhase_Enum>> { new List<BattlePhase_Enum>() { }, new List<BattlePhase_Enum>() { } },
            CardColor_Enum.Green
            ) { }
    }
}
