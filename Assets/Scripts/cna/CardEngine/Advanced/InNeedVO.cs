using cna.poo;
namespace cna {
    public partial class InNeedVO : CardActionVO {
        public override ActionResultVO ActionValid_00(ActionResultVO ar) {
            int wounds = totalWoundsInHandAndOnUnits(ar);
            ar.TurnPhase(TurnPhase_Enum.Influence);
            ar.ActionInfluence(3 + wounds);
            return ar;
        }
        public override ActionResultVO ActionValid_01(ActionResultVO ar) {
            int wounds = totalWoundsInHandAndOnUnits(ar) * 2;
            ar.TurnPhase(TurnPhase_Enum.Influence);
            ar.ActionInfluence(5 + ar.CardModifier + wounds);
            return ar;
        }

        private int totalWoundsInHandAndOnUnits(ActionResultVO ar) {
            int total = 0;
            ar.LocalPlayer.Deck.Hand.ForEach(c => {
                if (D.Cards[c].CardType == CardType_Enum.Wound) {
                    total++;
                }
            });

            ar.LocalPlayer.Deck.State.Values.ForEach(w => {
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
