namespace cna {
    public partial class GREEN_RedCrystalCraftVO : CardSkillVO {
        public override GameAPI ActionValid_00(GameAPI ar) {
            ar.CrystalBlue(1);
            ar.ManaRed(1);
            return ar;
        }
    }
}
