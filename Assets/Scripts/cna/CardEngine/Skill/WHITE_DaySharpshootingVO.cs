using cna.poo;
namespace cna {
    public partial class WHITE_DaySharpshootingVO : CardSkillVO {
        public override GameAPI ActionValid_00(GameAPI ar) {
            int i = 1;
            if (D.Scenario.isDay) {
                i = 2;
            }
            ar.BattleRange(new AttackData(i));
            return ar;
        }
    }
}
