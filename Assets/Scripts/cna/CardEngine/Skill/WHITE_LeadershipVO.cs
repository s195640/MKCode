using cna.poo;
namespace cna {
    public partial class WHITE_LeadershipVO : CardSkillVO {
        public override GameAPI ActionValid_00(GameAPI ar) {
            ar.AddGameEffect(GameEffect_Enum.WHITE_Leadership);
            ar.AddLog("Next Unit activated will be busted during combat");
            return ar;
        }
    }
}
