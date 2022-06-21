using cna.poo;
namespace cna {
    public partial class InNeedVO : CardActionVO {
        public override GameAPI ActionValid_00(GameAPI ar) {
            int wounds = totalWoundsInHandAndOnUnits(ar);
            ar.TurnPhase(TurnPhase_Enum.Influence);
            ar.ActionInfluence(3 + wounds);
            return ar;
        }
        public override GameAPI ActionValid_01(GameAPI ar) {
            int wounds = totalWoundsInHandAndOnUnits(ar) * 2;
            ar.TurnPhase(TurnPhase_Enum.Influence);
            ar.ActionInfluence(5 + ar.CardModifier + wounds);
            return ar;
        }

        private int totalWoundsInHandAndOnUnits(GameAPI ar) {
            int total = 0;
            ar.P.Deck.Hand.ForEach(c => {
                if (D.Cards[c].CardType == CardType_Enum.Wound) {
                    total++;
                }
            });

            ar.P.Deck.State.Values.ForEach(w => {
                if (w.Contains(CardState_Enum.Unit_Wounded)) {
                    total++;
                }
                if (w.Contains(CardState_Enum.Unit_Poisoned)) {
                    total++;
                }
            });

            return total;
        }
    }
}
