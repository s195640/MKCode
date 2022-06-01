namespace cna {
    public partial class GREEN_GreenCrystalCraftVO : CardSkillVO {
        public override ActionResultVO ActionValid_00(ActionResultVO ar) {
            ar.CrystalBlue(1);
            ar.ManaGreen(1);
            return ar;
        }
    }
}
