using System.Collections.Generic;
using cna.poo;

namespace cna {
    public abstract class CardSpellVO : CardActionVO {

        public CardSpellVO(int uniqueId,
            string spellTitle01,
            string spellTitle02,
            CardType_Enum cardType,
            Image_Enum cardImage,
            List<string> actions,
            List<List<Crystal_Enum>> costs,
            List<List<TurnPhase_Enum>> allowed,
            List<List<BattlePhase_Enum>> battleAllowed,
            CardColor_Enum cardColor)
            : base(uniqueId, spellTitle01, cardImage, cardType, actions, costs, allowed, battleAllowed, cardColor) {
            SpellTitle = new string[] { spellTitle01, spellTitle02 };
        }
    }
}
