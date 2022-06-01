using System.Collections.Generic;
using cna.poo;
namespace cna {
    public partial class UndergroundTravelVO : CardSpellVO {
        public UndergroundTravelVO(int uniqueId) : base(
            uniqueId,
            "Underground Travel",
            "Underground Attack",
            CardType_Enum.Spell,
            Image_Enum.CS_underground_travel,
            new List<string> { "Move by up to 3 revealed spaces on the map. You must end your move on a safe space. Moving this way does not provoke rampaging enemies", "Move by up to 3 revealed spaces on the map. You must end your move on fortified site and trigger a battle. Ignore site fortifications. If withdrawing after the combat, return to your original position." },
            new List<List<Crystal_Enum>> { new List<Crystal_Enum>() { Crystal_Enum.Green }, new List<Crystal_Enum>() { Crystal_Enum.Green, Crystal_Enum.Black } },
            new List<List<TurnPhase_Enum>> { new List<TurnPhase_Enum>() { TurnPhase_Enum.Move }, new List<TurnPhase_Enum>() { TurnPhase_Enum.Move } },
            new List<List<BattlePhase_Enum>> { new List<BattlePhase_Enum>() { }, new List<BattlePhase_Enum>() { } },
            CardColor_Enum.Green
            ) { }
    }
}
