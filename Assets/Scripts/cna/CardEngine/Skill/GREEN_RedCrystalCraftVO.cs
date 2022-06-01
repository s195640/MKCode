namespace cna {
    public partial class GREEN_RedCrystalCraftVO : CardSkillVO {
        public override ActionResultVO ActionValid_00(ActionResultVO ar) {
            ar.CrystalBlue(1);
            ar.ManaRed(1);
            return ar;
        }
    }
}
