namespace cna {
    public partial class GREEN_PotionMakingVO : CardSkillVO {
        public override ActionResultVO ActionValid_00(ActionResultVO ar) {
            ar.Healing(2);
            return ar;
        }
    }
}
