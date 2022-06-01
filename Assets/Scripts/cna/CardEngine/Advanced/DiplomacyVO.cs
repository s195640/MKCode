using cna.poo;
namespace cna {
    public partial class DiplomacyVO : CardActionVO {
        public override ActionResultVO ActionValid_00(ActionResultVO ar) {
            ar.TurnPhase(TurnPhase_Enum.Move);
            ar.AddGameEffect(GameEffect_Enum.AC_Diplomacy01);
            ar.ActionInfluence(2);
            return ar;
        }
        public override ActionResultVO ActionValid_01(ActionResultVO ar) {
            ar.TurnPhase(TurnPhase_Enum.Move);
            ar.AddGameEffect(GameEffect_Enum.AC_Diplomacy02);
            ar.ActionInfluence(4 + ar.CardModifier);
            return ar;
        }
    }
}
