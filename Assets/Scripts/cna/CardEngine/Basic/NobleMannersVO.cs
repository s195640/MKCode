using cna.poo;

namespace cna {
    public partial class NobleMannersVO : CardActionVO {
        public override ActionResultVO ActionValid_00(ActionResultVO ar) {
            ar.TurnPhase(TurnPhase_Enum.Influence);
            ar.ActionInfluence(2);
            ar.Fame(1);
            return ar;
        }
        public override ActionResultVO ActionValid_01(ActionResultVO ar) {
            ar.TurnPhase(TurnPhase_Enum.Influence);
            ar.ActionInfluence(4 + ar.CardModifier);
            ar.Fame(1);
            ar.Rep(1);
            return ar;
        }
    }
}
