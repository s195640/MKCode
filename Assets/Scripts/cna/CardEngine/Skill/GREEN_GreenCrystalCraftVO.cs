namespace cna {
    public partial class GREEN_GreenCrystalCraftVO : CardSkillVO {
        public override GameAPI ActionValid_00(GameAPI ar) {
            ar.CrystalBlue(1);
            ar.ManaGreen(1);
            return ar;
        }
    }
}
