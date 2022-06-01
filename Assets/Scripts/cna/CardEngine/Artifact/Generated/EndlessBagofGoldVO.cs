using System.Collections.Generic;
using cna.poo;
namespace cna {
    public partial class EndlessBagofGoldVO : CardArtifactVO {
        public EndlessBagofGoldVO(int uniqueId) : base(
            uniqueId,
            "Endless Bag of Gold",
            Image_Enum.CT_endless_bag_of_gold,
            new List<string> { "Influence 4, Fame +2.", "Influence 9, Fame +3." },
            new List<List<TurnPhase_Enum>> { new List<TurnPhase_Enum>() { TurnPhase_Enum.Influence }, new List<TurnPhase_Enum>() { TurnPhase_Enum.Influence } },
            new List<List<BattlePhase_Enum>> { new List<BattlePhase_Enum>() { }, new List<BattlePhase_Enum>() { } }
            ) { }
    }
}
