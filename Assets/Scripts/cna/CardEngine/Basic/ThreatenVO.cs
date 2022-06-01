using cna.poo;

namespace cna {
    public partial class ThreatenVO : CardActionVO {
        public override ActionResultVO ActionValid_00(ActionResultVO ar) {
            ar.TurnPhase(TurnPhase_Enum.Influence);
            ar.ActionInfluence(2);
            return ar;
        }
        public override ActionResultVO ActionValid_01(ActionResultVO ar) {
            ar.TurnPhase(TurnPhase_Enum.Influence);
            ar.ActionInfluence(5 + ar.CardModifier);
            ar.Rep(-1);
            return ar;
        }
    }
}
