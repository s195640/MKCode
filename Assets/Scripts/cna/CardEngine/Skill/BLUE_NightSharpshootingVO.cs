using cna.poo;
namespace cna {
    public partial class BLUE_NightSharpshootingVO : CardSkillVO {
        public override GameAPI ActionValid_00(GameAPI ar) {
            int i = 2;
            if (D.Scenario.isDay) {
                i = 1;
            }
            ar.BattleRange(new AttackData(i));
            return ar;
        }
    }
}
