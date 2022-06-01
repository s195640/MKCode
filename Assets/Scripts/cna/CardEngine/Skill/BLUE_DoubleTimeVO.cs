namespace cna {
    public partial class BLUE_DoubleTimeVO : CardSkillVO {
        public override ActionResultVO ActionValid_00(ActionResultVO ar) {
            int i = 1;
            if (D.Scenario.isDay) {
                i = 2;
            }
            ar.ActionMovement(i);
            return ar;
        }
    }
}
