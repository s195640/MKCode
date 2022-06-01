using System.Collections.Generic;
using cna.poo;
namespace cna {
    public partial class NobleMannersVO : CardActionVO {
        public NobleMannersVO(int uniqueId, Image_Enum avatar) : base(
            uniqueId,
            "Noble Manners",
            Image_Enum.CB_noble_manners,
            CardType_Enum.Basic,
            new List<string> { "Influence 2. If you use this during interaction you get Fame +1 at the end of the turn.", "Influence 4. If you use this during interaction you get Fame +1 and Reputation +1 at the end of the turn." },
            new List<List<Crystal_Enum>> { new List<Crystal_Enum>() { Crystal_Enum.NA }, new List<Crystal_Enum>() { Crystal_Enum.White } },
            new List<List<TurnPhase_Enum>> { new List<TurnPhase_Enum>() { TurnPhase_Enum.Influence }, new List<TurnPhase_Enum>() { TurnPhase_Enum.Influence } },
            new List<List<BattlePhase_Enum>> { new List<BattlePhase_Enum>() { }, new List<BattlePhase_Enum>() { } },
            CardColor_Enum.White
            ) { Avatar = avatar; }
    }
}
