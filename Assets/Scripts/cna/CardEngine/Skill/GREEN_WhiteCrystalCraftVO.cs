namespace cna {
    public partial class GREEN_WhiteCrystalCraftVO : CardSkillVO {
        public override GameAPI ActionValid_00(GameAPI ar) {
            ar.CrystalBlue(1);
            ar.ManaWhite(1);
            return ar;
        }
    }
}
