using cna.poo;
namespace cna {
    public partial class BLUE_NightSharpshootingVO : CardSkillVO {
        public override ActionResultVO ActionValid_00(ActionResultVO ar) {
            int i = 2;
            if (D.Scenario.isDay) {
                i = 1;
            }
            ar.BattleRange(new AttackData(i));
            return ar;
        }
    }
}
