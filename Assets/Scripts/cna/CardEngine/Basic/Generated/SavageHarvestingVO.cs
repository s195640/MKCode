using System.Collections.Generic;
using cna.poo;
namespace cna {
    public partial class SavageHarvestingVO : CardActionVO {
        public SavageHarvestingVO(int uniqueId, Image_Enum avatar) : base(
            uniqueId,
            "Savage Harvesting",
            Image_Enum.CB_savage_harvesting,
            CardType_Enum.Basic,
            new List<string> { "Move 2. Once this turn when you move a space you may discard a card to gain a crystal of the same color. If you discard an Artifact you may choose the color.","Move 4. Each time you move a space this turn you may discard a card to gain a crystal of the same color. If you discard an Artifact you may choose the color." },
            new List<List<Crystal_Enum>> {new List<Crystal_Enum>() {Crystal_Enum.NA}, new List<Crystal_Enum>() {Crystal_Enum.Green}},
            new List<List<TurnPhase_Enum>> {new List<TurnPhase_Enum>() {TurnPhase_Enum.Move}, new List<TurnPhase_Enum>() {TurnPhase_Enum.Move}},
            new List<List<BattlePhase_Enum>> {new List<BattlePhase_Enum>() {}, new List<BattlePhase_Enum>() {}},
            CardColor_Enum.Green
            ) { Avatar = avatar; }
    }
}
