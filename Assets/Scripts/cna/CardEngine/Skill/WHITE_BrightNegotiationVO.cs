using cna.poo;
namespace cna {
    public partial class WHITE_BrightNegotiationVO : CardSkillVO {
        public override GameAPI ActionValid_00(GameAPI ar) {
            int i = 2;
            if (D.Scenario.isDay) {
                i = 3;
            }
            ar.ActionInfluence(i);
            ar.TurnPhase(TurnPhase_Enum.Influence);
            return ar;
        }
    }
}
