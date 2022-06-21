namespace cna {
    public partial class WHITE_LeavesintheWindVO : CardSkillVO {
        public override GameAPI ActionValid_00(GameAPI ar) {
            ar.CrystalGreen(1);
            ar.ManaWhite(1);
            return ar;
        }
    }
}
