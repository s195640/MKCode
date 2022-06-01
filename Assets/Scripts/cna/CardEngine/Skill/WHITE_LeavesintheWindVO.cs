namespace cna {
    public partial class WHITE_LeavesintheWindVO : CardSkillVO {
        public override ActionResultVO ActionValid_00(ActionResultVO ar) {
            ar.CrystalGreen(1);
            ar.ManaWhite(1);
            return ar;
        }
    }
}
