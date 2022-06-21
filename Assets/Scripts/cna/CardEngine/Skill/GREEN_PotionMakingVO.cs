namespace cna {
    public partial class GREEN_PotionMakingVO : CardSkillVO {
        public override GameAPI ActionValid_00(GameAPI ar) {
            ar.Healing(2);
            return ar;
        }
    }
}
