using cna.poo;

namespace cna {
    public partial class UtemGuardsmenVO : CardUnitVO {
        public override ActionResultVO ActionValid_00(ActionResultVO ar) {
            ar.BattleAttack(new AttackData(2));
            return ar;
        }
        public override ActionResultVO ActionValid_01(ActionResultVO ar) {
            ar.BattleBlock(new AttackData(4));
            ar.AddGameEffect(GameEffect_Enum.UtemGuardsmen);
            return ar;
        }
    }
}
