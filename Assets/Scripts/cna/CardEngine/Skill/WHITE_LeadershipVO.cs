using cna.poo;
namespace cna {
    public partial class WHITE_LeadershipVO : CardSkillVO {
        public override ActionResultVO ActionValid_00(ActionResultVO ar) {
            ar.AddGameEffect(GameEffect_Enum.WHITE_Leadership);
            ar.AddLog("Next Unit activated will be busted during combat");
            return ar;
        }
    }
}
