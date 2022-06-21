using cna.poo;

namespace cna {
    public partial class PromiseVO : CardActionVO {
        public override GameAPI ActionValid_00(GameAPI ar) {
            ar.TurnPhase(TurnPhase_Enum.Influence);
            ar.ActionInfluence(2);
            return ar;
        }
        public override GameAPI ActionValid_01(GameAPI ar) {
            ar.TurnPhase(TurnPhase_Enum.Influence);
            ar.ActionInfluence(4 + ar.CardModifier);
            return ar;
        }
    }
}
