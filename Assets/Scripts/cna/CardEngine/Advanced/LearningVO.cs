using cna.poo;
namespace cna {
    public partial class LearningVO : CardActionVO {
        public override GameAPI ActionValid_00(GameAPI ar) {
            ar.TurnPhase(TurnPhase_Enum.Influence);
            ar.ActionInfluence(2);
            ar.AddGameEffect(GameEffect_Enum.AC_Learning01, 0);
            return ar;
        }

        public override GameAPI ActionValid_01(GameAPI ar) {
            ar.TurnPhase(TurnPhase_Enum.Influence);
            ar.ActionInfluence(4 + ar.CardModifier);
            ar.AddGameEffect(GameEffect_Enum.AC_Learning02, 0);
            return ar;
        }
    }
}
