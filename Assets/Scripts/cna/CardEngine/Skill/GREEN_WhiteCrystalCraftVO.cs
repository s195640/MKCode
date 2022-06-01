namespace cna {
    public partial class GREEN_WhiteCrystalCraftVO : CardSkillVO {
        public override ActionResultVO ActionValid_00(ActionResultVO ar) {
            ar.CrystalBlue(1);
            ar.ManaWhite(1);
            return ar;
        }
    }
}
