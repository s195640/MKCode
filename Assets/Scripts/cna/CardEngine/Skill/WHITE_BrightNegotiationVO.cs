using cna.poo;
namespace cna {
    public partial class WHITE_BrightNegotiationVO : CardSkillVO {
        public override ActionResultVO ActionValid_00(ActionResultVO ar) {
            int i = 2;
            if (D.Scenario.isDay) {
                i = 3;
            }
            ar.ActionInfluence(i);
            ar.TurnPhase(TurnPhase_Enum.Influence);
            return ar;
        }
    }
}
