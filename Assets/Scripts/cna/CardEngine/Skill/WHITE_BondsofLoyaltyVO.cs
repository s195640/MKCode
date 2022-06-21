namespace cna {
    public partial class WHITE_BondsofLoyaltyVO : CardSkillVO {
        public override GameAPI checkAllowedToUse(GameAPI ar) {
            ar.ErrorMsg = "This Skill is activated when you recruit a unit.";
            return ar;
        }
    }
}
