using System.Collections.Generic;
using cna.poo;

namespace cna {
    public class CardRuinVO : CardVO {
        public CardRuinVO(int uniqueId, Image_Enum image, CardType_Enum cardType, Crystal_Enum cost, List<MonsterType_Enum> monsters = null, List<Reward_Enum> rewards = null) : base(
            uniqueId,
            "Ancient Ruins",
            image,
            cardType
            ) {
            if (cardType == CardType_Enum.AncientRuins_Alter) {
                Costs = new List<List<Crystal_Enum>>() { new List<Crystal_Enum>() { cost, cost, cost } };
            } else {
                Monsters = monsters;
                Rewards = rewards;
            }
            MonsterBackCardId = Image_Enum.R_back;
        }
    }
}
