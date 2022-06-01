namespace cna {
    public partial class WHITE_BondsofLoyaltyVO : CardSkillVO {
        public override ActionResultVO checkAllowedToUse(ActionResultVO ar) {
            ar.ErrorMsg = "This Skill is activated when you recruit a unit.";
            return ar;
        }
    }
}
