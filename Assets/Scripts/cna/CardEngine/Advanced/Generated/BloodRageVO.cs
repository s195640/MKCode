using System.Collections.Generic;
using cna.poo;
namespace cna {
    public partial class BloodRageVO : CardActionVO {
        public BloodRageVO(int uniqueId) : base(
            uniqueId,
            "Blood Rage",
            Image_Enum.CA_blood_rage,
            CardType_Enum.Advanced,
            new List<string> { "Attack 2. You can take a Wound to increase this to Attack 5.","Attack 4. You can take a Wound to increase this to Attack 9." },
            new List<List<Crystal_Enum>> {new List<Crystal_Enum>() {Crystal_Enum.NA}, new List<Crystal_Enum>() {Crystal_Enum.Red}},
            new List<List<TurnPhase_Enum>> {new List<TurnPhase_Enum>() {TurnPhase_Enum.Battle}, new List<TurnPhase_Enum>() {TurnPhase_Enum.Battle}},
            new List<List<BattlePhase_Enum>> {new List<BattlePhase_Enum>() {BattlePhase_Enum.Attack}, new List<BattlePhase_Enum>() {BattlePhase_Enum.Attack}},
            CardColor_Enum.Red
            ) { }
    }
}
