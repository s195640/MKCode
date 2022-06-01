
using System.Collections.Generic;
using cna.poo;

namespace cna {
    public class CardSkillVO : CardActionVO {
        public CardSkillVO(
            int uniqueId,
            string cardTitle,
            CardType_Enum cardType,
            SkillRefresh_Enum refresh,
            Image_Enum cardImage,
            Image_Enum cardImageBack,
            Image_Enum avatar,
            List<List<TurnPhase_Enum>> allowed,
            List<List<BattlePhase_Enum>> battleAllowed,
            bool interactive,
            string description)
                : base(uniqueId, cardTitle, cardImage, cardType, new List<string>() { description }, new List<List<Crystal_Enum>> { new List<Crystal_Enum> { Crystal_Enum.NA }, new List<Crystal_Enum> { Crystal_Enum.NA } }, allowed, battleAllowed) {
            SkillBackCardId = cardImageBack;
            Avatar = avatar;
            SkillInteractive = interactive;
            SkillRefresh = refresh;
        }
    }
}
