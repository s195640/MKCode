using cna.poo;
namespace cna {
    public partial class AltemGuardiansVO : CardUnitVO {
        public override ActionResultVO ActionValid_00(ActionResultVO ar) {
            ar.BattleAttack(new AttackData(5));
            return ar;
        }

        public override ActionResultVO ActionValid_01(ActionResultVO ar) {
            ar.BattleBlock(new AttackData(8));
            ar.AddGameEffect(GameEffect_Enum.CUE_AltemGuardians01);
            return ar;
        }
        public override ActionResultVO ActionValid_02(ActionResultVO ar) {
            ar.AddGameEffect(GameEffect_Enum.CUE_AltemGuardians02);
            return ar;
        }
    }
}
