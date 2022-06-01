using cna.poo;
namespace cna {
    public partial class RED_DarkPathsVO : CardSkillVO {
        public override ActionResultVO ActionValid_00(ActionResultVO ar) {
            int i = 2;
            if (D.Scenario.isDay) {
                i = 1;
            }
            ar.ActionMovement(i);
            ar.TurnPhase(TurnPhase_Enum.Move);
            return ar;
        }
    }
}
