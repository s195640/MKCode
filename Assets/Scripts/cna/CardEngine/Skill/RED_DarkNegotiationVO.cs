using cna.poo;
namespace cna {
    public partial class RED_DarkNegotiationVO : CardSkillVO {
        public override ActionResultVO ActionValid_00(ActionResultVO ar) {
            int i = 3;
            if (D.Scenario.isDay) {
                i = 2;
            }
            ar.ActionInfluence(i);
            ar.TurnPhase(TurnPhase_Enum.Influence);
            return ar;
        }
    }
}
