using cna.poo;

namespace cna {
    public partial class NobleMannersVO : CardActionVO {
        public override GameAPI ActionValid_00(GameAPI ar) {
            ar.TurnPhase(TurnPhase_Enum.Influence);
            ar.ActionInfluence(2);
            ar.Fame(1);
            return ar;
        }
        public override GameAPI ActionValid_01(GameAPI ar) {
            ar.TurnPhase(TurnPhase_Enum.Influence);
            ar.ActionInfluence(4 + ar.CardModifier);
            ar.Fame(1);
            ar.Rep(1);
            return ar;
        }
    }
}
